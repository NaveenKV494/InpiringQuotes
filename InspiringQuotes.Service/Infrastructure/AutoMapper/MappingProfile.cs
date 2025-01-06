using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InspiringQuotes.Data.DTOs.RequestDTO;
using InspiringQuotes.Data.DTOs.ResponseDTO;
using InspiringQuotes.Data.Models;

namespace InspiringQuotes.Service.Infrastructure.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<QuoteRequestDTO, Quote>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(tagName => new Tag { TagName = tagName }).ToList()));

            CreateMap<Quote, QuoteResponseDTO>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(tag => tag.TagName).ToList()));
        }
    }
}
