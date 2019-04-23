using Elysium;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elysium.StartPage
{
    public class StartPage
    {
        public StartPage()
        {

        }

        public async Task ExecuteAsync(HttpContext context, StartPageOptions _options, Service hostedService = null)
        {
            var assembly = typeof(StartPage).Assembly;
            var resourceStream = assembly.GetManifestResourceStream("Elysium.Service.Assets.index.html");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var s = await reader.ReadToEndAsync();

                s = s.Replace("{{ApplicationName}}", _options.ApplicationName);
                s = s.Replace("{{ServiceName}}", _options.ServiceName);

                var servicesString = "<table style='width: 100 % '>";

                servicesString += "<tr><th> Service </th><th> Branch </th><th> Status </th></tr>";

                hostedService?.Children.ToList().ForEach(x => {
                    servicesString += "<tr><td> "+x.Options.Application+ " </td><td> /" +x.Options.Branch + " </td><td> " + Enum.GetName(typeof(ServiceStatus), x.Status.State)  + " </td></tr>";
                });

                servicesString += "</table>";

                s = s.Replace("{{Services}}", servicesString);

                await context.Response.WriteAsync(s);

            }
        }
    }




}
