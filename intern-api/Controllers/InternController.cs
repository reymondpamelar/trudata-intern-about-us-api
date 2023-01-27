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
    private readonly AppDbContext _dynamoDbContext;
    public InternController(AppDbContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }
    
    
    //GET BY User Name
    [HttpGet("getBy{id}")]
    public async Task<ActionResult<User>> GetIntern(int id)
    {
        var item = await _dynamoDbContext.interns.FindAsync(id);
            
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Intern>> PostIntern(InternDto arg)
    {
        Intern? intern = new Intern();
        intern.Id = null;
        intern.Username = arg.Username;
        intern.Email = arg.Email;
        intern.Image = arg.Image;
        intern.Description = arg.Description;
        await _dynamoDbContext.interns.AddAsync(intern);
        await _dynamoDbContext.SaveChangesAsync();
        return Ok(intern);
    }
}

