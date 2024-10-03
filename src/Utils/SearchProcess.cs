namespace src.Utils
{
    public class SearchProcess
    {
        //first: user will search on a product name:
        public string Search { get; set; } =string.Empty;

        //second: user will filter the product based on specific criteria

        public string? Name { get; set; }
        public string? Color { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        //third: user will sort the prodcut based on sku or price by asc or desc

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending; // the enum is defined in sortOptions class , I might put it in a sperate class
        public string SortBy { get; set; } = "price";

        //fourth: user will implmenet pagination

        public int Limit { get; set; }=5;

        public int Offset { get; set; } =0;

    }
}
