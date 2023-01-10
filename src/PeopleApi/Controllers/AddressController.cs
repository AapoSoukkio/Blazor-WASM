using Microsoft.AspNetCore.Mvc;
using PeopleLib;
using PeopleApi.Data;
using Microsoft.EntityFrameworkCore;

namespace PeopleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly ILogger<AddressController> _logger;
    private readonly PeopleContext _db;

    public AddressController(ILogger<AddressController> logger, PeopleContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Address>> Get()
    {
        return await _db.Addresses.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Address model)
    {
        _db.Addresses.Add(model);
        await _db.SaveChangesAsync();

        return Ok(); // return 200 Ok response
    }

}
