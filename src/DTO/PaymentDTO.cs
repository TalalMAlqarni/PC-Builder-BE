namespace src.DTO
{
    public class PaymentDTO
    {
        public class PaymentCreateDto
        {
            public string? PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public bool PaymentStatus { get; set; }
            public decimal TotalPrice { get; set; }
            public Guid CartId { get; set; }
            public Guid OrderId { get; set; }
        }

        public class PaymentReadDto
        {
            public Guid PaymentId { get; set;}
            public string? PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public bool PaymentStatus { get; set; }
            public decimal TotalPrice { get; set; }
            public Guid CartId { get; set; }
            public Guid OrderId { get; set; }
        }

        public class PaymentUpdateDto
        {
            public Guid PaymentId { get; set;}
            public string? PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public bool PaymentStatus { get; set; }
            public decimal TotalPrice { get; set; }
            public Guid CartId { get; set; }
            public Guid OrderId { get; set; }
        }
        public class PaymentDeleteDto
        {
            public Guid PaymentId { get; set;}
            public string? PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public bool PaymentStatus { get; set; }
            public decimal TotalPrice { get; set; }
            public Guid CartId { get; set; }
            public Guid OrderId { get; set; }
        }
    }
}
