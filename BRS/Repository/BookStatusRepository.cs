using BRS.Data;
using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BRS.Repository
{
    public class BookStatusRepository : IBookStatusRepository
    {
        private readonly BRSDbContext _context;
        private readonly ILogger<BookStatusRepository> _logger;
        public BookStatusRepository(
            BRSDbContext context,
            ILogger<BookStatusRepository> logger
            )
        {
            _context = context;
            _logger = logger;
        }


        public async Task AddBookStatus(Books book)
        {
            var status = new BookStatus()
            {
                StatusId = Guid.NewGuid(),
                BookId = book.BookId,
                Bk_Status = Enum.BookStatusEnum.Available
            };
            await _context.BookStatus.AddAsync(status);
        }

        public async Task<bool> UpdateBookStatus(Guid BookId)
        {
            try
            {
                var userStatus = await _context.BookStatus.FirstOrDefaultAsync(u => u.BookId == BookId);

                if (userStatus != null)
                {
                    userStatus.Bk_Status = Enum.BookStatusEnum.Reserved;

                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occur While Updating Status{ex.Message}");
                throw;
            }
        }

    }
}
