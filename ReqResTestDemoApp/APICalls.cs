using RestSharp;


namespace ReqResTestDemoApp
{
    public class APICalls
    {


        private RestClient client;
        private RestResponse response;
        private RestRequest request;

        private string UsersEndpoint = "/api/users/";
        private string RegisterEndpoint = "/api/register";
        private string LoginEndpoint = "/api/login";

        private const string BaseURL = "https://reqres.in/";


        public RestResponse GetSingleUser(int UserID)
        {
            client = new RestClient(BaseURL);
            request = new RestRequest(UsersEndpoint + UserID.ToString());
            response = client.GetAsync(request).Result;
            return response;
        }
        public RestResponse GetUserPage(int PageNumber = 0)
        {
            client = new RestClient(BaseURL);
            string FinalEndpoint = PageNumber != 0 ? UsersEndpoint + "?page=" + PageNumber.ToString() : UsersEndpoint;
            request = new RestRequest(FinalEndpoint);
            response = client.GetAsync(request).Result;
            return response;
        }
        public RestResponse DeleteUser(int UserID)
        {
            client = new RestClient(BaseURL);
            request = new RestRequest(UsersEndpoint + UserID.ToString());
            response = client.DeleteAsync(request).Result;
            return response;
        }

        public RestResponse CreateUser(object content)
        {
            client = new RestClient(BaseURL);
            request = new RestRequest(UsersEndpoint);
            request.AddJsonBody(content);
            response = client.PostAsync(request).Result;
            return response;
        }

        public RestResponse UpdateUser(int id, object content)
        {
            client = new RestClient(BaseURL);
            request = new RestRequest(UsersEndpoint+id.ToString());
            request.AddJsonBody(content);
            response = client.PutAsync(request).Result;
            return response;
        }

        public RestResponse RegisterUser(object content)
        {
            client = new RestClient(BaseURL);
            request = new RestRequest(RegisterEndpoint);
            request.AddJsonBody(content);
            response = client.PostAsync(request).Result;
            return response;
        }

        public RestResponse LoginUser(object content)
        {
            client = new RestClient(BaseURL);
            request = new RestRequest(LoginEndpoint);
            request.AddJsonBody(content);
            response = client.PostAsync(request).Result;
            return response;
        }


    }
}
