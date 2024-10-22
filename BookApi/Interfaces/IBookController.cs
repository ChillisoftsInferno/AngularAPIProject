using BookApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Interfaces;

public interface IBookController
{
    public ActionResult<IEnumerable<Book>> GetBooks();
    // public ActionResult<Book> GetBookByFilter(string filters);
    // public ActionResult<Book> GetBookByMultipleFilters(params string[] filters);
}
