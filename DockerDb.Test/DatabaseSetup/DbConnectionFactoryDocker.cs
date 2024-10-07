using System;
using DockerDbContainer.Infrastructure.Database.Interfaces;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Test.DatabaseSetup;

public class DbConnectionFactoryDocker : IDbConnectionFactory
{
    private const string ImageName = "mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04";
    private const ushort MssqlPort = 1433;
    private const string Username = "sa";
    private const string Password = "myStr0ng!&Passw0rd!";
    private const string ContainerName = "WeatherTestDb";
    public const string DatabaseName = "WeatherTestDb";

    private IContainer _dbContainer = new ContainerBuilder()
        .WithImage(ImageName)
        .WithName(ContainerName)
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithEnvironment("SQLCMDUSER", Username)
        .WithEnvironment("SQLCMDPASSWORD", Password)
        .WithEnvironment("MSSQL_SA_PASSWORD", Password)
        .WithAutoRemove(true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MssqlPort))
        .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged((".*Recovery is complete. This is an informational message only. No user action is required.*"), o => o.WithTimeout(TimeSpan.FromMinutes(1))))
        .WithPortBinding(MssqlPort, true)
        .WithCleanUp(true)
        .Build();

    public IContainer DbContainer => _dbContainer;

    public string GetMasterConnectionString()
    {
        var connectionString = $"Server={DbContainer.Hostname},{DbContainer.GetMappedPublicPort(MssqlPort)};Database=master;User Id={Username};Password={Password};TrustServerCertificate=True;";
        return connectionString;
    }

    public string GetConnectionString(string databaseName)
    {
        var connectionString = $"Server={DbContainer.Hostname},{DbContainer.GetMappedPublicPort(MssqlPort)};Database=master;User Id={Username};Password={Password};TrustServerCertificate=True;";
        return connectionString.Replace("Database=master", $"Database={databaseName}");
    }

}
