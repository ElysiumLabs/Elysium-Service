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

        public virtual async Task<string> GetPageStringAsync()
        {
            var assembly = typeof(StartPage).Assembly;
            var resourceStream = assembly.GetManifestResourceStream("Elysium.Assets.index.html");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public virtual async Task ExecuteAsync(HttpContext context, StartPageOptions _options, Service hostedService = null)
        {
            var s = await GetPageStringAsync();

            var serviceName = _options.ServiceName;

            if (!string.IsNullOrEmpty(_options.ApplicationName))
            {
                serviceName = _options.ApplicationName + " - " + serviceName;
            }

            s = s.Replace("{{ServiceName}}", _options.ServiceName);

            var servicesString = "<table style='width: 100 % '>";

            servicesString += "<tr><th> Service </th><th> Branch </th><th> Status </th></tr>";

            hostedService?.Children.Where(x => x.Options.Discoverable).ToList().ForEach(x =>
            {
                servicesString += "<tr><td> " + x.Options.Name + " </td><td> /" + x.Options.Branch + " </td><td> " + Enum.GetName(typeof(ServiceState), x.Status.State) + " </td></tr>";
            });

            servicesString += "</table>";

            s = s.Replace("{{Services}}", servicesString);

            await context.Response.WriteAsync(s);


        }
    }




}
