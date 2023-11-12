using AutoMapper;
using MediatR;
using PersonReplations.Application.Features.PersonFeatures.Models;
using PersonReplations.Application.Interfaces;
using System.Text;

namespace PersonReplations.Application.Features.PersonFeatures;

public record GetPersonRequest(int PersonId) : IRequest<GetPersonResponse?>;

public class GetPersonRequestHandler : IRequestHandler<GetPersonRequest, GetPersonResponse?>
{
  private readonly IPersonRepository _personRepository;
  private readonly IMapper _mapper;
  private readonly IFileServiceRepository _fileServiceRepository;
  public GetPersonRequestHandler(IPersonRepository repository, IMapper mapper, IFileServiceRepository fileService)
  {
    _personRepository = repository;
    _mapper = mapper;
    _fileServiceRepository = fileService;
  }
  public async Task<GetPersonResponse?> Handle(GetPersonRequest request, CancellationToken cancellationToken)
  {
    var person = await _personRepository.GetPersonFullInfo(request.PersonId);
    if (person == null) return null;
    var result = _mapper.Map<GetPersonResponse>(person);
    result.Image = await GetImageString(person.Id, person.ImagePath);
    return result;

  }

  private async Task<string?> GetImageString(int personId, string? fileName)
  {
    if (string.IsNullOrEmpty(fileName)) return null;
    var file = await _fileServiceRepository.GetFile(personId, fileName);
    return $"data:image/{fileName.Split(".")[1]};base64,{Convert.ToBase64String(file)}";
  }
}


