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

namespace MonsterMatchWeb.Pages.Matches
{
    public class EditModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public EditModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        [BindProperty]
        public Match Match { get; set; } = default!;
        [BindProperty]
        public Guid Character1Id { get; set; }
        [BindProperty]
        public Guid Character2Id { get; set; }
        [BindProperty]
        public Guid WinnerId { get; set; }
        [BindProperty]
        public Guid Weapon1Id { get; set; }
        [BindProperty]
        public Guid Weapon2Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters.ToListAsync();
            characters.Insert(0, new Character() { Id = Guid.Empty });

            ViewData["WeaponId"] = new SelectList(_context.Weapons, "Id", "Name");
            ViewData["CharacterId"] = new SelectList(characters, "Id", "Name");

            var match = await _context.Matches.Include(m => m.Character1).Include(m => m.Character2).Include(m => m.WeaponCharacter1).Include(m => m.WeaponCharacter2).Include(m => m.Winner).FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }
            Match = match;
            Character1Id = Match.Character1.Id;
            Character2Id = Match.Character2.Id;
            Weapon1Id = Match.WeaponCharacter1.Id;
            Weapon2Id = Match.WeaponCharacter2.Id;
            WinnerId = Match.Winner != null ? Match.Winner.Id : Guid.Empty;

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

            Match.Character1 = await _context.Characters.FindAsync(Character1Id);
            Match.Character2 = await _context.Characters.FindAsync(Character2Id);
            Match.WeaponCharacter1 = await _context.Weapons.FindAsync(Weapon1Id);
            Match.WeaponCharacter2 = await _context.Weapons.FindAsync(Weapon2Id);
            Match.Winner = WinnerId != Guid.Empty ? await _context.Characters.FindAsync(WinnerId) : null;

            _context.Attach(Match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(Match.Id))
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

        private bool MatchExists(Guid id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
    }
}
