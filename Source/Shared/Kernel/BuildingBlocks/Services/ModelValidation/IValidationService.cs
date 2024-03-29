using FluentValidation.Results;

namespace Shared.Kernel.BuildingBlocks.Services.ModelValidation
{
    public interface IValidationService
    {
        ValidationResult Validate<T>(T model);
    }
}