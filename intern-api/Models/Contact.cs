using System.ComponentModel.DataAnnotations;

namespace intern_api.Models;

public class Contact
{
    public int? id { get; set; }
    public string Address { get; set; }
    
    public string Recipient { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}