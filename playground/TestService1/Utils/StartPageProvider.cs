using Elysium.StartPage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestService1.Utils
{
    public class StartPageProvider : IStartPageProvider
    {
        public StartPage GetStartPage()
        {
            return new CustomStartPage();
        }
    }

    public class CustomStartPage : StartPage
    {
        public override async Task<string> GetPageStringAsync()
        {
            var assembly = this.GetType().Assembly;
            var resourceStream = assembly.GetManifestResourceStream("TestService1.Assets.index.html");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
