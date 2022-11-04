using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Model;
using BusinessLogic;
using BusinessLogic.Web.Contracts;

namespace MonsterMatchWeb.Pages.Players
{
    public class CreateModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;
        private readonly ISecurityService _securityService;

        public CreateModel(Data.ContextBizagiMatch context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Player Player { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            Player.Pass = _securityService.HashString(Player.Pass);
            _context.Players.Add(Player);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
