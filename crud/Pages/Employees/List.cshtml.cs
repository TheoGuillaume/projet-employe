using System.Linq;
using System.Reflection.Metadata;
using Crud.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
        public int PageSize { get; set; } = 5; // Number of items per page
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }
        public ListModel(RazorPageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> OnPostExportPdfAsync()
        {
            var employees = await dbContext.Employees.Include(e => e.Poste).ToListAsync();
            var pdfBytes = GeneratePdf(employees);

            return File(pdfBytes, "application/pdf", "Employees.pdf");
        }
        public byte[] GeneratePdf(List<Employee> products)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            using (var memoryStream = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, memoryStream).CloseStream = false;
                doc.Open();

                // Add a title
                doc.Add(new Paragraph("Product List"));

                // Add a table
                PdfPTable table = new PdfPTable(8); // Assuming 3 columns: Id, Name, Price
                table.AddCell("ID");
                table.AddCell("Name");
                table.AddCell("email");
                table.AddCell("Solde");
                table.AddCell("Salaire");
                table.AddCell("Anniversaire");
                table.AddCell("Poste");
                table.AddCell("Departement");

                // Add rows to the table
                foreach (var product in products)
                {
                    table.AddCell(product.Id.ToString());
                    table.AddCell(product.Name);
                    table.AddCell(product.Email);
                    table.AddCell(product.Solde.ToString());
                    table.AddCell(product.Salary.ToString());
                    table.AddCell(product.DateOfBirth.ToString());
                    table.AddCell(product.Poste.NamePoste);
                    table.AddCell(product.Department);
                }

                doc.Add(table);
                doc.Close();

                // Save the PDF to a file
                //File.WriteAllBytes(filePath, memoryStream.ToArray());
                return memoryStream.ToArray();
            }
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