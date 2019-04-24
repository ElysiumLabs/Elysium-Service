using Microsoft.AspNetCore.Http;

namespace Elysium.StartPage
{
    public class StartPageOptions
    {
        public string ApplicationName { get; set; } = "Elysium";

        public string ServiceName { get; set; } = "Service";

        public PathString Path { get; set; } = "/";
    }

 


}
