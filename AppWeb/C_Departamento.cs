using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppWeb
{
    public class PrivacyModel : PageModel
    {
        [BindProperty]
        public int codigo { get; set; }

        [BindProperty]
        public string nombre { get; set; }

        [BindProperty]
        public int jefe { get; set; }

        public void OnGet()
        {
            // Aquí puedes poner código para la carga inicial de la página, si es necesario.
        }

        public IActionResult OnPostRegistrar_depa()
        {

            return RedirectToPage("Confirmacion");
        }
    }
}