using System.Linq;
using Crud.Data;
using Crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crud.Pages.Employees
{
    public class ListModel : PageModel
    {
        private readonly RazorPageDbContext dbContext;
        public List<Employee> Employees { get; set; }
        // Parameters for pagination
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 2; // Number of items per page
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }
        public ListModel(RazorPageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int currentPage = 1, string searchTerm = null)
        {
            try
            {
                SearchTerm = searchTerm;

                // Create a queryable for employees
                var query = dbContext.Employees.AsQueryable();

                // Apply search criteria
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    var searchTermUpper = SearchTerm.ToUpper();
                    query = query.Where(e =>
                        e.Name.ToUpper().Contains(searchTermUpper) ||
                        e.Salary.ToString().ToUpper().Contains(searchTermUpper) ||
                        e.Department.ToUpper().Contains(searchTermUpper)
                    );
                }

                // Calculate the total number of employees
                var totalEmployees = query.Count();

                // Calculate the total number of pages
                TotalPages = (int)Math.Ceiling((double)totalEmployees / PageSize);

                // Validate the current page to ensure it's within the valid range
                CurrentPage = currentPage < 1 ? 1 : (currentPage > TotalPages ? TotalPages : currentPage);

                // Retrieve the employees for the current page using Skip and Take
                Employees = query
                .Include(e => e.Poste)  // Inclure les informations du poste
                .OrderBy(e => e.Id)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }
            // Update the search term

        }

        public IActionResult OnPostDelete(Guid id)
        {
            var employeeToDelete = dbContext.Employees.Find(id);

            if (employeeToDelete != null)
            {
                dbContext.Employees.Remove(employeeToDelete);
                dbContext.SaveChanges();
            }

            // Redirect to the same page with the current page number
            return RedirectToPage("./List", new { currentPage = CurrentPage });
        }
    }
}