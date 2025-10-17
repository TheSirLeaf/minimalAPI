using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Minimal_Api.Dominio.Interfaces;
using Test.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Test.Helpers;

public class Setup
{
    public const string PORT = "5001";
    // Do not store TestContext in a static field. Extract only what you need locally in ClassInit.
    public static WebApplicationFactory<Program> http = default!;
    public static HttpClient client = default!;

    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        // Use TestContext locally; don't store the whole object.
        var testRunDirectory = testContext.TestRunDirectory;

        // WebApplicationFactory should use the Program type from the application project.
        Setup.http = new WebApplicationFactory<Program>();

        Setup.http = Setup.http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
            });

        });

        Setup.client = Setup.http.CreateClient();
    }

    public static void ClassCleanup()
    {
        Setup.http.Dispose();
    }
}