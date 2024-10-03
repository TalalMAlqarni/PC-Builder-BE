namespace src.Entity
{
    public class Product
    {



        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }

        //public string? ProductImage { get; set; } , It will be added during 

        public string? ProductColor { get; set; }

        public string? Description {get;set;}

        public int SKU { get; set; }

        public decimal ProductPrice { get; set; } 


        public decimal Weight { get; set; }


        public decimal AverageRating { get; set; } 
    }
}
