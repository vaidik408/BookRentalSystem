using BRS.Repository.Interface;
using Quartz;

namespace BRS.Services
{
    public class RentalReminderJob : IJob   
    {
        private readonly IBookRentalRepository _bookRentalRepository;
        public RentalReminderJob(IBookRentalRepository bookRentalRepository)
        {
            _bookRentalRepository = bookRentalRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var overdueRentals = await _bookRentalRepository.GetOverdueRentalsAsync();

            foreach (var rental in overdueRentals)
            {
                await _bookRentalRepository.SendEmailNotificationForDueDate(rental.BookId);
                
            }
        }
    }
}
