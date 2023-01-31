using System.Net;
using System.Net.Mail;
using intern_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace intern_api.Controllers;

public class ContactController
{
    [HttpPost("sendEmail")]
    public async Task<ActionResult<Contact>> SendEmail(Contact contact)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("reymondpamelar@gmail.com", "nhmoscuzxrcbkqki"),
            EnableSsl = true,
        };
    
        smtpClient.Send("reymondpamelar@gmail.com", contact.Address, contact.Title, contact.Body);
        return null;
    }
}