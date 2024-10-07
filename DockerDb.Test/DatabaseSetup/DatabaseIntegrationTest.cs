using NUnit.Framework;
using System.Threading.Tasks;

namespace Test.DatabaseSetup
{
    public class DatabaseIntegrationTest
    {
        protected DatabaseFixture DbFixture;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            DbFixture = new DatabaseFixture();
            await DbFixture.InitializeAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await DbFixture.DisposeAsync();
        }
    }
}