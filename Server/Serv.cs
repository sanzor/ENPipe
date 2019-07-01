using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using Utils;

namespace Server
{
    public class Serv
    {
        private const string STD_ERR = @"D://err.txt";
        private string Data = "LALALA";
        public Serv()
        {

        }
        private const string CLIENT_PATH = @"C:\Users\DskAdrianPC\source\repos\Pipes\Client\bin\Debug\Client";
        private Process process;
        private NamedPipeServerStream pipe;

        public void Stop()
        {
            
            Kill();
        }
        public void Kill()
        {
            if (process != null)
            {
                process.Kill();
                process.Dispose();
                process = null;
                Debugger.Log(1, "DISPOSED", "Disposed process");
            }

        }
        public void OnSessionChanged(SessionChangedArguments args)
        {
            Debugger.Log(1, "session", "Session changed");
            File.AppendAllText(STD_ERR,$"Time:{DateTime.Now.ToShortTimeString()}\tMessage:SessionChanged to:" + args.SessionId);
        }
        public void StartProcess()
        {
            Kill();
            process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = CLIENT_PATH
            };
            process.Start();


        }
        private void StartPipe()
        {
            if (pipe != null)
            {
                pipe.Dispose();
                pipe = null;
            }
            pipe = new NamedPipeServerStream(
            "adiPipe",
            PipeDirection.InOut,
            3,
            PipeTransmissionMode.Message
            );
            pipe.WaitForConnection();
        }
        public void Run()
        {
            char[] buf = new char[1024];
            byte[] bytes = new byte[1024];
            Restart:
            StartProcess();
            StartPipe();
            try
            {
                while (true)
                {
                    Debugger.Launch();
                    //await writer.WriteAsync("pop").ConfigureAwait(false);

                    pipe.Write("pop".ToBytes(), 0, "pop".ToBytes().Length);

                    //var read = await reader.ReadToEndAsync().ConfigureAwait(false);
                    var read = pipe.Read(bytes, 0, bytes.Length);
                    // var data = Encoding.UTF8.GetString(bytes, 0, read);
                    var data = Encoding.UTF8.GetString(bytes, 0, read);
                    File.AppendAllLines(@"D:/server.txt",
                        new string[] { $"Date:{DateTime.Now.ToString()}\tMessage:{data}" }
                        );
                    Thread.Sleep(1000);

                }
            }
            catch (Exception)
            {
                goto Restart;

            }

        }




    }
}
