using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation.Excceptions;
using static FluentValidation.AssemblyScanner;

namespace Shared.Kernel.BuildingBlocks.Services.ModelValidation
{
    public class ValidationService : IValidationService
    {
        private readonly List<AssemblyScanResult> assemblyScanResult = new List<AssemblyScanResult>();
        private readonly IServiceProvider serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Validate<T>(T model)
        {
            var validator = GetValidatorForModel(model, typeof(T));
            if (validator == null)
            {
                throw new NoValidatorFoundException();
            }
            ValidationResult validationResult = validator.Validate(new ValidationContext<T>(model));
            if (validationResult.IsValid is false)
            {
                throw new Exception();
            }
        }

        private IValidator GetValidatorForModel(object model, Type modelType)
        {
            assemblyScanResult.AddRange(FindValidatorsInAssembly(modelType.Assembly));

            var interfaceValidatorType = typeof(IValidator<>).MakeGenericType(model.GetType());

            Type modelValidatorType = assemblyScanResult.FirstOrDefault(i => interfaceValidatorType.IsAssignableFrom(i.InterfaceType))?.ValidatorType;

            if (modelValidatorType == null)
            {
                return null;
            }

            return (IValidator)ActivatorUtilities.CreateInstance(serviceProvider, modelValidatorType);
        }
    }
}
