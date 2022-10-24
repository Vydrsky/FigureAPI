using AutoMapper;
using Figure.Application._Commands.Figure;
using Figure.Application._Commands.Order;
using Figure.Application.Models;
using Figure.DataAccess.Entities;
using Figure.DataAccess.Models;

namespace Figure.Core.MappingProfiles;
public class DefaultMappingProfile : Profile {
    public DefaultMappingProfile() {
        CreateMap<Order, ReadOrderModel>().ReverseMap();
        CreateMap<Order, PostOrderCommand>().ReverseMap();
        CreateMap<Order, UpdateOrderCommand>().ReverseMap();

        CreateMap<DataAccess.Entities.Figure, ReadFigureModel>().ReverseMap();
        CreateMap<DataAccess.Entities.Figure, PostFigureCommand>().ReverseMap();
        CreateMap<DataAccess.Entities.Figure, UpdateFigureCommand>().ReverseMap();

        CreateMap<ApplicationUser, UserModel>().ReverseMap();
    }
}
