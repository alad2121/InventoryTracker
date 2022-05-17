namespace InventoryTracker.Models.ViewModels
{
    public class LocationVM
    {
        public IEnumerable<LocationModel> Locations { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }
    }
}
