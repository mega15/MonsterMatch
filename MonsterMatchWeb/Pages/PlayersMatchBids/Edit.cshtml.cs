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

namespace MonsterMatchWeb.Pages.PlayersMatchBids
{
    public class EditModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public EditModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        [BindProperty]
        public PlayerMatchBid PlayerMatchBid { get; set; } = default!;
        [BindProperty]
        public Guid MatchId { get; set; }
        [BindProperty]
        public Guid CharacterId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.PlayersMatchBids == null)
            {
                return NotFound();
            }

            ViewData["MatchId"] = new SelectList(_context.Matches, "Id", "Name");
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name");

            var playermatchbid = await _context.PlayersMatchBids.Include(b => b.Match).Include(b => b.Character).FirstOrDefaultAsync(m => m.Id == id);
            if (playermatchbid == null)
            {
                return NotFound();
            }
            PlayerMatchBid = playermatchbid;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PlayerMatchBid.Match = await _context.Matches.FindAsync(MatchId);
            PlayerMatchBid.Character = await _context.Characters.FindAsync(CharacterId);

            _context.Attach(PlayerMatchBid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerMatchBidExists(PlayerMatchBid.Id))
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

        private bool PlayerMatchBidExists(Guid id)
        {
            return  _context.PlayersMatchBids.Any(e => e.Id == id);
        }
    }
}
