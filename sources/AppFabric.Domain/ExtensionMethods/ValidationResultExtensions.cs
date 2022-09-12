using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DFlow.Domain.Validation;
using ValidationResultFluent = FluentValidation.Results.ValidationResult;

namespace AppFabric.Domain.ExtensionMethods;

public static class ValidationResultExtensions
{
    public static IReadOnlyList<Failure> ToFailures(this ValidationResultFluent results)
    {
        return results.Errors.Select(r => 
            Failure.For(r.PropertyName, r.ErrorMessage, r.ErrorCode))
            .ToImmutableList();
    }
}