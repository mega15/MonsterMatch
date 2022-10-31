using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.PlayersMatchBids
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
            ViewData["MatchId"] = new SelectList(_context.Matches, "Id", "Name");
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public PlayerMatchBid PlayerMatchBid { get; set; }
        [BindProperty]
        public Guid MatchId { get; set; }
        [BindProperty]
        public Guid CharacterId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("PlayerMatchBid.Id");
            ModelState.Remove("PlayerMatchBid.Match");
            ModelState.Remove("PlayerMatchBid.Character");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PlayerMatchBid.Id = Guid.NewGuid();
            PlayerMatchBid.Match = await _context.Matches.FindAsync(MatchId);
            PlayerMatchBid.Character = await _context.Characters.FindAsync(CharacterId);

            _context.PlayersMatchBids.Add(PlayerMatchBid);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
