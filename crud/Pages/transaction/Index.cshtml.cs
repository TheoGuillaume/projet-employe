using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using crud.Models.Domain;

namespace crud.Pages.transaction
{
    public class IndexModel : PageModel
    {
        private readonly Crud.Data.RazorPageDbContext _context;

        public IndexModel(Crud.Data.RazorPageDbContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Transaction = await _context.Transactions
                .Include(t => t.Employe).ToListAsync();
        }
    }
}
