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
            NamedPipeServerStream server = new NamedPipeServerStream("adiPipe",
                PipeDirection.InOut,
                1,
                PipeTransmissionMode.Byte
                );
            char[] buf = new char[1024];
            byte[] bytes = new byte[1024];
            await server.WaitForConnectionAsync();
            //using (StreamReader reader = new StreamReader(server))
            //{
            //    using (StreamWriter writer = new StreamWriter(server))
            //    {
            try
            {
                await server.FlushAsync();
                while (true)
                {

                    await server.WriteAsync("pop".ToBytes(), 0, "pop".Length);
                    await server.FlushAsync();

                    var read = await server.ReadAsync(bytes, 0, bytes.Length);
                    var data = Encoding.UTF8.GetString(bytes, 0, read);
                    File.AppendAllLines(@"D:/server",
                        new string[] { $"Date:{DateTime.Now.ToString()}\tMessage:{data}" }
                        );
                    await Task.Delay(10000);

                }
            }
            catch (Exception ex)
            {

                throw;
            }


            //    }
            //}
        }
    }
}
