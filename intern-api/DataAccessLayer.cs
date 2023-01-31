using intern_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace intern_api;

public class DataAccessLayer : IDataAccessLayer
{
    private readonly AppDbContext _context;

    public DataAccessLayer(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<Intern>> GetIntern(int? id)
    {
        var intern = await _context.interns.FindAsync(id);
        return intern;
    }

    public Intern PostIntern(InternDto arg)
    {
        Intern? intern = new Intern()
        {
            Id = null,
            Username = arg.Username,
            Email = arg.Email,
            Image = arg.Image,
            Description = arg.Description,
            LinkedInURL = arg.LinkedInURL,
            EngineerType = arg.EngineerType
        };
        
        _context.interns.AddAsync(intern);
        _context.SaveChangesAsync();
        
        return intern;
    }
    
    //Get All Interns
    public async Task<List<Intern>> GetAllInterns()
    {
        var res = await _context.interns.ToListAsync();
        return res;

    }

    public async Task<ActionResult<Contact>> PostContactToDb(ContactDto con)
    {
        Contact contact = new Contact()
        {
            Address = con.Address,
            Title = con.Title,
            Body = con.Body
        };
        
        await _context.contact.AddAsync(contact);
        return contact;

    }

    public async Task<ActionResult<Intern>> GetInternByUsername(string username)
    {
        return await _context.interns.Where(u=>u.Username == username).FirstOrDefaultAsync();
    }
}
