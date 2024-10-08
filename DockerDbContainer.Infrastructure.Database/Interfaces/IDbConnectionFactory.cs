namespace DockerDbContainer.Infrastructure.Database.Interfaces
{
    public interface IDbConnectionFactory
    {
        string GetConnectionString(string databaseName);
    }
}
