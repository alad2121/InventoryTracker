namespace InventoryTracker.Models.ViewModels
{
    public class CategoryVM
    {
        public IEnumerable<CategoryModel> Categories { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }   
    }
}
