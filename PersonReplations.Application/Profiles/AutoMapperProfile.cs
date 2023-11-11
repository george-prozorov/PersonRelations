using AutoMapper;
using PersonReplations.Application.Features.PersonFeatures;
using PersonReplations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Profiles
{
  internal class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<AddPersonRequest, Person>().IncludeAllDerived().ReverseMap();
    }
  }
}
