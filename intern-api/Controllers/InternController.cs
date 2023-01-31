using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using intern_api.Models;
using Microsoft.AspNetCore.Authorization;

namespace intern_api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class InternController : ControllerBase
{
    private readonly IDataAccessLayer _dataLayer;
    
    
    public InternController(IDataAccessLayer datalayer)
    {
        _dataLayer = datalayer;
    }
    
    
    //GET BY User Name
    [HttpGet("/api/intern/{userName}")]
    public async Task<ActionResult<Intern>> GetIntern([FromQuery] int? id, [FromQuery] string? username)
    {
        ActionResult<Intern> item;
        if(id != null)
        {
            item = await _dataLayer.GetIntern(id);
        }
        else
        {
            item = await _dataLayer.GetInternByUsername(username);
        }
            
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Intern>> PostIntern(InternDto arg)
    {
        return Ok(_dataLayer.PostIntern(arg));
    }
    
    [HttpGet("getAllInterns")]
    public async Task<ActionResult<Intern>> GetAllInterns()
    {
        var res = await _dataLayer.GetAllInterns();
        return Ok(res);
    }
    
}

