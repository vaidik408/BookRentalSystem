using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services.Interface;

namespace BRS.Services
{
    public class BookRentalService : IBookRentalService

    {
        private readonly ILogger<BookRentalService> _logger;
        private readonly IBookRentalRepository _bookRentalRepository;
        private readonly IBookStatusRepository _bookStatusRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public BookRentalService
            (
            IBookRentalRepository bookRentalRepository,
            IBookStatusRepository statusRepository,
            IInventoryRepository inventoryRepository,
            ILogger<BookRentalService> logger
        )
        {
            _logger = logger;
            _bookRentalRepository = bookRentalRepository;
            _bookStatusRepository = statusRepository;
            _inventoryRepository = inventoryRepository;
        }

       public async Task RentBook(Guid BookId,BookRentalDto bookRentalDto)
        {
            try
            {
                await _bookRentalRepository.RentBook(BookId,bookRentalDto);
                await _bookStatusRepository.UpdateBookStatus(BookId);
                await _inventoryRepository.UpdateReservedBook();

            }
            catch ( Exception ex ) 
            {
                _logger.LogError(ex.Message);
                throw; 
            }
        }

    }
}
