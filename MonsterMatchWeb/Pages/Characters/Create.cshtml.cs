using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.Characters
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Character Character { get; set; }
        [BindProperty]
        public Guid PlayerId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Character.Player");
            ModelState.Remove("Character.ImageUrl");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Character.Id = Guid.NewGuid();
            Character.Player = await _context.Players.FindAsync(PlayerId);
            _context.Characters.Add(Character);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
