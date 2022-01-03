using Application.Domain.Entities;
using Application.Web.UI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<SupplierViewModel, SupplierPhysical>().ReverseMap();
            CreateMap<SupplierViewModel, SupplierJuridical>().ReverseMap();
            CreateMap<AddressViewModel, Address>().ReverseMap();
            CreateMap<EmailViewModel, Email>().ReverseMap();
            CreateMap<PhoneViewModel, Phone>().ReverseMap();
        }
    }
}
