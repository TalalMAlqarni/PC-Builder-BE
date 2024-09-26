namespace src.Entity
{
    public class CartDetails
    {

        public Product? Product { get; set; }
        Guid CartId { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}