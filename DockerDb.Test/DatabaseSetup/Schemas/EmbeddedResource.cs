using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.DatabaseSetup.Schemas
{
    public static class EmbeddedResource
    {
        public static string GetData(string fullyQualifiedFilename)
        {
            var assembly = Assembly.GetAssembly(typeof(SqlEmbeddedResource));

            if (assembly == null)
                throw new InvalidOperationException("Could not get assembly for resource");

            var resourceStream = assembly.GetManifestResourceStream(fullyQualifiedFilename);

            if (resourceStream == null)
                throw new InvalidOperationException($"Could not find resource: {fullyQualifiedFilename}");

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var payLoad = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(payLoad))
                    throw new InvalidOperationException($"Could not find payload for resource: {fullyQualifiedFilename}");

                return payLoad;
            }
        }

        public static IEnumerable<string> GetEmbeddedSqlFiles(string resourceFolder)
        {
            var assembly = Assembly.Load("DockerDbContainer.Infrastructure.Database");
            return assembly.GetManifestResourceNames()
                .Where(name => name.StartsWith(resourceFolder) && name.EndsWith(".sql"));
        }

        public static async Task<string> ReadEmbeddedResourceAsync(string resourceName)
        {
            var assembly = Assembly.Load("DockerDbContainer.Infrastructure.Database");
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Resource not found", resourceName);
                }

                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
