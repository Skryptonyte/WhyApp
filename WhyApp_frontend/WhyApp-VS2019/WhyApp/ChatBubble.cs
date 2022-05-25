using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyApp
{
    public partial class ChatBubble : UserControl
    {
        public ChatBubble()
        {
            InitializeComponent();
            this.label1.MaximumSize = new Size(this.Size.Width - 20, 0);
        }

        public void setMessage(string message)
        {
            this.label1.Text = message;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
