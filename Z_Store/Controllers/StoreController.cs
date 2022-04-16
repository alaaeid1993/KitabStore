using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Z_Store.Models;

namespace Z_Store.Controllers
{
    [Authorize]
    public class StoreController : Controller

    {
        private readonly ZDBContext _context;
        //internal readonly object Books;
        public StoreController(ZDBContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string word)
        {
            var searach = _context.Books.Select(b => b);
            if (!string.IsNullOrEmpty(word))
            {
                searach = searach.Where(b => b.Title.Contains(word) || b.Author.Contains(word));
            }

            return View(await searach.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

    }
}
