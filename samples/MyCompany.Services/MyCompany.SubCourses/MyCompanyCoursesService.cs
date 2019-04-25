using Elysium;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.SubCourses
{
    public class MyCompanySubCoursesService : Service
    {
        public MyCompanySubCoursesService(IHostingEnvironment environment, IConfiguration configuration, ILogger<Service> logger = null) : base(environment, configuration, logger)
        {
        }


        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

          
        }

        public override void Configure(IApplicationBuilder app)
        {
            

            app.UseMvc();
        }
    }
}
