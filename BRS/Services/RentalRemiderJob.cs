using BRS.Repository.Interface;
using Quartz;

namespace BRS.Services
{
    public class RentalReminderJob : IJob   
    {
        private readonly IBookRentalRepository _bookRentalRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public RentalReminderJob(IBookRentalRepository bookRentalRepository,IInventoryRepository inventoryRepository)
        {
            _bookRentalRepository = bookRentalRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var overdueRentals = await _bookRentalRepository.GetOverdueRentalsAsync();

            foreach (var rental in overdueRentals)
            {
                await _bookRentalRepository.SendEmailNotificationForDueDate(rental.BookId);
                await _inventoryRepository.UpdateAvailableBook();
            }
        }
    }
}
