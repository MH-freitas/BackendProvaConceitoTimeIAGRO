using Library.Domain.Intefaces.Services;
using Library.Domain.Services;
using Library.Shared.Dtos;
using Librery.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Library.App.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookFilterDto filterDto)
        {
            var books = await _bookService.GetAllBooksAsync(filterDto);
            if (books == null || !books.Any())
            {
                return NotFound("No books found.");
            }
            return Ok(books);
        }

        [HttpPost("precify")]
        public IActionResult PrecifyBooks([FromBody] PrecifierBooksDto ids)
        {
            var precifiedBooks = _bookService.Precifier(ids);
            if (precifiedBooks == null || !precifiedBooks.Any())
            {
                return NotFound("No books found for the provided IDs.");
            }
            return Ok(precifiedBooks);
        }

        [HttpPost("precify/{id:int}")]
        public IActionResult GetPrecifiedBook(int id)
        {
            var result = _bookService.PrecifierOneBook(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


    }
}