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
using Microsoft.AspNetCore.Components.Forms;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;

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
    public async Task Checkpoint03()
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

        // Act
        var cut = ctx.RenderComponent<PersonEdit>(parameters => parameters.Add(p => p.Id, person.Id));
        cut.WaitForState(() => cut.HasComponent<EditForm>());
        var markup = cut.Markup;
        var edit = cut.FindComponent<EditForm>();
        var inputs = cut.FindAll("input");
        var inputValues = inputs.Select(i => i.GetAttribute("value"));
        var submit = cut.Find("button[type=\"submit\"]");
        // _testOutputHelper.WriteLine(markup);

        // Assert
        Assert.NotNull(edit);
        Assert.NotNull(inputs);
        Assert.Equal(3, inputs.Count);
        Assert.NotNull(submit);
        Assert.Equal("Save changes", submit.TextContent);
        Assert.Contains(person.FirstName, inputValues);
        Assert.Contains(person.LastName, inputValues);
        Assert.Contains(person.Title, inputValues);

        // Act 2
        var titleInput = inputs.FirstOrDefault(i => i.GetAttribute("value") == person.Title);
        System.Random r = new System.Random();
        string newTitle = $"changed by test - {r.Next()}";
        titleInput.Change(newTitle);
        await submit.ClickAsync(new MouseEventArgs());

        var dbPerson = await _db.People.FindAsync(person.Id);

        // Assert 2
        Assert.Equal(newTitle, dbPerson.Title);
    }
}