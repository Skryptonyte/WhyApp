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
    public partial class ModeratorForm : Form
    {
        string domainName;
        public ModeratorForm(string domainName)
        {
            InitializeComponent();
            this.domainName = domainName;
        }

        private async void ban_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.banUserTextbos.Text))
            {
                MessageBox.Show("Please enter userID.", "Notice");
                return;
            }

        
            var banReq = new Dictionary<String, String>
            { {"user_id", banUserTextbos.Text },
                {"ban_reason", reasonTB.Text },
                {"room_id", roomIDBox.Value.ToString() },
                {"minute", minuteBox.Value.ToString() },
                {"hour", hourBox.Value.ToString() }
            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(banReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.PostAsync($"http://{domainName}/api/users/ban",stringContent);

            resp.EnsureSuccessStatusCode();
            string responseText = await resp.Content.ReadAsStringAsync();
            MessageBox.Show(responseText);

        }

        private async void unbanButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.banUserTextbos.Text))
            {
                MessageBox.Show("Please enter userID.", "Notice");
                return;
            }


            var banReq = new Dictionary<String, String>
            { {"user_id", banUserTextbos.Text },
                {"room_id", roomIDBox.Value.ToString() }
            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(banReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.PostAsync($"http://{domainName}/api/users/unban", stringContent);

            resp.EnsureSuccessStatusCode();
            string responseText = await resp.Content.ReadAsStringAsync();
            MessageBox.Show(responseText);
        }

        private async void displayUserButton_Click(object sender, EventArgs e)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.GetAsync($"http://{domainName}/api/users");

            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(rawstr);

            gridForm gf = new gridForm(dt);
            gf.Show();

        }
    }
}
