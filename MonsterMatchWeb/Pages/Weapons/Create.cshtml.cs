using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MonsterMatchWeb.Pages.Weapons
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
            ViewData["CharactersId"] = new SelectList(_context.Characters, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Weapon Weapon { get; set; }
        [BindProperty]
        public Guid CharacerId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Weapon.Id");
            ModelState.Remove("Weapon.Character");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Weapon.Id = Guid.NewGuid();
            Weapon.Character = await _context.Characters.FindAsync(CharacerId);

            _context.Weapons.Add(Weapon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
