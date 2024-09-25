
namespace src.Entity
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        // this will be removed when cartController is updated to use CartDetails instead of Products
        public List<Product> Products { get; set; }
        public List<Product> CartDetails { get; set; }
        public int CartQuantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
}