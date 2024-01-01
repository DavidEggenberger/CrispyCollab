namespace Shared.Kernel.BuildingBlocks.ModelValidation
{
    public interface IValidationService
    {
        void Validate<T>(T model);
    }
}