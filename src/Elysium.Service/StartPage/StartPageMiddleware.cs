using Elysium;
using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elysium.StartPage
{
    public class StartPageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly StartPageOptions _options;

        
        public StartPageMiddleware(RequestDelegate next, IOptions<StartPageOptions> options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options.Value;
        }

        public Task InvokeAsync(HttpContext context)
        {

            HttpRequest request = context.Request;

            //not asking for a homepage
            if (!(request.Path == "" || request.Path == "/"))
            {
                return _next(context);
            }

            PathString matchPath = "";

            var services = new List<Service>();
            var midService = context.RequestServices.GetElysiumHostService();
            var pService = midService;

            do
            {
                services.Add(pService);
                pService = pService.Parent;
            }
            while (pService != null);

            services.Reverse();

            services.Select(x => x.Options.GetBranch()).ToList().ForEach(x => matchPath = matchPath.Add(x));

            matchPath = matchPath.Value.AsServiceSegment();

            
            var currentPath = new PathString(request.PathBase + request.Path).Value.AsServiceSegment();
            if (matchPath == currentPath)
            {
                // Dynamically generated for LOC.
                var startPageProvider = context.RequestServices.GetService<IStartPageProvider>();
                var welcomePage = startPageProvider?.GetStartPage() ?? new StartPage();
                return welcomePage.ExecuteAsync(context, _options, midService);

            }

            return _next(context);
        }

    }

    public static class PathStringExtensions
    {
        public static string AsServiceSegment(this string branch)
        {
            if (!branch.StartsWith("/"))
            {
                branch = "/" + branch;
            }

            if (!branch.EndsWith("/"))
            {
                branch += "/";
            }

            return branch;
        }
    }
 


}
