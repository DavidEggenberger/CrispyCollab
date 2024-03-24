namespace Shared.Kernel.BuildingBlocks.Services.ModelValidation
{
    public interface IValidationService
    {
        void Validate<T>(T model);
    }
}