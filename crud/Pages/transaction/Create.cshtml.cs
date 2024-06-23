using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crud.Data;
using crud.Models.Domain;
using Crud.Models.Domain;

namespace crud.Pages.transaction
{
    public class CreateModel : PageModel
    {
        private readonly Crud.Data.RazorPageDbContext _context;

        public CreateModel(Crud.Data.RazorPageDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emp = await _context.Employees.FindAsync(Transaction.EmployeeId);
            if (emp.Solde >= Transaction.NombreJour)
            {
                if (Transaction.Id == Guid.Empty)
                {
                    Transaction.Id = Guid.NewGuid();
                }
                var transaction = new Transaction
                {
                    Id = Transaction.Id,
                    Motif = Transaction.Motif,
                    NombreJour = Transaction.NombreJour,
                    EmployeeId = Transaction.EmployeeId,
                    Status = "0",
                    Employe = Transaction.Employe
                };
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
              return RedirectToPage("./Index");
        }
    }
}
