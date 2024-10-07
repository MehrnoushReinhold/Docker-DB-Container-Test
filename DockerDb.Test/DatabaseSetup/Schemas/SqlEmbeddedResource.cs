namespace Test.DatabaseSetup.Schemas
{
    internal static class SqlEmbeddedResource
    {
        public static string CreateDatabase()
        {
            return EmbeddedResource.GetData("Test.DatabaseSetup.Schemas.CreateDatabase.sql");
        }
    }
}
