using System.Net;
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
        public async Task GetAllUsersLocations()
        {
            var response = await testServer.CreateRequest("locations").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task GetLocationsWrongUrl()
        {
            var response = await testServer.CreateRequest("wrongurl").GetAsync();
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GetSingleUserLocation()
        {
            var response = await testServer.CreateRequest("locations/user1").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task GetSingleUserLocationHistory()
        {
            var response = await testServer.CreateRequest("locations/user1/history").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task GetAllUsersLocationsWithinArea()
        {
            var response = await testServer.CreateRequest("locations/area?n=52.0&e-1.0&s=51.0&w=-2.0").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task PostSingleUserLocation()
        {
            var response = await testServer.CreateRequest("locations").PostAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
