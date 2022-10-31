using BusinessLogic;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace MonsterMatchWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ContextBizagiMatch _context;
        private readonly IConfiguration _configuration;

        public LoginModel(ContextBizagiMatch context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Error { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Error");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var player = await _context.Players.FirstOrDefaultAsync(p => p.UserName.Equals(UserName));

            if ((UserName == "sa" && Password == _configuration["AdminPassword"])  || (player != null && Utils.isEqualPassword(Password, player.Pass)))
            {
                HttpContext.Session.SetString("LoginId", UserName == "sa" ? "Admin" : player.Id.ToString());

                return RedirectToPage("./Index");
            }
            else
            {
                Error = "Invalid user or incorrect password";
                return Page();
            }
        }
    }
}
