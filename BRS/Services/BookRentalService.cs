using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services.Interface;

namespace BRS.Services
{
    public class BookRentalService : IBookRentalService

    {
        private readonly IBookRentalRepository _bookRentalRepository;
        private readonly ILogger<BookRentalService> _logger;
        private readonly IBookStatusRepository _bookStatusRepository;

        public BookRentalService
            (
            IBookRentalRepository bookRentalRepository,
            IBookStatusRepository statusRepository,
            ILogger<BookRentalService> logger
        )
        {
            _bookRentalRepository = bookRentalRepository;
            _logger = logger;
            _bookStatusRepository = statusRepository;
        }

       public async Task RentBook(Guid BookId,BookRentalDto bookRentalDto)
        {
            try
            {
                await _bookRentalRepository.RentBook(BookId,bookRentalDto);
                await _bookStatusRepository.UpdateBookStatus(BookId);
            }
            catch ( Exception ex ) 
            {
                _logger.LogError(ex.Message);
                throw; 
            }
        }
    }
}
