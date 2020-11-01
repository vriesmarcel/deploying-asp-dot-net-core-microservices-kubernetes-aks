using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;

namespace GloboTicket.Services.EventCatalog.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile(IConfiguration configuration)
        {
            var version = configuration["CategorySchemaVersion"];
            bool UseSchemaV2 = version.Equals("2.0");
            if (UseSchemaV2)
            {
                CreateMap<Entities.Category, Models.CategoryDto>().ReverseMap();
            }
            else
            {
                //ignore the new properties untill we use the new schema
                CreateMap<Entities.Category, Models.CategoryDto>()
                    .Ignore(c=>c.FromDate)
                    .Ignore(c=>c.UntilDate)
                    .ReverseMap();
            }
        }
    }

    public static class ProfileExtentions
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }

    }
}
