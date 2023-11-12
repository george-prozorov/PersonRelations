using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using PersonReplations.Application.Helpers;
using PersonReplations.Application.Interfaces;
using PersonReplations.Application.Resources.Localization;
using PersonReplations.Domain.Entities;
using System.Data;

namespace PersonReplations.Application.Features.ImageFeatures;

public record SetImageRequest(IFormFile File, int PersonId) : IRequest;

public class SetImageRequestHandler : IRequestHandler<SetImageRequest>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IFileServiceRepository _fileServiceRepository;
  public SetImageRequestHandler(IUnitOfWork unitOfWork, IFileServiceRepository fileServiceRepository)
  {
    _unitOfWork = unitOfWork;
    _fileServiceRepository = fileServiceRepository;
  }

  async Task IRequestHandler<SetImageRequest>.Handle(SetImageRequest request, CancellationToken cancellationToken)
  {
    var fileName = await _fileServiceRepository.SaveFileAsync(request.File, request.PersonId);
    var person = await _unitOfWork.PersonRepository.GetPersonForUpdate(request.PersonId);
    person.UpdateImagePath(fileName);
    await _unitOfWork.SaveChangesAsync();
  }
}

public class SetImageRequestValidator : AbstractValidator<SetImageRequest>
{

  public SetImageRequestValidator(IStringLocalizer<Messages> localizer, IReferenceRepository repository)
  {
    RuleFor(x => x.File.ContentType)
      .Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
      .WithMessage(localizer["FileType"]);
    RuleFor(x => x.File)
      .Must(x => x.IsImage())
      .WithMessage(localizer["FileType"]);
    RuleFor(x => x.PersonId)
      .NotNull()
      .GreaterThan(0)
      .MustAsync(async (id, cancelationToken) => await repository.ValidateReference<Person>(id, cancelationToken));
  }
}
