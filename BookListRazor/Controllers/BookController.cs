using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.BookControllers
{
    [Route("/Api/book")]
    [ApiController]
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() }); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.ID == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Livro não encontrado, deleção não efetuada" });
            }

            _db.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success =true, message = "Ok, Deleção realizada com sucesso" });
        }
        
    }
}