using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.InMemoryDataAccess;
using Newtonsoft.Json.Linq;

namespace ApiTests
{
    /// <summary>
    /// These are integration tests, so that they cover routing, model binding and related issues.
    /// </summary>
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

        [TestInitialize]
        public void TestInitialize()
        {
            var locationStore = testServer.Services.GetService<LocationStore>();
            locationStore?.Clear();
        }

        [TestMethod]
        public async Task GetAllUsersCurrentLocations()
        {
            await PostUser("user1", 51.5, -1.5);
            await PostUser("user1", 51.6, -1.6);
            await PostUser("user2", 51.3, -1.3);
            var response = await testServer.CreateRequest("locations").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var locations = await response.Content.ReadFromJsonAsync<IList<Location>>();
            Assert.IsNotNull(locations);
            Assert.AreEqual(2, locations.Count);
            var location1 = locations.First();
            Assert.AreEqual("user1", location1.UserId);
            Assert.AreEqual(51.6, location1.Latitude);
            Assert.AreEqual(-1.6, location1.Longitude);

            var location2 = locations.Last();
            Assert.AreEqual("user2", location2.UserId);
            Assert.AreEqual(51.3, location2.Latitude);
            Assert.AreEqual(-1.3, location2.Longitude);
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
            // Create a new location:
            await PostUser("user1", 51.5, -1.5);
            var response = await testServer.CreateRequest("locations/user1").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var location = await response.Content.ReadFromJsonAsync<Location>();
            Assert.AreEqual("user1", location?.UserId);
            Assert.AreEqual(51.5, location?.Latitude);
            Assert.AreEqual(-1.5, location?.Longitude);
        }

        [TestMethod]
        public async Task GetSingleUserLocationHistory()
        {
            await PostUser("user1", 51.5, -1.5);
            await PostUser("user1", 51.6, -1.6);
            await PostUser("user2", 51.5, -1.5);
            var response = await testServer.CreateRequest("locations/user1/history").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var locations = await response.Content.ReadFromJsonAsync<IList<Location>>();
            Assert.IsNotNull(locations);
            Assert.AreEqual(2, locations.Count);

            // The first location returned should be the most recent.
            Assert.AreEqual("user1", locations.First().UserId);
            Assert.AreEqual(51.6, locations.First().Latitude);
            Assert.AreEqual(-1.6, locations.First().Longitude);

            // The last location returned should be the oldest.
            Assert.AreEqual("user1", locations.Last().UserId);
            Assert.AreEqual(51.5, locations.Last().Latitude);
            Assert.AreEqual(-1.5, locations.Last().Longitude);

        }

        [TestMethod]
        public async Task GetAllUsersLocationsWithinArea()
        {
            // Only 1 user is currently within the area
            await PostUser("user1", 50.5, -1.5);    // User 1 outside area
            await PostUser("user1", 51.7, -1.7);    // User 1 moves into area
            await PostUser("user2", 52.5, -1.5);    // User 2 outside area
            var response = await testServer.CreateRequest("locations/area?n=52.0&s=51.0&e=-1.0&w=-2.0").GetAsync();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var locations = await response.Content.ReadFromJsonAsync<IList<Location>>();
            Assert.IsNotNull(locations);
            Assert.AreEqual(1, locations.Count);
            Assert.AreEqual("user1", locations.First().UserId);
            Assert.AreEqual(51.7, locations.First().Latitude);
            Assert.AreEqual(-1.7, locations.First().Longitude);
        }

        [TestMethod]
        public async Task PostSingleUserLocation()
        {
            var location = await PostUser("user1", 51.5, -1.5);
            Assert.AreEqual("user1", location?.UserId);
            Assert.AreEqual(51.5, location?.Latitude);
            Assert.AreEqual(-1.5, location?.Longitude);
        }

        /// <summary>
        /// Post a new location for a user.
        /// </summary>
        /// <param name="user">The user's ID.</param>
        /// <param name="latitude">The user's new latitude.</param>
        /// <param name="longitude">The user's new longitude.</param>
        /// <returns>The new location.</returns>
        private static async Task<Location> PostUser(string user, double latitude, double longitude)
        {
            var json = new JObject {{"latitude", latitude}, {"longitude", longitude}};
            var body = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var client = testServer.CreateClient();
            var response = await client.PostAsync($"locations/{user}", body);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var location = await response.Content.ReadFromJsonAsync<Location>();
            return location;
        }
    }
}
