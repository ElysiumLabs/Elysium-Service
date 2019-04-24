using Elysium.Extensions;
using Elysium.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elysium.Stats
{
    public class ServiceStatusMiddleware
    {
        private RequestDelegate _next;
        private readonly Service _service;

        public ServiceStatusMiddleware(RequestDelegate next, IService service)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _service = service as Service ?? throw new ArgumentNullException(nameof(service));
        }


        public async Task Invoke(HttpContext context)
        {
            if (!(context.Request.Path == "/stats"))
            {
                // jump to the next middleware
                await _next.Invoke(context);
                return;
            }

            await context.WriteModelAsync(_service.Children.Where(x => x.Options.Discoverable).AsPresentation());

            // do your stuff
        }
    }
}
