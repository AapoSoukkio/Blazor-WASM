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
    public async Task Checkpoint01()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(_client);

        // Act
        var cut = ctx.RenderComponent<Index>();
        cut.WaitForState(() => cut.FindAll("tr").Count.Equals(5));
        var tableElm = cut.Find("table");
        var markup = cut.Markup;
        var linkHrefs = cut.FindAll("a").Select(l => l.GetAttribute("href"));
        var people = await _db.People.ToListAsync();
        

        // Assert
        Assert.NotNull(tableElm);
        foreach (var item in people)
        {
            Assert.Contains(item.FirstName, markup);
            Assert.Contains(item.LastName, markup);
            Assert.Contains(item.Title, markup);
            Assert.Contains($"/person/details/{item.Id}", linkHrefs);
        }
    }
}