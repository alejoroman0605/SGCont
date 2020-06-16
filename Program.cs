using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SGCont.Utils;

namespace SGCont
{
    public class Program
    {
        private static IList<ModuleInfo> _modules = new List<ModuleInfo>();
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>();
            //.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "SGCont");
            return host;
        }
    }
}
