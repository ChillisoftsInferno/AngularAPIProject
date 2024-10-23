using BookApi.Interfaces;
using BookApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

// http://localhost:5173/api/books
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase, IBookController
{
    private readonly Book[] _books = new Book[]
    {
        new Book { Id = 1, Author = "Author One", Title = "Book One" },
        new Book { Id = 2, Author = "Author Two", Title = "Book Two" },
        new Book { Id = 3, Author = "Author Three", Title = "Book Three" },
    };
    
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_books);
    }
    
    //
    // [HttpGet]
    // public ActionResult<Book> GetBookByFilter(string filter)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpGet]
    // public ActionResult<Book> GetBookByMultipleFilters(params string[] filters)
    // {
    //     throw new NotImplementedException();
    // }
}
