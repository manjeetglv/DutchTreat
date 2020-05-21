using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.Models;

namespace DutchTreat.Data
{
    public class DutchTreatMappingProfile: Profile
    {
        public DutchTreatMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(
                    orderViewModel => orderViewModel.OrderId, 
                    ex => ex.MapFrom(order => order.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }
    }
}