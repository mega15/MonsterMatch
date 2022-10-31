using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.PlayersMatchBids
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public DeleteModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        [BindProperty]
      public PlayerMatchBid PlayerMatchBid { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.PlayersMatchBids == null)
            {
                return NotFound();
            }

            var playermatchbid = await _context.PlayersMatchBids.FirstOrDefaultAsync(m => m.Id == id);

            if (playermatchbid == null)
            {
                return NotFound();
            }
            else
            {
                PlayerMatchBid = playermatchbid;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.PlayersMatchBids == null)
            {
                return NotFound();
            }
            var playermatchbid = await _context.PlayersMatchBids.FindAsync(id);

            if (playermatchbid != null)
            {
                PlayerMatchBid = playermatchbid;
                _context.PlayersMatchBids.Remove(PlayerMatchBid);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
