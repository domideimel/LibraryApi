using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryContext _db;

        public LibraryController(LibraryContext context)
        {
            _db = context;
        }

        [HttpPost]
        public IActionResult CreateBook(Book? book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();

            if (book != null) return CreatedAtAction("GetBook", new {id = book.Id}, book);
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetBook(int id)
        {
            Book? book = _db.Books.SingleOrDefault(b => b != null && b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        
        [HttpPut]
        public IActionResult UpdateBook(Book? book)
        {
            Book? bookDb = _db.Books.SingleOrDefault(b => book != null && b != null && b.Id == book.Id);
            if (bookDb == null)
            {
                return NotFound();
            }
            
            bookDb.Title = book?.Title;
            bookDb.Author = book?.Author;
            bookDb.PageCount = ((int) book?.PageCount)!;
            
            _db.SaveChanges();

            return Ok(book);
        }
    }
}