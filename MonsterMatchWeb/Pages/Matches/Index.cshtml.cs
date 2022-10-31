using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Model;

namespace MonsterMatchWeb.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public IndexModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        public IList<Match> Match { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Matches != null)
            {
                Match = await _context.Matches.Include(m => m.Character1).Include(m => m.Character2).Include(m => m.Winner).Include(m => m.WeaponCharacter1).Include(m => m.WeaponCharacter2).ToListAsync();
            }
        }
    }
}
