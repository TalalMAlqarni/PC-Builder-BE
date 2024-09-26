using src.Entity;

namespace sda_3_online_Backend_Teamwork
{
    public static class CartUtils
    {
        public static string ThereIsLowStockProduct(Cart cart)
        {
            var lowStockProduct = cart.CartDetails.FirstOrDefault(p => p.Product.SKU < p.Quantity);
            if (lowStockProduct != null)
            {
                return lowStockProduct.Product.ProductName;
            }
            return "";
        }
    }
}