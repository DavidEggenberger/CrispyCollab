using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using static FluentValidation.AssemblyScanner;

namespace WebWasmClient.Services
{
    public class ValidationService
    {
        private readonly List<string> ScannedAssembly = new List<string>();
        private readonly List<AssemblyScanResult> AssemblyScanResults = new List<AssemblyScanResult>();
        private readonly IServiceProvider serviceProvider;
        public ValidationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(i => !ScannedAssembly.Contains(i.FullName)))
            {
                try
                {
                    AssemblyScanResults.AddRange(FindValidatorsInAssembly(assembly));
                }
                catch (Exception)
                {
                }

                ScannedAssembly.Add(assembly.FullName);
            }
        }

        public ValidationResult Validate<T>(T model)
        {
            var validator = GetValidatorForModel(model);
            return validator.Validate(new ValidationContext<T>(model));
        }

        private IValidator GetValidatorForModel(object model)
        {
            var interfaceValidatorType = typeof(IValidator<>).MakeGenericType(model.GetType());

            Type modelValidatorType = AssemblyScanResults.FirstOrDefault(i => interfaceValidatorType.IsAssignableFrom(i.InterfaceType))?.ValidatorType;

            if (modelValidatorType == null)
            {
                return null;
            }

            return (IValidator)ActivatorUtilities.CreateInstance(serviceProvider, modelValidatorType);
        }
    }
}
