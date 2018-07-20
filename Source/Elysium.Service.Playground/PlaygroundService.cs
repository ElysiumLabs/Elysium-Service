using Microsoft.AspNetCore.Builder;

namespace Elysium.Service.Playground
{
    public class PlaygroundService : Elysium.Service.Service
    {
        public PlaygroundService()
        {
            Name = "Playground";
            Version = "1.0.1";
            Application = "Elysium";
        }

        public override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            //app.UseWelcomePage();
        }
    }
}