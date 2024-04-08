using BRS.Data;
using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services;
using Microsoft.EntityFrameworkCore;

namespace BRS.Repository
{
    public class BookRentalRepository : IBookRentalRepository
    {

        private readonly BRSDbContext _context;
        private readonly ILogger<BookRepository> _logger;
        private readonly IBookStatusRepository _statusRepository;
        private readonly EmailService _emailService;
        private readonly IUserRepository _userRepository;
        public BookRentalRepository(
            BRSDbContext context,
            ILogger<BookRepository> logger,
            IBookStatusRepository bookStatusRepository,
            IUserRepository userRepository,
            EmailService emailService
            )
        {
            _context = context;
            _logger = logger;
            _statusRepository = bookStatusRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task RentBook(Guid BookId, BookRentalDto bookRentalDto)
        {
           
 
            var book = await _context.BookStatus.FirstOrDefaultAsync(b => b.BookId == BookId && b.Bk_Status == Enum.BookStatusEnum.Available);

            if (book != null)
            {
                var Rent = new BookRental()
                {
                    BookId = BookId,
                    UserId = bookRentalDto.UserId,
                    RentDate = bookRentalDto.RentDate,
                    ReturnDate = bookRentalDto.ReturnDate,
                };

                await _context.AddAsync( Rent );
                await SendBookRentendNotification(Rent.UserId);
            }
        }

        public async Task<string> SendBookRentendNotification(Guid UserId)
        {
            try { 
           
            var bookRental = await _context.BookRental
            .Include(br => br.Books)
            .FirstOrDefaultAsync(br => br.UserId == UserId);

            if (bookRental != null)
            {
                var rentDate = bookRental.RentDate.ToString("yyyy-MM-dd");
                var returnDate = bookRental.ReturnDate.ToString("yyyy-MM-dd");
                var bookTitle = bookRental.Books.Bk_Title;
                var bookNumber = bookRental.Books.Bk_Number;

                var recipientEmail = await _userRepository.GetAdminEmailbyUserId(UserId);
                var subject = "New Book Rented";
                var body = $"A new book has been rented.\n\n" +
                           $"Rent Date: {rentDate}\n" +
                           $"Return Date: {returnDate}\n" +
                           $"Book Title: {bookTitle}\n" +
                           $"Book Number: {bookNumber}";

                    await _emailService.SendEmailAsync(recipientEmail, subject, body);

                return "Email notification sent successfully.";
            }
            else
            {
                return "No book rental found for the user.";
            }
        }
    catch (Exception ex)
    {
        _logger.LogError($"Error occurred while sending book rental notification email: {ex.Message}");
        throw;
    }
}

    }
}
