using System.Threading.Tasks;
using DockerDbContainer.Infrastructure.Database;
using DockerDbContainer.Infrastructure.Database.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;
using Test.DatabaseSetup.Schemas;

namespace Test.DatabaseSetup;

public class DatabaseFixture
{
    public WeatherDbContext WeatherDbContext;
    public WeatherDbRepository WeatherDbRepository;
    public DbConnectionFactoryDocker DbConnectionFactory { get; } = new();
    public IServiceScope _scope;

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        await StartDatabase();
        await InitializeDatabase();
        await ApplyMigrations();

        var _options = new DbContextOptionsBuilder<WeatherDbContext>().UseSqlServer(DbConnectionFactory.GetConnectionString(DbConnectionFactoryDocker.DatabaseName)).Options;

        WeatherDbContext = new WeatherDbContext(_options);

        DataSeeder.SeedData(WeatherDbContext);

        // Configure services
        var services = new ServiceCollection();
        ConfigureServices(services);

        // Build service provider
        var serviceProvider = services.BuildServiceProvider();

        // Create scope
        _scope = serviceProvider.CreateScope();

        WeatherDbRepository = new WeatherDbRepository(WeatherDbContext);
    }

    private async Task StartDatabase()
    {
        await DbConnectionFactory.DbContainer.StartAsync();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        //Add DbContext.
        services.AddDbContext<WeatherDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(DbConnectionFactory.GetConnectionString(DbConnectionFactoryDocker.DatabaseName));
        });
    }

    [OneTimeTearDown]
    public async Task DisposeAsync()
    {
        await DbConnectionFactory.DbContainer.DisposeAsync().AsTask();
    }

    private async Task InitializeDatabase()
    {
        var connection = DbConnectionFactory.GetMasterConnectionString();
        var sqlCreateDatabase = SqlEmbeddedResource.CreateDatabase();
        ExecuteScript(sqlCreateDatabase, connection);
    }

    public async Task ApplyMigrations()
    {
        var connection = DbConnectionFactory.GetConnectionString(DbConnectionFactoryDocker.DatabaseName);
        var sqlFiles = EmbeddedResource.GetEmbeddedSqlFiles("DockerDbContainer.Infrastructure.Database.sql");
        foreach (var file in sqlFiles)
        {
            var script = await EmbeddedResource.ReadEmbeddedResourceAsync(file);
            ExecuteScript(script, connection);
        }
    }

    private static void ExecuteScript(string script, string connectionString)
    {
        var sqlConnection = new SqlConnection(connectionString);
        var serverConnection = new Microsoft.SqlServer.Management.Common.ServerConnection(sqlConnection);
        var server = new Server(serverConnection);

        int result = server.ConnectionContext.ExecuteNonQuery(script);
    }
}
