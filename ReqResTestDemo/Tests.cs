using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using ReqResTestDemoApp.Model;
using ReqResTestDemoApp;
using System;

namespace ReqResTestDemoTests
{
    [TestFixture]
    public class Tests
    {

        [Test, Category("GET")]
        public void GetSmokeTest()
        {
            var call = new APICalls();
            Assert.IsTrue(call.GetUserPage().IsSuccessful);
        }

        [Test, Category("GET")]
        public void CorrectNumberOfUsers()
        {
            var call = new Helper<UsersPage>();
            UsersPage defaultUsersResponse = call.ResponseClass(call.GetUserPage());
            Assert.AreEqual(defaultUsersResponse.total, call.TotalUsers());
        }

        [Test, Category("GET")]
        [TestCase(1, "george.bluth@reqres.in", "George", "Bluth", "https://reqres.in/img/faces/1-image.jpg")]
        [TestCase(2, "janet.weaver@reqres.in", "Janet", "Weaver", "https://reqres.in/img/faces/2-image.jpg")]
        [TestCase(3, "emma.wong@reqres.in", "Emma", "Wong", "https://reqres.in/img/faces/3-image.jpg")]

        public void GetSingleUser(int id, string email, string firstname, string lastname, string avatar)
        {
            var call = new Helper<SingleUser>();
            SingleUser user = call.ResponseClass(call.GetSingleUser(id));

            Assert.IsNotNull(user);
            Assert.AreEqual(user.data.id, id);
            Assert.AreEqual(user.data.email, email);
            Assert.AreEqual(user.data.first_name, firstname);
            Assert.AreEqual(user.data.last_name, lastname);
            Assert.AreEqual(user.data.avatar, avatar);
        }

        [Test, Category("GET")]
        public void InvalidUserID()
        {
            var call = new APICalls();
            Assert.AreEqual(call.GetSingleUser(999).StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [Test, Category("POST")]
        [TestCase("Tony", "Tester")]
        [TestCase("Dan", "Developer")]
        public void UserCreation(string UserName, string Job)
        {
            var call = new Helper<CreatedUser>();
            RestResponse CreateUserResponse = call.CreateUser(new { name = UserName, job = Job });
            CreatedUser user = call.ResponseClass(CreateUserResponse);

            Assert.AreEqual(UserName, user.name);
            Assert.AreEqual(Job, user.job);
        }

        [Test, Category("DELETE")]
        public void DeleteUser()
        {
            var call = new APICalls();
            Assert.IsTrue(call.DeleteUser(1).IsSuccessful);
        }

        //not using PATCH because the implementation in reqres.in results in identical behavior
        [Test, Category("PUT")]
        [TestCase(1, "Mike", "Manager")]
        [TestCase(2, "Andy", "Analyst")]
        public void UpdateUser(int ID, string UserName, string Job)
        {
            DateTime CurrentTime = DateTime.UtcNow;
            var call = new Helper<UpdatedUser>();
            RestResponse UpdateUserResponse = call.UpdateUser(ID, new { name = UserName, job = Job });

            UpdatedUser user = call.ResponseClass(UpdateUserResponse);

            Assert.AreEqual(UserName, user.name);
            Assert.AreEqual(Job, user.job);
            Assert.Greater(user.updatedAt, CurrentTime);
        }

        //method seems to work only with hardcoded data
        [Test, Category("POST")]
        [TestCase("eve.holt@reqres.in", "pistol")]
        public void RegisterUser(string email, string password)
        {
            var call = new Helper<RegisteredUser>();
            RestResponse RegisteredUserResponse = call.RegisterUser(new { email = email, password= password });
            RegisteredUser user = call.ResponseClass(RegisteredUserResponse);
            Assert.AreEqual(user.id, 4);
            Assert.AreEqual(user.token, "QpwL5tke4Pnpja7X4");
        }

    }
}