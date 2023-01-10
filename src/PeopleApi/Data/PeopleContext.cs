using Microsoft.EntityFrameworkCore;
using PeopleLib;

namespace PeopleApi.Data;
public class PeopleContext : DbContext
{
    public PeopleContext(DbContextOptions<PeopleContext> options)
           : base(options)
    {
    }

    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<ContactInfo> ContactInfos { get; set; } = null!;
}