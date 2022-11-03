using BusinessLogic;
using BusinessLogin.Web.Contracts;
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
        private readonly ISecurityService _securityService;

        public LoginModel(ContextBizagiMatch context, IConfiguration configuration, ISecurityService securityService)
        {
            _context = context;
            _configuration = configuration;
            _securityService = securityService;
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

            if ((UserName == "sa" && Password == _configuration["AdminPassword"])  || (player != null && _securityService.isEqualPassword(Password, player.Pass)))
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
