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
        private readonly ILogger<BookRentalRepository> _logger;
        private readonly IBookStatusRepository _statusRepository;
        private readonly EmailService _emailService;
        private readonly IUserRepository _userRepository;

        public BookRentalRepository(
            BRSDbContext context,
            ILogger<BookRentalRepository> logger,
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
            try
            {
                var book = await _context.BookStatus.FirstOrDefaultAsync(b => b.BookId == BookId && b.Bk_Status == Enum.BookStatusEnum.Available);

                if (book != null)
                {
                    var rent = new BookRental()
                    {
                        BookId = BookId,
                        UserId = bookRentalDto.UserId,
                        RentDate = bookRentalDto.RentDate, 
                        ReturnDate = bookRentalDto.ReturnDate, 
                    };

                    await SendBookRentendNotification(rent, bookRentalDto.UserId);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException("The book is not available for rental.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while renting the book.");
                throw;
            }
        }

        public async Task<string> SendBookRentendNotification(BookRental rent, Guid UserId)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == rent.BookId);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);

                if (book != null && user != null)
                {
                    var rentDate = rent.RentDate.ToString("yyyy-MM-dd");
                    var returnDate = rent.ReturnDate.ToString("yyyy-MM-dd");
                    var bookTitle = book.Bk_Title;
                    var bookNumber = book.Bk_Number;
                    var userName = user.UserName;

                    var recipientEmail = await _userRepository.GetAdminEmailbyUserId();
                    var subject = "New Book Rented";
                    var body = $"A new book has been rented.\n\n" +
                               $"Rent Date: {rentDate}\n" +
                               $"Return Date: {returnDate}\n" +
                               $"Book Title: {bookTitle}\n" +
                               $"Book Number: {bookNumber}\n" +
                               $"Customer Name: {userName}\n";

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
                _logger.LogError(ex, "Error occurred while sending book rental notification email.");
                throw;
            }
        }

        public async Task SendEmailNotificationForDueDate(Guid BookId)
        {
            try
            {
                var bookRental = await _context.BookRental.FirstOrDefaultAsync(b => b.BookId == BookId);
                var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == BookId);
                var UserId = bookRental.UserId;

                var rentDate = bookRental.RentDate.ToString("yyyy-MM-dd");
                var returnDate = bookRental.ReturnDate.ToString("yyyy-MM-dd");
                var bookTitle = book.Bk_Title;
                var bookNumber = book.Bk_Number;

                var recipientEmail = await _userRepository.GetCustomerEmailbyUserId(UserId);
                var subject = "Book Return Due Date";
                var body = $"Please return the book.\n\n" +
                           $"Book Title: {bookTitle}\n" +
                           $"Book Number: {bookNumber}\n" +
                           $"Rent Date: {rentDate}\n" +
                           $"Return Date: {returnDate}\n";

                await _emailService.SendEmailAsync(recipientEmail, subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending due date notification email.");
                throw;
            }
        }

        public async Task<List<BookRental>> GetOverdueRentalsAsync()
        {
            try
            {
                return await _context.BookRental
                    .Where(b => b.ReturnDate == DateTime.Today || b.ReturnDate < DateTime.Today)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching overdue rentals.");
                throw;
            }
        }

    }
}
