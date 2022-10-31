using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.PlayersByGames
{
    public class CreateModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public CreateModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GamesId"] = new SelectList(_context.Games, "Id", "Name");
        ViewData["PlayersId"] = new SelectList(_context.Players, "Id", "Name");
            ViewData["CharactersId"] = new SelectList(_context.Characters, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public PlayersByGame PlayersByGame { get; set; }
        [BindProperty]
        public Guid CharacterId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            _context.PlayersByGames.Add(PlayersByGame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
