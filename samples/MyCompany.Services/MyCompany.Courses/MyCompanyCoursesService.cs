using Elysium;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.SubCourses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.Courses
{
    public class MyCompanyCoursesService : Service
    {
        public MyCompanyCoursesService(IHostingEnvironment environment, IConfiguration configuration, ILogger<Service> logger = null) : base(environment, configuration, logger)
        {

        }


        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddElysiumService<MyCompanySubCoursesService>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseElysiumService<MyCompanySubCoursesService>("sub");

            app.UseMvc();
        }
    }
}
