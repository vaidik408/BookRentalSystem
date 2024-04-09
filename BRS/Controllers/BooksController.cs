using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(
            IBookService bookService,
            IBookRepository bookRepository,
            ILogger<BooksController> logger
        )
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _bookService = bookService;
        }

        [Authorize(Roles = "1")]
        [HttpPost("AddBooks")]
        public async Task<IActionResult> AddBooks(BookDto bookDto)
        {
            try
            {
                if (bookDto == null)
                {
                    return BadRequest("Book data is required.");
                }

                await _bookService.AddBook(bookDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding books: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBooks(int page = 1, int pageSize = 10, string sortBy = "bookid", string search = "", bool descending = false)
        {
            try
            {
                var query = _bookService.GetAllBooks();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(c => c.Bk_Title.Contains(search));
                }

                query = _bookRepository.ApplySorting(query, sortBy, descending);

                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                query = query.Skip((page - 1) * pageSize).Take(pageSize);

                var result = new
                {
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Page = page,
                    PageSize = pageSize,
                    Books = query.ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching books: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
            try
            {
                var result = await _bookService.DeleteBook(bookId);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the book.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
