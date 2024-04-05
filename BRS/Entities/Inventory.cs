using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BRS.Entities
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InventoryId { get; set; }
        public int TotalBooks {  get; set; }
        public int AvailableBooks { get; set; }
        public int ReservedBooks { get; set; }
    }
}
