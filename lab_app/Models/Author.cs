using System.ComponentModel.DataAnnotations;

namespace lab_app.Models;

public class Author
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Imię i nazwisko autora jest wymagane")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Imię i nazwisko")]
    public string Name { get; set; } = "";
}