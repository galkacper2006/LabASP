namespace lab_app.Models;

public class RouteInfo
{
    public string Pattern { get; set; } = "";
    public string Controller { get; set; } = "";
    public string Action { get; set; } = "";
    public string? Area { get; set; }
    public string Methods { get; set; } = "";
}