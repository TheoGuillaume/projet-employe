using Crud.Data;
using Crud.Models.Domain;
using Crud.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crud.Pages.Postes
 {
    public class AddModel : PageModel
    {
         private readonly RazorPageDbContext dbContext;
        public AddModel(RazorPageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public AddPosteViewModel AddPosteRequest {get; set;}
        public void OnPost()
        {
            try
            {
                if(AddPosteRequest.NamePoste == "" || AddPosteRequest.NamePoste == null) {
                     throw new InvalidOperationException("Nom poste obligatoire");
                }
                var poste = new Poste
                {
                    NamePoste = AddPosteRequest.NamePoste
                };

                dbContext.Poste.Add(poste);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }
        }
    }
 }