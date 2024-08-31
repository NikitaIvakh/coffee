using AutoMapper;
using Coffee.Domain.DTOs;
using Coffee.Domain.Entities;

namespace Coffee.Application.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<CoffeeEntity, CreateCoffeeDto>().ReverseMap();
        CreateMap<CoffeeEntity, UpdateCoffeeDto>().ReverseMap();
        CreateMap<CoffeeEntity, DeleteCoffeeDto>().ReverseMap();
    }
}