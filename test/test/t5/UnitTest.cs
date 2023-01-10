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
    public async Task Checkpoint05()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(_client);
        System.Random r = new System.Random();
        var person = new Person
        {
            Id = 4,
        };

        var address = new Address
        {
            Id = 10,
        };
        var info = $"test info\n{r.Next()}";

        // Act
        var cut = ctx.RenderComponent<ContactInfoPage>();
        cut.WaitForState(() => cut.HasComponent<EditForm>());
        var markup = cut.Markup;
        var selects = cut.FindAll("select");
        var submit = cut.Find("button[type=\"submit\"]");
        var forAddress = cut.Find("#foraddress");
        var forPerson = cut.Find("#forperson");
        var forInfo = cut.Find("textarea");
        // _testOutputHelper.WriteLine(markup);


        // Assert
        Assert.NotNull(selects);
        Assert.Equal(2, selects.Count);
        Assert.NotNull(submit);
        Assert.Equal("Join", submit.TextContent);
        Assert.True(cut.HasComponent<InputSelect<int>>());
        Assert.True(cut.HasComponent<InputTextArea>());

        // Act 2
        forAddress.Change(address.Id);
        forPerson.Change(person.Id);
        forInfo.Change(info);

        await submit.ClickAsync(new MouseEventArgs());

        var dbContact = await _db.ContactInfos.FirstOrDefaultAsync(c => c.PersonId == person.Id && c.AddressId == address.Id);

        // Assert 2
        Assert.Equal(info, dbContact.Info);
    }
}