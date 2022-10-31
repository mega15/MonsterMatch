using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.PlayersByGames
{
    public class EditModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public EditModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        [BindProperty]
        public PlayersByGame PlayersByGame { get; set; } = default!;
        [BindProperty]
        public Guid CharacterId { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.PlayersByGames == null)
            {
                return NotFound();
            }

            string[] keys = id.Split(" ");

            var playersbygame = await _context.PlayersByGames.Include(p => p.Player).Include(p => p.Game).Include(p => p.Character).FirstOrDefaultAsync(m => m.PlayerId == Guid.Parse(keys[0]) && m.GameId == Guid.Parse(keys[1]));
            if (playersbygame == null)
            {
                return NotFound();
            }
            PlayersByGame = playersbygame;
            ViewData["GamesId"] = new SelectList(_context.Games, "Id", "Name");
            ViewData["PlayersId"] = new SelectList(_context.Players, "Id", "Name");
            ViewData["CharactersId"] = new SelectList(_context.Characters, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("PlayersByGame.Player");
            ModelState.Remove("PlayersByGame.Game");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PlayersByGame.Player = await _context.Players.FindAsync(PlayersByGame.PlayerId);
            PlayersByGame.Game = await _context.Games.FindAsync(PlayersByGame.GameId);
            PlayersByGame.Character = await _context.Characters.FindAsync(CharacterId);
            _context.Attach(PlayersByGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersByGameExists(PlayersByGame.PlayerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlayersByGameExists(Guid id)
        {
            return _context.PlayersByGames.Any(e => e.PlayerId == id);
        }
    }
}
