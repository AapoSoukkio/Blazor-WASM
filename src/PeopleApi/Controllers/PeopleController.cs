using Microsoft.AspNetCore.Mvc;
using PeopleLib;
using PeopleApi.Data;
using Microsoft.EntityFrameworkCore;

namespace PeopleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly ILogger<PeopleController> _logger;
    private readonly PeopleContext _db;

    public PeopleController(ILogger<PeopleController> logger, PeopleContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> Get()
    {
        return await _db.People.ToListAsync();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Person model)
    {
        var person = await _db.People.FindAsync(id);
        if (null == person)
        {
            return NotFound();
        }
        person.FirstName = model.FirstName;
        person.LastName = model.LastName;
        person.Title = model.Title;

        await _db.SaveChangesAsync();

        return Ok();
    }

}
