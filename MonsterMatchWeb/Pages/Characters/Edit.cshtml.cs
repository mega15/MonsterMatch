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

namespace MonsterMatchWeb.Pages.Characters
{
    public class EditModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public EditModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        [BindProperty]
        public Character Character { get; set; } = default!;
        [BindProperty]
        public Guid PlayerId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Characters == null)
            {
                return NotFound();
            }

            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");

            var character =  await _context.Characters.FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }
            Character = character;
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

            Character.Player = await _context.Players.FindAsync(PlayerId);
            _context.Attach(Character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(Character.Id))
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

        private bool CharacterExists(Guid id)
        {
          return _context.Characters.Any(e => e.Id == id);
        }
    }
}
