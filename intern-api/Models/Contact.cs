using System.ComponentModel.DataAnnotations;

namespace intern_api.Models;

public class Contact
{
    [Key]
    public string Address { get; set; }
    
    public string Recipient { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}