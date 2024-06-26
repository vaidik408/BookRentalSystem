﻿using BRS.Entities;
using BRS.Model;

namespace BRS.Repository.Interface
{
    public interface IBookRentalRepository
    {
        Task<string> SendBookRentendNotification(BookRental Rent, Guid UserId);
        Task RentBook(Guid BookId, BookRentalDto bookRentalDto);
        Task SendEmailNotificationForDueDate(Guid BookId);
        Task<List<BookRental>> GetOverdueRentalsAsync();
    }
}
