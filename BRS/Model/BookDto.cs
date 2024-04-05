using BRS.Entities;

namespace BRS.Model
{
    public class BookDto
    {
        public Guid StatusId { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Number { get; set; }
        public string Bk_Name { get; set; }
        public string Bk_Description { get; set; }
        public string Bk_Author { get; set; }
    }

}
