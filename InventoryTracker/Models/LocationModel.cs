using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryTracker.Models
{
    public class LocationModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Warehouse")]
        [Required]
        public string? WarehouseName { get; set; }   

        

        public int? NumberOfProducts { get; set; }
    }
}
