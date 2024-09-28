using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO
{
    public class OrderDTO
    {
        public class OrderCreateDTO
        {
            public Guid UserId { get; set; }
            public Guid CartId { get; set; }
            public Guid PaymentId { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime ShipDate { get; set; }
            public string? OrderStatus { get; set; }
            public bool IsDelivered { get; set; }

            public string? Address { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public int PostalCode { get; set; }
        }
        public class OrderReadDTO
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public Guid CartId { get; set; }
            public Guid PaymentId { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime ShipDate { get; set; }
            public string? OrderStatus { get; set; }
            public bool IsDelivered { get; set; }

            public string? Address { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public int PostalCode { get; set; }
        }
        public class OrderUpdateDTO
        {
            public Guid UserId { get; set; }
            public Guid CartId { get; set; }
            public Guid PaymentId { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime ShipDate { get; set; }
            public string? OrderStatus { get; set; }
            public bool IsDelivered { get; set; }

            public string? Address { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public int PostalCode { get; set; }
        }

    }
}