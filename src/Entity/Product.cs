namespace src.Entity
{
    public class Product
    {

        
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        //public string? ProductImage { get; set; } 

        public string? ProductColor { get; set; }

        public int SKU { get; set; }

        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; } // Probably it'll be shown to the user, the used on will be subtotal

        public decimal Subtotal { get; set; }

        public decimal Weight { get; set; }

       // public string? Barcode { get; set; } 

        public decimal RatingResult { get; set; } //Idk how I'll use it here
    }
}
