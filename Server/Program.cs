using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using Newtonsoft.Json;
using Utils;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            NamedPipeServerStream server = new NamedPipeServerStream(
                "adiPipe",
                PipeDirection.InOut,
                3,
                PipeTransmissionMode.Message
                );
            char[] buf = new char[1024];
            byte[] bytes = new byte[1024];
            await server.WaitForConnectionAsync();
            using (StreamReader reader = new StreamReader(server))
            {
                using (StreamWriter writer = new StreamWriter(server))
                {
                    try
                    {
                        
                        while (true)
                        {

                            //await writer.WriteAsync("pop").ConfigureAwait(false);

                            await server.WriteAsync("pop".ToBytes(), 0, "pop".ToBytes().Length);
                            
                            //var read = await reader.ReadToEndAsync().ConfigureAwait(false);
                            var read=await server.ReadAsync(bytes, 0, bytes.Length);
                            // var data = Encoding.UTF8.GetString(bytes, 0, read);
                            var data = Encoding.UTF8.GetString(bytes, 0, read);
                            File.AppendAllLines(@"D:/server.txt",
                                new string[] { $"Date:{DateTime.Now.ToString()}\tMessage:{data}" }
                                );
                            await Task.Delay(1000);

                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                }
            }
        }
    }
}
