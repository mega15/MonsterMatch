using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.Weapons
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public DeleteModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        [BindProperty]
      public Weapon Weapon { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Weapons == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons.FirstOrDefaultAsync(m => m.Id == id);

            if (weapon == null)
            {
                return NotFound();
            }
            else 
            {
                Weapon = weapon;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Weapons == null)
            {
                return NotFound();
            }
            var weapon = await _context.Weapons.FindAsync(id);

            if (weapon != null)
            {
                Weapon = weapon;
                _context.Weapons.Remove(Weapon);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
