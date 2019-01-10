using AutoMapper;
using MedPoint.Data.Entities;
using MedPoint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Data
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            ForAllMaps((typeMap, mappingExpr) =>
            {
                var propertyMaps = typeMap.PropertyMaps;

                foreach (var map in propertyMaps)
                {
                    if (typeof(IEntity).IsAssignableFrom(map.DestinationType))
                    {
                        map.Ignored = true;
                    }
                }
            });

            CreateMap<UserDto, User>();
            CreateMap<TestObjectDto, TestObject>();
        }
    }
}
