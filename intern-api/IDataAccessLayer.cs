using intern_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace intern_api;

public interface IDataAccessLayer
{
    Task<ActionResult<Intern>> GetIntern(int? id);
    Intern PostIntern(InternDto arg);
    Task<ActionResult<Contact>> PostContactToDb(ContactDto con);
    Task<ActionResult<Intern>> GetInternByUsername(string username);

    Task<List<Intern>> GetAllInterns();
}