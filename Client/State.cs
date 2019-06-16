using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class State
    {
        private readonly object @lock = new object();
        private bool wasDelayed;
        private bool wasClosed;
        public bool WasDelayed
        {
            get
            {
                lock (@lock)
                {
                    return this.wasDelayed;
                }
               
            }
            set
            {
                lock (@lock)
                {
                    this.wasDelayed = value;
                }
            }
        }
        public bool WasClosed
        {
            get
            {
                lock (@lock)
                {
                    return this.wasClosed;
                }

            }
            set
            {
                lock (@lock)
                {
                    this.wasClosed = value;
                }
            }
        }
    }
}
