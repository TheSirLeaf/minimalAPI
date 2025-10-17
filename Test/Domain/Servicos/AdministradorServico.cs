using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minimal_Api.Dominio.Entidades;
using Minimal_Api.Dominio.Servicos;
using Minimal_Api.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public sealed class AdministradorServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        // Assembly location is ...\Test\bin\Debug\net10.0
        // go up 3 levels to reach the test project root (...\Test\)
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        // var connectionString = configuration.GetConnectionString("MySQL");

        // var options = new DbContextOptionsBuilder<DbContexto>()
        //     .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        //     .Options;

        return new DbContexto(configuration);
    }
    [TestMethod]
    public void TestandoBuscaPorId()
    {
        //Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE administradores;");
        var adm = new Administrador();
        adm.Id = 1;
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";
        var administradorServico = new AdministradorServico(context);

        //Act
        administradorServico.Incluir(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        //Assert
        if(admDoBanco == null)
        {
            Assert.Fail("Administrador não encontrado no banco de dados.");
        }
        Assert.AreEqual(1, admDoBanco.Id);
        // Assert.AreEqual(1, administradorServico.Todos(1).Count());
        // Assert.AreEqual("teste@teste.com", adm.Email);
        // Assert.AreEqual("teste", adm.Senha);
        // Assert.AreEqual("Adm", adm.Perfil);
    }
}
