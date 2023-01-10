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
    public async Task Checkpoint04()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(_client);
        System.Random r = new System.Random();
        var address = new Address
        {
            StreetAddress = "Tester Lane 1",
            PostalNumber = r.Next(),
            PostalAddress = "CI/CD"
        };

        // Act
        var cut = ctx.RenderComponent<AddressCreate>();
        cut.WaitForState(() => cut.HasComponent<EditForm>());
        var markup = cut.Markup;
        var inputs = cut.FindAll("input");
        var submit = cut.Find("button[type=\"submit\"]");
        // _testOutputHelper.WriteLine(markup);
        

        // Assert
        Assert.NotNull(inputs);
        Assert.Equal(3, inputs.Count);
        Assert.NotNull(submit);
        Assert.Equal("Add new", submit.TextContent);
        Assert.True(cut.HasComponent<InputText>());
        Assert.True(cut.HasComponent<InputNumber<int>>());

        // Act 2
        cut.FindAll("input")[0].Change(address.StreetAddress);
        cut.FindAll("input")[1].Change(address.PostalNumber);
        cut.FindAll("input")[2].Change(address.PostalAddress);

        await submit.ClickAsync(new MouseEventArgs());

        var dbAddress = await _db.Addresses.OrderBy(a => a.Id).LastOrDefaultAsync();

        // Assert 2
        Assert.Equal(address.StreetAddress, dbAddress.StreetAddress);
        Assert.Equal(address.PostalNumber, dbAddress.PostalNumber);
        Assert.Equal(address.PostalAddress, dbAddress.PostalAddress);
    }
}