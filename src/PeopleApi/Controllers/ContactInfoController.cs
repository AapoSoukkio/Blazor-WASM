using Microsoft.AspNetCore.Mvc;
using PeopleLib;
using PeopleApi.Data;
using Microsoft.EntityFrameworkCore;

namespace PeopleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactInfoController : ControllerBase
{
    private readonly ILogger<ContactInfoController> _logger;
    private readonly PeopleContext _db;

    public ContactInfoController(ILogger<ContactInfoController> logger, PeopleContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpGet]
    public async Task<IEnumerable<ContactInfo>> Get()
    {
        return await _db.ContactInfos.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ContactInfo model)
    {
        _db.ContactInfos.Add(model);
        await _db.SaveChangesAsync();

        return Ok(); // return 200 Ok response
    }

}
