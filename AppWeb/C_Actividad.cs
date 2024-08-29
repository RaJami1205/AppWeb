using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppWeb
{
    public class ActividadModel : PageModel
    {
        [BindProperty]
        public int empleado { get; set; }

        [BindProperty]
        public DateTime fechaHora_inicio { get; set; }

        [BindProperty]
        public DateTime fechaHora_final { get; set; }

        [BindProperty]
        public DateTime hora { get; set; }

        [BindProperty]
        public string tipo { get; set; }

        [BindProperty]
        public string etapa { get; set; }

        public void OnGet()
        {
            // Aquí puedes poner código para la carga inicial de la página, si es necesario.
        }

        public IActionResult OnPostRegistrar_actividad()
        {

            return RedirectToPage("Confirmacion");
        }
    }
}