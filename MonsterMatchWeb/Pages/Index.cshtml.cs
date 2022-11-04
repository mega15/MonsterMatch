using BusinessLogic;
using BusinessLogic.Web.Contracts;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Net.WebSockets;

namespace MonsterMatchWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ContextBizagiMatch _context;
        private readonly IAssetService _assetService;

        [BindProperty]
        public List<Match> GameMatches { get; set; }
        [BindProperty]
        public PlayersByGame PlayerGameData { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string LoginName { get; set; }
        [BindProperty]
        public Guid CharacterId { get; set; }
        [BindProperty]
        public List<PlayersByGame> OldGamesData { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ContextBizagiMatch context, IAssetService assetService)
        {
            _logger = logger;
            _context = context;
            _assetService = assetService;
        }

        public async Task<IActionResult> OnGet()
        {
            var loginId = HttpContext.Session.GetString("LoginId");

            if (string.IsNullOrEmpty(loginId))
                return RedirectToPage("./Login");

            if (!loginId.Equals("Admin"))
            {
                var login = await _context.Players.FindAsync(Guid.Parse(loginId));
                LoginName = login.Name;

                var activeGame = await _context.Games.FirstOrDefaultAsync(g => !g.IsClosed);
                var OldGamesData = await _context.PlayersByGames.Include(pg => pg.Game).Where(pg => pg.PlayerId == Guid.Parse(loginId) && pg.Game.IsClosed).ToListAsync();

                foreach(var oldMatch in OldGamesData)
                {
                    oldMatch.PointsPosition = (await _context.PlayersByGames.Include(pg => pg.GameId == oldMatch.GameId).OrderBy(pg => pg.Points).ToListAsync()).
                        FindIndex(pg => pg.PlayerId == Guid.Parse(loginId))+1;

                    oldMatch.MoneyPosition = (await _context.PlayersByGames.Include(pg => pg.GameId == oldMatch.GameId).OrderBy(pg => pg.Money).ToListAsync()).
                        FindIndex(pg => pg.PlayerId == Guid.Parse(loginId)) + 1;
                }

                if (activeGame != null)
                {
                    Message = $"There is an active game named {activeGame.Name}, please select the information for each match.";
                    PlayerGameData = await _context.PlayersByGames.Include(pg => pg.Player).Include(pg => pg.Character).Include(pg => pg.Character.Weapons).FirstOrDefaultAsync(pg => pg.GameId == activeGame.Id && pg.PlayerId == Guid.Parse(loginId));
                    GameMatches = await _context.Matches.Include(m => m.Character1).Include(m => m.Character2).Where(m => m.Character1 == PlayerGameData.Character || m.Character2 == PlayerGameData.Character).ToListAsync();

                    CharacterId = PlayerGameData.Character.Id;

                    SetCharacterImage(PlayerGameData.Character);

                    var userWeapons = _context.Weapons.Where(w => w.Character.Equals(PlayerGameData.Character));
                    ViewData["WeaponId"] = new SelectList(userWeapons, "Id", "Name");

                    foreach (var match in GameMatches)
                    {
                        SetCharacterImage(match.Character1);
                        SetCharacterImage(match.Character2);
                    }
                }
                else
                {
                    Message = "There are no games created yet for play";
                }
            }
            

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var match in GameMatches)
            {
                var matchDb = await _context.Matches.Include(m => m.Character1).Include(m => m.Character2).FirstOrDefaultAsync(m => m.Id == match.Id);
                var character = await _context.Characters.FindAsync(CharacterId);
                var weapon = await _context.Weapons.FindAsync(match.WeaponSelectedId);

                if(matchDb.Character1.Id == CharacterId)
                    matchDb.WeaponCharacter1 = weapon;
                else
                    matchDb.WeaponCharacter2 = weapon;

                _context.Entry(matchDb).State = EntityState.Modified;

                await _context.PlayersMatchBids.AddAsync(new PlayerMatchBid 
                { 
                    Id = Guid.NewGuid(),
                    BidAmount = match.BidAmonunt,
                    BidClass = match.BidClass,
                    Character = character, 
                    Match = matchDb
                });

                await _context.SaveChangesAsync();

                Message = "Matches saved!";
            }

            return RedirectToPage("./Index");
        }

        private void SetCharacterImage(Character? character)
        {
            if (character.CharacterType == CharacterType.Robot)
                character.ImageUrl = _assetService.GetRobotUrl(character.Name);
            else
            {
                //TODO add monster image
                character.ImageUrl = _assetService.GetMonsterUrl(character.Name);
            }
        }
    }
}