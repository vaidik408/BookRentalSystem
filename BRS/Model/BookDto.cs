using System.ComponentModel.DataAnnotations;

namespace BRS.Model
{
    public class BookDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Bk_Title { get; set; }

        [Required(ErrorMessage = "Number is required")]
        public string Bk_Number { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Bk_Description { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Author can only contain letters and spaces")]
        public string Bk_Author { get; set; }
    }
}
