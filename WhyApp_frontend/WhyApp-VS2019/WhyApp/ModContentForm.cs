using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyApp
{
    public partial class ModContentForm : Form
    {
        string post_id;
        int mod_id;
        string domainName;
        public ModContentForm(string domainName, string post_id, int mod_id)
        {
            this.domainName = domainName;
            this.post_id = post_id;
            this.mod_id = mod_id;
            InitializeComponent();
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            var deletePostReq = new Dictionary<String, String>
            { {"m_id",  this.mod_id.ToString()},
                {"post_id", this.post_id },
                {"new_content",  this.richTextBox1.Text}
            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(deletePostReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.PostAsync($"http://{domainName}/api/posts/modify", stringContent);


            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();

            MessageBox.Show(rawstr, "Info");

            this.Close();
        }
    }
}
