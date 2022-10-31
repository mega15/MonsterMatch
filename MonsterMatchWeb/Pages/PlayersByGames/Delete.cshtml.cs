using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.PlayersByGames
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public DeleteModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        [BindProperty]
      public PlayersByGame PlayersByGame { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.PlayersByGames == null)
            {
                return NotFound();
            }

            var playersbygame = await _context.PlayersByGames.FirstOrDefaultAsync(m => m.PlayerId == id);

            if (playersbygame == null)
            {
                return NotFound();
            }
            else
            {
                PlayersByGame = playersbygame;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.PlayersByGames == null)
            {
                return NotFound();
            }
            var playersbygame = await _context.PlayersByGames.FindAsync(id);

            if (playersbygame != null)
            {
                PlayersByGame = playersbygame;
                _context.PlayersByGames.Remove(PlayersByGame);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
