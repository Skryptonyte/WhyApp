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

        public void setUser(string userString, string colour_name="blue")
        {
            this.label2.Text = userString;
            this.label2.ForeColor = Color.FromName(colour_name);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
