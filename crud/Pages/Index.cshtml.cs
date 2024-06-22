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
        // V�rification des identifiants (c'est g�n�ralement mieux de ne pas afficher les mots de passe, mais pour l'exemple, nous allons garder cette ligne)
        Console.WriteLine(mdp);

        // V�rification des identifiants
        if (identifiant == "admin" && mdp == "admin")
        {
            // Redirection vers l'action Index du contr�leur Home
            return RedirectToPage("/Employees/List");
        }
        else
        {
            // En cas d'authentification �chou�e, vous pouvez rediriger vers une autre page, afficher un message d'erreur, etc.
            // Par exemple, rediriger vers une page de connexion avec un message d'erreur
            message= "Identifiant ou mot de passe incorrect.";
            return RedirectToPage("/index");
        }
    }

}
