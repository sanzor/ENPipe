using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class PopForm : Form
    {
        private State state;
        public PopForm(State state)
        {
            this.state = state;
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.state.WasDelayed = false;
            this.state.WasClosed = true;
            this.Close();
        }

        private void delayBtn_Click(object sender, EventArgs e)
        {
            this.state.WasClosed = false;
            this.state.WasDelayed = true;
            this.Close();
        }
    }
}
