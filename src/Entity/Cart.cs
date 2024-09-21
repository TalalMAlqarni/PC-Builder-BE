namespace sda_3_online_Backend_Teamwork.src.Entity
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Product> Products { get; set; }
        public int CartQuantity { get; set; }
        public decimal TotalPrice { get; set; }

//     }
// }