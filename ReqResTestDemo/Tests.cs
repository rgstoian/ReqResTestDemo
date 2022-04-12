using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReqResTestDemo.Model;
using System.Collections.Generic;

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
        public async Task ValidGetResponse()
        {
            request = new RestRequest("/api/users");
            response = await client.GetAsync(request);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test, Category("GET")]
        public async Task ValidateUsersMetaData()
        {
            string endpoint = "/api/users";
            request = new RestRequest(endpoint);
            response = await client.GetAsync(request);

            Users userpage = JsonConvert.DeserializeObject<Users>(response.Content);
            Assert.IsNotNull(userpage);
            
            int maxpages = userpage.total_pages;
            int totalusers = userpage.total;
            
            List<UserData> users = new List<UserData>();

            for (int i=1; i<=maxpages; i++)
            {
                request = new RestRequest(endpoint + "?page=" + i.ToString());
                response = await client.GetAsync(request);
                userpage = JsonConvert.DeserializeObject<Users>(response.Content);

                Assert.IsTrue(userpage.per_page >= userpage.data.Count);
            
                users.AddRange(userpage.data);
            }

            Assert.AreEqual(totalusers, users.Count);
        }

        [Test, Category("GET")]
        public async Task ValidateExistingUserFields()
        {
            request = new RestRequest("/api/users/1");
            response = await client.GetAsync(request);

            SingleUser user = JsonConvert.DeserializeObject<SingleUser>(response.Content);

            Assert.IsNotNull(user);
            Assert.IsNotNull(user.data.id);
            Assert.IsNotNull(user.data.email);
            Assert.IsNotNull(user.data.first_name);
            Assert.IsNotNull(user.data.last_name);
            Assert.IsNotNull(user.data.avatar);
        }

        [Test, Category("GET")]
        public async Task ValidateUserNotFound()
        {
            request = new RestRequest("/api/users/999");
            response = await client.GetAsync(request);

            Assert.IsFalse(response.IsSuccessful);
        }

        [Test, Category("POST")]
        [TestCase("Tony", "Tester")]
        [TestCase("Dan", "Developer")]
        public async Task ValidateUserCreation(string UserName, string Job)
        {
            request = new RestRequest("/api/users");
            request.AddJsonBody(new { name = UserName, job = Job });

            response = await client.PostAsync(request);

            CreatedUserResponse user = JsonConvert.DeserializeObject<CreatedUserResponse>(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(UserName, user.name);
            Assert.AreEqual(Job, user.job);
        }

    }
}