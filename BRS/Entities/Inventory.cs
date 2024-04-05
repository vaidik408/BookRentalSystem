using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BRS.Data;

namespace BRS.Entities
{
    public class Inventory : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InventoryId { get; set; }
        public int TotalBooks {  get; set; }
        public int AvailableBooks { get; set; }
        public int ReservedBooks { get; set; }
    }
}
