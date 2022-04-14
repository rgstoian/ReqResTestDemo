
# ReqResTestDemo
#### Simple test solution implementing RestSharp and NUnit, testing endpoints of  https://reqres.in/ 

## Requirements:

 - .NET 6
 - RestSharp 107 or newer
 - NUnit (Latest Stable version)
 - NUnit Test Adapter (Latest Stable version)

## Execution
The tests can be executed from the Visual Studio Test Explorer after an initial build of the solution.

## Methods implemented:
- Smoke test (getting a successful response code from a basic GET method)
- Getting a list of users and verifying the Total Users property
- Getting a single user
- Getting an invalid user
- Updating an user's details
- Deleting an user
- Creating a new user
- Registering a new user
- Logging in an existing user

## Issues/Notes:
- The Register/Login methods only function with the sample email address provided by https://reqres.in/. Any other data results in a BadRequest error.
- Due to the volatility of the site (no users are actually created/updated) the Update User endpoint behaves identically using both the PUT and PATCH methods (a new object is actually created, using the same fields)
