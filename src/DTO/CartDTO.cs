using src.Entity;

namespace src.DTO
{
    public class CartDTO
    {
        //create new cart
        public class CartCreateDto
        {
            public string UserId { get; set; }
            public List<CartDetails> CartDetails { get; set; }
            public int CartQuantity { get; set; }//Todo: should be auto calculated
            public decimal TotalPrice { get; set; }//Todo: should be auto calculated
        }
        //read cart
        public class CartReadDto
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public List<CartDetails> CartDetails { get; set; }
            public int CartQuantity { get; set; }
            public decimal TotalPrice { get; set; }

        }

        //update cart
        public class CartUpdateDto
        {
            public List<CartDetails> CartDetails { get; set; }
            public int CartQuantity { get; set; }//Todo: should be auto calculated
            public decimal TotalPrice { get; set; }//Todo: should be auto calculated
        }
    }
}