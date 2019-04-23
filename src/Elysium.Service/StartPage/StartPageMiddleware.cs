using Elysium;
using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
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

            PathString matchPath = "";

            var services = context.GetServiceChain();

            services.Select(x => x.Options.Branch).ToList().ForEach(x => matchPath += x.AsServiceSegment());

            matchPath = matchPath.Value.AsServiceSegment();

            HttpRequest request = context.Request;
            var currentPath = new PathString(request.PathBase + request.Path).Value.AsServiceSegment();
            if (matchPath == currentPath)
            {
                // Dynamically generated for LOC.
                var welcomePage = new StartPage();
                return welcomePage.ExecuteAsync(context, _options, context.RequestServices.GetElysiumHostService());

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
