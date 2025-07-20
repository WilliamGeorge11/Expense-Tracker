using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string type, decimal? amount, DateTime? date)
        {
            var transactions = _context.Transactions.Include(t => t.Category).AsQueryable();

            // Filter by type
            if (!string.IsNullOrEmpty(type))
            {
                transactions = transactions.Where(t => t.TransactionType == type);
            }

            // Filter by amount
            if (amount.HasValue)
            {
                transactions = transactions.Where(t => t.Amount == amount.Value);
            }

            // Filter by date
            if (date.HasValue)
            {
                transactions = transactions.Where(t => t.Date.Date == date.Value.Date);
            }

            return View(await transactions.ToListAsync());
        }

    }
}
