using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppWeb
{
    public class EmpleadoModel : PageModel
    {
        [BindProperty]
        public int cedula { get; set; }

        [BindProperty]
        public string nombre { get; set; }

        [BindProperty]
        public string apellido1 { get; set; }

        [BindProperty]
        public string apellido2 { get; set; }

        [BindProperty]
        public int telefono { get; set; }

        public void OnGet()
        {
            // Aquí puedes poner código para la carga inicial de la página, si es necesario.
        }

        public IActionResult OnPostRegistrar_empleado()
        {

            return RedirectToPage("Confirmacion");
        }
    }
}