using Crud.Data;
using Crud.Models.Domain;
using Crud.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crud.Pages.Employees
{
    public class AddModel : PageModel
    {
        private readonly RazorPageDbContext dbContext;
        public List<Poste> Postes { get; set; }
        public AddModel(RazorPageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public AddEmployeeViewModel AddEmployeeRequest { get; set; }
        private readonly ILogger<AddModel> logger;
        public void OnGet()
        {
            Postes = dbContext.Poste.ToList();
        }

        public void OnPost()
        {
            try
            {
                if (dbContext.Employees.Any(e => e.Email == AddEmployeeRequest.Email))
                {
                    throw new InvalidOperationException("Email already exist");
                }

                var employeeDomainModel = new Employee
                {
                    Name = AddEmployeeRequest.Name,
                    Email = AddEmployeeRequest.Email,
                    Salary = AddEmployeeRequest.Salary,
                    DateOfBirth = AddEmployeeRequest.DateOfBirth,
                    PosteId = AddEmployeeRequest.PosteId.GetValueOrDefault(),
                    Department = AddEmployeeRequest.Department
                };

                dbContext.Employees.Add(employeeDomainModel);
                dbContext.SaveChanges();

                ViewData["Message"] = "Employee created successfully!";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }

        }
    }
}