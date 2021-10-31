using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests
{
    [TestClass]
    public class LocationsControllerTests
    {
        private static TestServer testServer;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            testServer = new TestServer(builder);
        }

        [TestMethod]
        public async Task Get()
        {
            var response = await testServer.CreateRequest("locations").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
