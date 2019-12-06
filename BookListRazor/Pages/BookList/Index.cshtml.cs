using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        //Injeção de Dependencia
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Colocar as informações em uma coleção
        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync(); 
        }

        public async Task<IActionResult> OnPostDelete(int? id)
        {
            var BookDelete = await _db.Book.FindAsync(id);
            if (BookDelete == null)
            {
                return NotFound();
            }
            else
            {
                 _db.Remove(BookDelete);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }


        }
    }
}