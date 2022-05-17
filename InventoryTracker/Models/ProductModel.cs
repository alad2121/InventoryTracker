using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryTracker.Models
{
    public class ProductModel
    {



        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Prevents the database from autofilling the id
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Price { get; set; }

        public int LocationId { get; set; }
        [ForeignKey("LocationId")]

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]





        [Display(Name = "Category")]
        [ValidateNever]
        public CategoryModel Category { get; set; }

        [Display(Name = "Location")]
        [ValidateNever]
        public LocationModel Location { get; set; }

        public int? Quantity { get; set; }
        public string selectedLocation { get; set; }
        public string? selectedWareHouse { get; set; }
        public string selectedCategory { get; set; }

    }
}
