using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using Newtonsoft.Json;
using Utils;
using System.Threading;
using System.Diagnostics;
using Topshelf;

namespace Server
{
    class Program
    {
        
        
        static async Task Main(string[] args)
        {
            HostFactory.Run(hostConfig =>
            {
                hostConfig.Service<Serv>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(() => new Serv());
                    serviceConfig.WhenStarted(s => s.Run());
                    serviceConfig.WhenStopped(s => s.Stop());
                    serviceConfig.WhenSessionChanged((x, y) => x.OnSessionChanged(y));
                });
                hostConfig.SetServiceName("KService");
                hostConfig.SetDisplayName("K_Service");
                hostConfig.SetDescription("Test Service for launching winforms in user session");
                
            });

        }
    }
   
}

