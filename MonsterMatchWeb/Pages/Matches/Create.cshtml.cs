using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Model;
using Microsoft.EntityFrameworkCore;

namespace MonsterMatchWeb.Pages.Matches
{
    public class CreateModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public CreateModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var characters = await _context.Characters.ToListAsync();
            characters.Insert(0, new Character() { Id = Guid.Empty });

            ViewData["WeaponId"] = new SelectList(_context.Weapons, "Id", "Name");
            ViewData["CharacterId"] = new SelectList(characters, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Match Match { get; set; }
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


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Match.Id");
            ModelState.Remove("Match.Winner");
            ModelState.Remove("Match.WeaponCharacter1Id");
            ModelState.Remove("Match.WeaponCharacter2Id");
            ModelState.Remove("Match.BidAmonunt");
            ModelState.Remove("Match.BidClass");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Match.Id = Guid.NewGuid();
            Match.Character1 = await _context.Characters.FindAsync(Character1Id);
            Match.Character2 = await _context.Characters.FindAsync(Character2Id);
            Match.WeaponCharacter1 = await _context.Weapons.FindAsync(Weapon1Id);
            Match.WeaponCharacter2 = await _context.Weapons.FindAsync(Weapon2Id);

            _context.Matches.Add(Match);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
