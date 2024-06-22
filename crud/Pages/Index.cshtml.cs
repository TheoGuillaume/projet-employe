using Azure;
using Crud.Data;
using Crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace crud.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    [BindProperty]
    public string identifiant { get; set; }
    [BindProperty]
    public string mdp { get; set; }
    [BindProperty]
    public string message { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
    public IActionResult OnPost(string identifiant, string mdp)
    {
        // Vérification des identifiants (c'est généralement mieux de ne pas afficher les mots de passe, mais pour l'exemple, nous allons garder cette ligne)
        Console.WriteLine(mdp);

        // Vérification des identifiants
        if (identifiant == "admin" && mdp == "admin")
        {
            // Redirection vers l'action Index du contrôleur Home
            return RedirectToPage("/Employees/List");
        }
        else
        {
            // En cas d'authentification échouée, vous pouvez rediriger vers une autre page, afficher un message d'erreur, etc.
            // Par exemple, rediriger vers une page de connexion avec un message d'erreur
            message= "Identifiant ou mot de passe incorrect.";
            return RedirectToPage("/index");
        }
    }

}
