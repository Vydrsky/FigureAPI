using AutoMapper;
using Figure.Core.Models.Order;
using Figure.DataAccess.Entities;

namespace Figure.Core.MappingProfiles;
public class DefaultMappingProfile : Profile {
    public DefaultMappingProfile() {
        CreateMap<Order, ReadOrderModel>().ReverseMap();
    }
}
