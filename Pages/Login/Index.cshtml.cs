using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiAgendaWeb.Pages.Login 
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (Email == "admin@1.com" && Password == "admin123")
            {
                HttpContext.Session.SetString("Usuario", "Admin");
                return RedirectToPage("/Home/Index");
            }

            ViewData["ErrorMessage"] = "Credenciales incorrectas.";
            return Page();
        }
    }
}