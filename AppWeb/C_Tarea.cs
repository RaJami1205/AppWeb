using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppWeb
{
    public class TareaModel : PageModel
    {
        [BindProperty]
        public string nombre { get; set; }

        [BindProperty]
        public string descripcion { get; set; }

        [BindProperty]
        public DateTime hora { get; set; }

        [BindProperty]
        public string tipo { get; set; }

        public void OnGet()
        {
            // Aquí puedes poner código para la carga inicial de la página, si es necesario.
        }

        public IActionResult OnPostRegistrar_tarea()
        {

            return RedirectToPage("Confirmacion");
        }
    }
}