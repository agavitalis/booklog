using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_RAZOR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_RAZOR.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(int id)
        {
            Book = _db.Books.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFormDb = _db.Books.Find(Book.Id);
                BookFormDb.Name = Book.Name;
                BookFormDb.ISBN = Book.ISBN;
                BookFormDb.Author = Book.Author;

                await _db.SaveChangesAsync();
                Message = "Book has been saved successfully";

                return RedirectToPage("index");
            }

            return RedirectToPage();
        }
    }
}