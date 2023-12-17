using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static FluentValidation.AssemblyScanner;

namespace Shared.Kernel.BuildingBlocks.Services
{
    public class ValidationService
    {
        private readonly List<AssemblyScanResult> assemblyScanResult;
        private readonly IServiceProvider serviceProvider;
        public ValidationService(IServiceProvider serviceProvider, Assembly assembly)
        {
            this.serviceProvider = serviceProvider;
            assemblyScanResult = new List<AssemblyScanResult>();
            assemblyScanResult.AddRange(FindValidatorsInAssembly(assembly));
        }

        public ValidationServiceResult Validate<T>(T model)
        {
            var validator = GetValidatorForModel(model);
            if(validator == null)
            {
                throw new ValidationServiceException("No Validator is registerd");
            }
            ValidationResult validationResult = validator.Validate(new ValidationContext<T>(model));
            return new ValidationServiceResult
            {
                IsValid = validationResult.IsValid,
                Errors = validationResult.Errors.Select(s => s.ErrorMessage).ToList()
            };
        }

        private IValidator GetValidatorForModel(object model)
        {
            var interfaceValidatorType = typeof(IValidator<>).MakeGenericType(model.GetType());

            Type modelValidatorType = assemblyScanResult.FirstOrDefault(i => interfaceValidatorType.IsAssignableFrom(i.InterfaceType))?.ValidatorType;

            if (modelValidatorType == null)
            {
                return null;
            }

            return (IValidator)ActivatorUtilities.CreateInstance(serviceProvider, modelValidatorType);
        }
    }
    public class ValidationServiceResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
    }
    public class ValidationServiceException : Exception
    {
        public ValidationServiceException(string message) : base(message)
        {

        }
    }
    public static class ValidationServieCollectionExtender
    {
        public static IServiceCollection AddValidation(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return serviceCollection.AddScoped<ValidationService>(sp => new ValidationService(sp, assembly));
        }
    }
}
