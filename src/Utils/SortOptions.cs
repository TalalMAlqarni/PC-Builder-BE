
namespace src.Utils{
    public enum SortOption{

    None,
    PriceAsc,
    PriceDesc,
    //NewArrival 
    SKUAsc, 
    SKUDesc

}

 /*
        productPrice = ASC , DESC
        SKU = ASC,DESC
        NEW ARRIVALS, CAN I DO IT? 
        */

    public class SortOptions{



        //public List<string> sortOptions {get;set;} = new List<string>();
        public SortOption SortOption {get;set;} = SortOption.None;

    }
}