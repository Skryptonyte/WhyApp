using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyApp
{
    public partial class ImageChatBubble : UserControl
    {
        public ImageChatBubble()
        {
            InitializeComponent();
        }

        public async void loadImage(string url)
        {
            /*
            var request = WebRequest.Create(url);
            Console.WriteLine("Writing image");
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                 pictureBox1.Image = Bitmap.FromStream(stream);
            }
            */

            pictureBox1.LoadAsync(url);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
