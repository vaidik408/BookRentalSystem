using System;
using System.ComponentModel.DataAnnotations;

namespace BRS.Model
{
    public class BookRentalDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Rent date is required")]
        [DataType(DataType.DateTime)]
        public DateTime RentDate = DateTime.Today;

        [Required(ErrorMessage = "Return date is required")]
        [DataType(DataType.DateTime)]
        public DateTime ReturnDate = DateTime.Today.AddDays(15);
    }
}
