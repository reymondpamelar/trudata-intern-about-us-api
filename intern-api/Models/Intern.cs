namespace intern_api.Models;

public class Intern
{
    public int? Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string LinkedInURL { get; set; }
    public string EngineerType { get; set; }
}