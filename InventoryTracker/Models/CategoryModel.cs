using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryTracker.Models
{
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? NumberOfProducts { get; set; }


        
    }
}
