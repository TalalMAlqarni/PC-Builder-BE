namespace src.DTO
{
    public class ProductDTO
    {
        //CREATE PRODUCT

        public class CreateProductDto
        {
            public string ProductName { get; set; }
            public string ProductColor { get; set; }
            public string? Description { get; set; }
            public int SKU { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal Weight { get; set; }
            //public decimal RatingResult { get; set; }
        }

        //GET ALL RPODUCTS

        public class GetProductDto
        {
            public Guid Id { get; set; }

            public Guid ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductColor { get; set; }
            public string Description { get; set; }
            public int SKU { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal Weight { get; set; }
            //public decimal RatingResult { get; set; }
        }

        //UPDATE PRODUCT INFO

        public class UpdateProductInfoDto
        {
            public string ProductName { get; set; }
            public string ProductColor { get; set; }
            public string Description { get; set; }
            public int SKU { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal Weight { get; set; }
        }
    }
}
