using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineCatalog.Entites;
using WineCatalog.Models;

namespace WineCatalog.Profiles
{
    public class WineProfile : Profile
    {
        public WineProfile()
        {
            CreateMap<Wine, WineDTO>()
                .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name));
        }
    }
}
