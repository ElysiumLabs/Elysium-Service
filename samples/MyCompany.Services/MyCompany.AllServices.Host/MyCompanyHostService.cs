using MyCompany.Courses;
using MyCompany.Identity;
using MyCompany.Music;
using Elysium;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Host
{
    public class MyCompanyHostService : Service
    {
        public MyCompanyHostService(IHostingEnvironment environment, IConfiguration configuration, ILogger<Service> logger = null) : base(environment, configuration, logger)
        {
            Options.Name = "MyCompany Apps";
        }

       

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddElysiumService<MyCompanyIdentityService>();
            services.AddElysiumService<MyCompanyCoursesService>();
            services.AddElysiumService<MyCompanyMusicService>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseElysiumService<MyCompanyIdentityService>("identity");
            app.UseElysiumService<MyCompanyCoursesService>("newcourses");
            app.UseElysiumService<MyCompanyMusicService>("goodmusic");

        }
    }
}
