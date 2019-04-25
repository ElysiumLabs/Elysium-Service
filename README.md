# Elysium Service

### The russian dolls of ASP.NET Core.

![Visual Studio Team services](https://img.shields.io/vso/build/elysiumlabs/28c50ec9-7531-40fd-8704-f2c618688944/1.svg?style=for-the-badge)
![NuGet](https://img.shields.io/nuget/dt/Elysium.Service.svg?style=for-the-badge)

[![forthebadge](http://forthebadge.com/images/badges/built-with-love.svg)](http://forthebadge.com)
[![forthebadge](http://forthebadge.com/images/badges/contains-cat-gifs.svg)](http://forthebadge.com)
[![forthebadge](http://forthebadge.com/images/badges/designed-in-ms-paint.svg)](http://forthebadge.com)
[![forthebadge](http://forthebadge.com/images/badges/fuck-it-ship-it.svg)](http://forthebadge.com)

Key assumptions:
 - A service it's just a aspnetcore startup service.
 - A service can contains 1 or multiple children services (per path)
 - A service can be easily pluggable in any other project.

Idea ilustration
![Services](https://i.imgur.com/k5U1jeb.jpg)

## Start is super easy:

Like any other aspnetcore, a startup structure.

```csharp
    public class MyCompanyService1 : Service
    {
        //Your constructor in your way.
        //This contructor is a ready configuration access template
        public MyCompanyCoursesService(IHostingEnvironment environment, IConfiguration configuration, ILogger<Service> logger = null) : base(environment, configuration, logger)
        {

        }


        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //...
        }

        public override void Configure(IApplicationBuilder app)
        {
            //...

            app.UseMvc();
        }
    }
```

and in the host builder just use this service in UseStartup, like this:

```csharp
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<MyCompanyService1>();
    }
```

Just it. Run and you have a ~~beautiful~~ default start page (customizable):

![Startup page](https://i.imgur.com/MiMNIak.png)



Join us @ 

<a href="https://discord.gg/6qFrxRQ">
<img src="https://discordapp.com/assets/fc0b01fe10a0b8c602fb0106d8189d9b.png" width="250">
</a>
