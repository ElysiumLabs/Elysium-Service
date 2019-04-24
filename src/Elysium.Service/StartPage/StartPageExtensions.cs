
using Elysium;
using Elysium.StartPage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class StartPageExtensions
    {
        public static IApplicationBuilder UseStartPage(this IApplicationBuilder app, Action<StartPageOptions> optionsAction = null)
        {
            var option = new StartPageOptions();

            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            optionsAction?.Invoke(option);

            return app.UseMiddleware<StartPageMiddleware>(Options.Create(option));
        }

        public static IApplicationBuilder UseStartPage(this IApplicationBuilder app, ServiceOptions options) 
        {
            return app.UseStartPage(x => 
            {
                x.ApplicationName = options.Application;
                x.ServiceName = options.Name;
            });
        }
    }
}
