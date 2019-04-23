using Microsoft.AspNetCore.Http;

namespace Elysium.StartPage
{
    public class StartPageOptions
    {
        public string ApplicationName { get; set; } = "Service";

        public string ServiceName { get; set; } = "";

        public PathString Path { get; set; } = "/";
    }

 


}
