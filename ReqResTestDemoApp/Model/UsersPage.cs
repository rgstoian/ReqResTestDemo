namespace ReqResTestDemoApp.Model
{
    public class UsersPage
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<UserData>? data { get; set; }
        public Support? support { get; set; }
    }
}
