using System.ComponentModel.DataAnnotations;

namespace lab_app.Validation;

public class NotInFutureAttribute : ValidationAttribute
{
    public NotInFutureAttribute()
    {
        ErrorMessage = "Data nie może być z przyszłości";
    }

    public override bool IsValid(object? value)
    {
        return value is not DateTime d || d.Date <= DateTime.Today;
    }
}