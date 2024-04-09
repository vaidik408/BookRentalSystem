using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookRentalController : Controller
    {
        private readonly IBookRentalService _bookRentalService;
        private readonly ILogger<BookRentalController> _logger;

        public BookRentalController(
            IBookRentalService bookRentalService,
            ILogger<BookRentalController> logger
        )
        {
            _logger = logger;
            _bookRentalService = bookRentalService;
        }

        [Authorize(Roles = "1,2")]
        [HttpPost("RentBook/{BookId}")]
        public async Task<IActionResult> RentBook(Guid BookId, [FromBody] BookRentalDto bookRentalDto)
        {
            try
            {
                if (bookRentalDto == null)
                {
                    return BadRequest("Book rental data is required.");
                }

                await _bookRentalService.RentBook(BookId, bookRentalDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while renting book: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
