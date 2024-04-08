using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("RentBook")]
        public async Task<IActionResult> RentBook(Guid BookId, BookRentalDto bookRentalDto)
        {
            try
            {
                await _bookRentalService.RentBook(BookId,bookRentalDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return BadRequest(ex.Message);

            }
        }




    }

}
