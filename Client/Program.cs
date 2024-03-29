﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
using Utils;
using System.Diagnostics;

namespace Client
{
    static class Program
    {


        static async Task Main()
        {
            State state = new State();
            PopForm form = new PopForm(state);
            NamedPipeClientStream client = new NamedPipeClientStream(
                ".",
                "adiPipe",
                PipeDirection.InOut,
                PipeOptions.Asynchronous
                );
            char[] buf = new char[1024];
            byte[] bytes = new byte[1024];


            try
            {
                Debugger.Launch();
                await client.ConnectAsync().ConfigureAwait(false);
                while (true)
                {
                    // var got = await reader.ReadToEndAsync().ConfigureAwait(false);
                    var read = client.Read(bytes, 0, bytes.Length);
                    var got = bytes.ToChars(read).FromChars(read);
                    if (got != "pop")
                    {
                        continue;
                    }
                    
                    form.ShowDialog();
                    var message = state.WasClosed ? "close" : "delay";
                    var tosend = message.ToBytes();
                    client.Write(tosend, 0, tosend.Length);
                    // await writer.WriteAsync(message);

                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
