﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly Data.ContextBizagiMatch _context;

        public IndexModel(Data.ContextBizagiMatch context)
        {
            _context = context;
        }

        public IList<PlayerMatchBid> PlayerMatchBid { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PlayersMatchBids != null)
            {
                PlayerMatchBid = await _context.PlayersMatchBids.ToListAsync();
            }
        }
    }
}
