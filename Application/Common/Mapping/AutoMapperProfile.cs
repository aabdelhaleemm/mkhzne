using Application.Store.Queries.Dto;
using Application.Store.Queries.GetAll;
using Application.User.Command.Add;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // source => target
            CreateMap<AddUserCommand, Domain.Entities.User>();
            CreateMap<Domain.Entities.Category, StoreCategoryDto>();
            CreateMap<Domain.Entities.Store, StoreDto>();
            CreateMap<Domain.Entities.Order, StoreOrderDto>();
            CreateMap<Domain.Entities.OrderStatus, StoreOrderStatusDto>();
            CreateMap<Domain.Entities.Store, GetAllStoresDto>();
        }
    }
}