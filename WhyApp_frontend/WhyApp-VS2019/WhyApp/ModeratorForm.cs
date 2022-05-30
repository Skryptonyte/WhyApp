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
        int modid;
        public ModeratorForm(string domainName, int modid)
        {
            InitializeComponent();
            this.domainName = domainName;
            this.modid = modid;
        }

        private async void ban_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.banUserTextbos.Text))
            {
                MessageBox.Show("Please enter userID.", "Notice");
                return;
            }

        
            var banReq = new Dictionary<String, String>
            {
                {"m_id", this.modid.ToString() }   ,
                {"user_id", banUserTextbos.Text },
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

        private async void delPostButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(postManageBox.Text))
            {
                MessageBox.Show("Please enter Post ID");
                return;
            }
            var deletePostReq = new Dictionary<String, String>
            { {"m_id",  this.modid.ToString()},
                {"post_id", postManageBox.Text.ToString() }
            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(deletePostReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.PostAsync($"http://{domainName}/api/posts/delete", stringContent);


            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();

            MessageBox.Show(rawstr, "Info");
        }

        private void modPostButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(postManageBox.Text))
            {
                MessageBox.Show("Please enter Post ID");
                return;
            }
            ModContentForm mcf = new ModContentForm(domainName, postManageBox.Text, modid);
            mcf.Show();
        }

        private async void displayPostButton_Click(object sender, EventArgs e)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.GetAsync($"http://{domainName}/api/posts");

            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(rawstr);

            gridForm gf = new gridForm(dt);
            DataGridView dgv = gf.GetGridView();

            dgv.Columns["post_id"].DisplayIndex = 0;
            dgv.Columns["room_id"].DisplayIndex = 1;
            dgv.Columns["user_id"].DisplayIndex = 2;
            dgv.Columns["content"].DisplayIndex = 3;
            gf.Show();
        }
    }
}
