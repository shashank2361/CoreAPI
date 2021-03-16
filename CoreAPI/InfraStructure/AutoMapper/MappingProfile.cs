using CoreAPI.Dtos;
using CoreAPI.Models;
using AutoMapper;
 

namespace CoreAPI.InfraStructure.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();

            CreateMap<OrderModel, Order>()
               .ForMember(x => x.OrderState, opt => opt.MapFrom(src => 1));
        }
    }
}