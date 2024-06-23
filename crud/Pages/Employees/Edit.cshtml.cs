using Crud.Data;
using Crud.Models.Domain;
using Crud.Models.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crud.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly RazorPageDbContext dbContext;

        [BindProperty]
        public EditEmployeeViewModel EditEmployeeViewModel { get; set; }

        public List<SelectListItem> Postes { get; set; }
        public Guid SelectedPosteId { get; set; }
        public EditModel(RazorPageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(Guid id)
        {
            Postes = dbContext.Poste
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.NamePoste })
            .ToList();

            var employee = dbContext.Employees
                .Include(e => e.Poste)
                .FirstOrDefault(e => e.Id == id);

            if (employee != null)
            {
                //Convert Domain Model to View Model
                EditEmployeeViewModel = new EditEmployeeViewModel()
                {
                    Id = id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    PosteId = employee.PosteId 
                };

                SelectedPosteId = employee.PosteId;
            }
        }

        public IActionResult OnPost()
        {
            if (EditEmployeeViewModel != null)
            {
                var existingEmployee = dbContext.Employees.Find(EditEmployeeViewModel.Id);
                if (existingEmployee != null)
                {
                    //Convert View Model to Domain
                    existingEmployee.Name = EditEmployeeViewModel.Name;
                    existingEmployee.Email = EditEmployeeViewModel.Email;
                    existingEmployee.DateOfBirth = EditEmployeeViewModel.DateOfBirth;
                    existingEmployee.Salary = EditEmployeeViewModel.Salary;
                    existingEmployee.Department = EditEmployeeViewModel.Department;
                    existingEmployee.PosteId = EditEmployeeViewModel.PosteId;
                    existingEmployee.Solde = EditEmployeeViewModel.Solde;
                    dbContext.SaveChanges();
                     return RedirectToPage("/Employees/List"); 
                }

            }
             return Page();
        }
    }
}