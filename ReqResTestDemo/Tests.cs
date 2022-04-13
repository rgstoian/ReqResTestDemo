using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;
using ReqResTestDemo.Model;
using System.Collections.Generic;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace ReqResTestDemo
{
    [TestFixture]
    public class Tests
    {
        private RestClient client;
        private RestResponse response;
        private RestRequest request;

        private const string BaseURL = "https://reqres.in/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(BaseURL);
        }

        [Test, Category("GET")]
        public async Task GetSmokeTest()
        {
            request = new RestRequest("/api/users");
            response = await client.GetAsync(request);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test, Category("GET")]
        public async Task UserPageMetadata()
        {
            string endpoint = "/api/users";
            request = new RestRequest(endpoint);
            response = await client.GetAsync(request);

            Users userpage = JsonSerializer.Deserialize<Users>(response.Content);
            Assert.IsNotNull(userpage);

            int maxpages = userpage.total_pages;
            int totalusers = userpage.total;

            List<UserData> users = new List<UserData>();

            for (int i = 1; i <= maxpages; i++)
            {
                request = new RestRequest(endpoint + "?page=" + i.ToString());
                response = await client.GetAsync(request);
                userpage = JsonSerializer.Deserialize<Users>(response.Content);

                Assert.IsTrue(userpage.per_page >= userpage.data.Count);

                users.AddRange(userpage.data);
            }

            Assert.AreEqual(totalusers, users.Count);
        }

        [Test, Category("GET")]
        [TestCase(1, "george.bluth@reqres.in", "George", "Bluth", "https://reqres.in/img/faces/1-image.jpg")]
        [TestCase(2, "janet.weaver@reqres.in", "Janet", "Weaver", "https://reqres.in/img/faces/2-image.jpg")]
        [TestCase(3, "emma.wong@reqres.in", "Emma", "Wong", "https://reqres.in/img/faces/3-image.jpg")]

        public async Task GetSingleUser(int id, string email, string firstname, string lastname, string avatar)
        {
            request = new RestRequest("/api/users/" + id.ToString());
            response = await client.GetAsync(request);

            SingleUser user = JsonSerializer.Deserialize<SingleUser>(response.Content);

            Assert.IsNotNull(user);
            Assert.AreEqual(user.data.id, id);
            Assert.AreEqual(user.data.email, email);
            Assert.AreEqual(user.data.first_name, firstname);
            Assert.AreEqual(user.data.last_name, lastname);
            Assert.AreEqual(user.data.avatar, avatar);
        }

        [Test, Category("GET")]
        public async Task InvalidUserID()
        {
            request = new RestRequest("/api/users/999");
            response = await client.GetAsync(request);

            Assert.IsFalse(response.IsSuccessful);
        }

        [Test, Category("POST")]
        [TestCase("Tony", "Tester")]
        [TestCase("Dan", "Developer")]
        public async Task UserCreation(string UserName, string Job)
        {
            request = new RestRequest("/api/users");
            request.AddJsonBody(new { name = UserName, job = Job });

            response = await client.PostAsync(request);

            CreatedUserResponse user = JsonSerializer.Deserialize<CreatedUserResponse>(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(UserName, user.name);
            Assert.AreEqual(Job, user.job);
        }

        [Test, Category("DELETE")]
        public async Task DeleteUser()
        {
            request = new RestRequest("/api/users/1");
            response = await client.DeleteAsync(request);

            Assert.IsTrue(response.IsSuccessful);
        }

    }
}