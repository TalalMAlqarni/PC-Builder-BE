using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.DTO;
using src.Entity;
using static src.DTO.OrderDTO;

namespace src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderReadDTO, Order>();
            CreateMap<OrderUpdateDTO, Order>().ForAllMembers(
                opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
        }
    }
}