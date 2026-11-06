using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace lab_app.Validation;

public class IsbnAttribute : ValidationAttribute, IClientModelValidator
{
    public bool RequireChecksum { get; set; }

    public IsbnAttribute() => ErrorMessage = "ISBN musi mieć 10 lub 13 cyfr (spacje i myślniki są dozwolone)";

    public override bool IsValid(object? value)
    {
        if (value is null) return true; // [Required] sprawdza puste pole
        if (value is not string isbn || isbn.Length == 0) return false;
        if (isbn.Any(ch => !char.IsAsciiDigit(ch) && ch != '-' && ch != ' ')) return false;

        string digits = new(isbn.Where(char.IsAsciiDigit).ToArray());
        if (digits.Length is not (10 or 13)) return false;
        if (!RequireChecksum || digits.Length == 10) return true;

        int sum = 0;
        for (int i = 0; i < digits.Length; i++)
            sum += (digits[i] - '0') * (i % 2 == 0 ? 1 : 3);

        return sum % 10 == 0;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-isbn", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        context.Attributes.TryAdd("data-val-isbn-checksum", RequireChecksum ? "true" : "false");
    }
}
