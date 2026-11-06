using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace lab_app.Models;

public class NotInFutureAttribute : ValidationAttribute, IClientModelValidator
{
    public NotInFutureAttribute() => ErrorMessage = "Data nie może być z przyszłości";

    public override bool IsValid(object? value) =>
        value is not DateTime date || date.Date <= DateTime.Today;

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-notinfuture", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
    }
}
