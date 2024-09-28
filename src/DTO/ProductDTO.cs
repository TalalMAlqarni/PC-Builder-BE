namespace src.DTO
{
    public class ProductDTO
    {
        //CREATE PRODUCT

        public class CreateProductDto
        {
            public string ProductName { get; set; }
            public string ProductColor { get; set; }
            public string Descreption { get; set; }
            public int Quantity { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal Weight { get; set; }
            public decimal RatingResult { get; set; }
        }


        //GET ALL RPODUCTS

        public class GetProductDto
        {
            public string ProductName { get; set; }
            public string ProductColor { get; set; }
            public string Descreption { get; set; }
            public int SKU { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal Weight { get; set; }
            public decimal RatingResult { get; set; }
        }

        //UPDATE PRODUCT INFO

        public class UpdateProductInfoDto
        {
            public string ProductName { get; set; }
            public string ProductColor { get; set; }
            public string Descreption { get; set; }
            public int SKU { get; set; }
            public decimal Weight { get; set; }
        }

// public class UpdateProdouctDescDto{

//     public string Descreption {get;set;}
// }
       
    }
}
