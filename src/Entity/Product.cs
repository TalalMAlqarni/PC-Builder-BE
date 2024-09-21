namespace src.Entity
{
    public class Product
    {

        
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; } //I've to make sure

        public string? ProductColor { get; set; }

        public int SKU { get; set; }

        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }

        public int Subtotal { get; set; }

        public decimal Weight { get; set; }

        public string? Barcode { get; set; } //I've to make sure

        public decimal RatingResult { get; set; }
    }
}
