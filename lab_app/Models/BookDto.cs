namespace lab_app.Models;

public class BookDto
{
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
