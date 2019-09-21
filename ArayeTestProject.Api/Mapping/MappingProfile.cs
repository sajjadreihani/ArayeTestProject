using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;
using AutoMapper;
namespace ArayeTestProject.Api.Mapping {
    public class MappingProfile : Profile {
        public MappingProfile () {
            CreateMap<Sale, ProductResource> ();
            CreateMap<Sale, SaleResource> ()
                .ForMember (sr => sr.CityName, opt => opt.MapFrom (s => s.City.Name));
            CreateMap<SaleResource, Sale> ().ForMember (s => s.Id, opt => opt.Ignore ());
            CreateMap<SaleResource, SaleListFilterModel> ()
                .ForMember (sl => sl.MaxPrice, opt => opt.MapFrom (sr => sr.Price));
        }
    }
}