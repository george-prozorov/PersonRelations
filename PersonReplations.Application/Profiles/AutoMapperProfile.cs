using AutoMapper;
using PersonReplations.Application.Features.PersonFeatures;
using PersonReplations.Application.Features.PersonFeatures.Models;
using PersonReplations.Domain.Entities;
using PersonReplations.Application.Helpers;

namespace PersonReplations.Application.Profiles;

internal class AutoMapperProfile : Profile
{
  public AutoMapperProfile()
  {
    CreateMap<AddPersonRequest, Person>();
    CreateMap<AddContactRequest, Contact>();
    CreateMap<Person, GetPersonResponse>()
      .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender!.DisplayName))
      .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.City!.DisplayName))
      .ForMember(dst => dst.Relatives, opt => opt.MapFrom(src => src.PersonRelations));
    CreateMap<Contact, ContactResponse>()
      .ForMember(dst => dst.ContactType, opt => opt.MapFrom(src => src.ContactType!.DisplayName));
    CreateMap<PersonRelation, Relative>()
      .ForMember(dst => dst.RelationTypeId, opt => opt.MapFrom(src => src.Relation!.RelationTypeId))
      .ForMember(dst => dst.RelationType, opt => opt.MapFrom(src => src.Relation!.RelationType!.DisplayName))
      .ForMember(dst => dst.RelativePersonId, opt => opt.MapFrom(src => src.Relation!.PersonRelations.First(x => x.PersonId != src.PersonId).PersonId))
      .ForMember(dst => dst.RelativePerson, opt => opt.MapFrom(src => src.Relation!.PersonRelations.First(x => x.PersonId != src.PersonId).Person!.GetPersonName()));
  }
}
