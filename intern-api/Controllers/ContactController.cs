using intern_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace intern_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly DataAccessLayer _dataLayer;

    public ContactController(AppDbContext context)
    {
        _dataLayer = new DataAccessLayer(context);
    }


    [HttpPost]
    public async Task<IActionResult> PostContact(ContactDto con)
    {
        var res = await _dataLayer.PostContactToDb(con);
        return Ok(res.Value);
    }
}