using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json.Linq;

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
            var locations = await response.Content.ReadFromJsonAsync<IEnumerable<Location>>();
            Assert.AreEqual(0, locations?.Count());
        }

        [TestMethod]
        [DataRow("wrongurl")]
        [DataRow("locations/wrongurl/user1")]
        public async Task GetLocationsWrongUrl(string url)
        {
            var response = await testServer.CreateRequest(url).GetAsync();
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GetSingleUserLocation()
        {
            var response = await testServer.CreateRequest("locations/user1").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("GetSingleUserLocation", content);
        }

        [TestMethod]
        public async Task GetSingleUserLocationHistory()
        {
            var response = await testServer.CreateRequest("locations/user1/history").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("GetSingleUserLocationHistory", content);
        }

        [TestMethod]
        public async Task GetAllUsersLocationsWithinArea()
        {
            var response = await testServer.CreateRequest("locations/area?n=52.0&e=-1.0&s=51.0&w=-2.0").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("GetAllUsersLocationsWithArea 52 -1 51 -2", content);
        }

        [TestMethod]
        public async Task PostSingleUserLocation()
        {
            var json = new JObject { { "latitude", 51.5 }, { "longitude", -1.5 } };
            var body = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var client = testServer.CreateClient();
            var response = await client.PostAsync("locations/user1", body);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var location = await response.Content.ReadFromJsonAsync<Location>();
            Assert.AreEqual("user1", location?.UserId);
            Assert.AreEqual(51.5, location?.Latitude);
            Assert.AreEqual(-1.5, location?.Longitude);
        }
    }
}
