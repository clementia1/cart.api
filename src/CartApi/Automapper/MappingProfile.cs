using System.Collections.Generic;
using AutoMapper;
using CartApi.Entities;
using CartApi.Models;

namespace CartApi.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, ProductEntity>();
            CreateMap<ProductEntity, ProductDto>();
        }
    }
}