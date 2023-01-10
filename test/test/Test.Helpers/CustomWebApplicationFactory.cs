using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Test.Helpers;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    //private readonly ITestOutputHelper _testOutputHelper;
    public CustomWebApplicationFactory()
    {
    }

    //public CustomWebApplicationFactory(ITestOutputHelper testOutputHelper)
    //{
    //    _testOutputHelper = testOutputHelper;
    //}

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Register the xUnit logger
        builder.ConfigureLogging(loggingBuilder =>
        {
                //loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XunitLoggerProvider(_testOutputHelper));
        });
    }
}
