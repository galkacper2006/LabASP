using System.ComponentModel.DataAnnotations;

namespace lab_app.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(20)]
    public string Isbn { get; set; } = "";

    public DateTime? PublicationDate { get; set; }

    public int NumPages { get; set; }

    public decimal Price { get; set; }

    [Required]
    [StringLength(100)]
    public string Author { get; set; } = "";
    
    
    [StringLength(100)]
    public string? Publisher { get; set; }

}