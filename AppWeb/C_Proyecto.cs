using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string NombrePortafolio { get; set; }

    [BindProperty]
    public string Nombre { get; set; }

    [BindProperty]
    public int Año { get; set; }

    [BindProperty]
    public string Trimestre { get; set; }

    [BindProperty]
    public string Descripcion { get; set; }

    [BindProperty]
    public string Tipo { get; set; }

    [BindProperty]
    public DateTime FechaInicio { get; set; }

    [BindProperty]
    public DateTime FechaCierre { get; set; }

    public void OnGet()
    {
        // Aquí puedes poner código para la carga inicial de la página, si es necesario.
    }

    public IActionResult OnPostRegistrar_proye()
    {
        
        return RedirectToPage("Confirmacion");
    }
}