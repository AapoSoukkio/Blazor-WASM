using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Bunit;
using PeopleApi;
using PeopleApi.Data;
using BlazorPeople;
using BlazorPeople.Pages;
using PeopleLib;

namespace test;

public class UnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<PeopleApi.Program> _applicationFactory;

    private readonly PeopleContext _db;

    public UnitTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _applicationFactory = new WebApplicationFactory<PeopleApi.Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

        _client = _applicationFactory.CreateClient();

        string connectionstring = "Data Source=People.db";

        var optionsBuilder = new DbContextOptionsBuilder<PeopleContext>();
        optionsBuilder.UseSqlite(connectionstring);

        _db = new PeopleContext(optionsBuilder.Options);
    }

    [Fact]
    public async Task Checkpoint02()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(_client);
        var person = new Person
        {
            Id = 1,
            FirstName = "Jean",
            LastName = "Riker",
            Title = "Commander"
        };

        var addresses = new Address[] {
            new Address {
                Id = 10,
                StreetAddress = "Boston blvd. 42",
                PostalNumber = 45678,
                PostalAddress = "Boston"
            },
            new Address {
                Id = 40,
                StreetAddress = "Homelane 22",
                PostalNumber = 98989,
                PostalAddress = "Toronto"
            }
        };
        var contacts = new ContactInfo[] {
            new ContactInfo {
                Id = 100,
                AddressId = 10,
                PersonId = 1,
                Info = "Home address"
            },
            new ContactInfo {
                Id = 200,
                AddressId = 40,
                PersonId = 1,
                Info = "Office address"
            }
        };

        // Act
        var cut = ctx.RenderComponent<PersonDetails>(parameters => parameters.Add(p => p.Id, person.Id));
        cut.WaitForState(() => cut.FindAll("table").Count.Equals(1));
        var tableElm = cut.Find("table");
        var markup = cut.Markup;
        // _testOutputHelper.WriteLine(cut.Markup);
        var linkHrefs = cut.FindAll("a").Select(l => l.GetAttribute("href"));

        // Assert
        Assert.NotNull(tableElm);
        Assert.Contains($"/person/edit/{person.Id}", linkHrefs);
        Assert.Contains(person.FirstName, markup);
        Assert.Contains(person.LastName, markup);
        Assert.Contains(person.Title, markup);
        foreach (var item in addresses)
        {
            Assert.Contains(item.StreetAddress, markup);
            Assert.Contains(item.PostalNumber.ToString(), markup);
            Assert.Contains(item.PostalAddress, markup);
        }        
        foreach (var item in contacts)
        {
            Assert.Contains(item.Info, markup);
        }        
    }
}