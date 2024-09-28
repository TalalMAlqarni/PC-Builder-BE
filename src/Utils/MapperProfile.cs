using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Entity;
using static src.DTO.UserDTO;
using static src.DTO.OrderDTO;

namespace src.Utils
{
    public class MapperProfile : Profile
    {
        // user
        public MapperProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderReadDTO, Order>();
            CreateMap<OrderUpdateDTO, Order>().ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
        }
    }
}