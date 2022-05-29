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
    public partial class AdminForm : Form
    {
        string domainName;
        public AdminForm(string domainName)
        {
            this.domainName = domainName;
            InitializeComponent();
        }

        private async void addRoomButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.addroomnamebox.Text))
            {
                MessageBox.Show("Please enter Room Name.", "Notice");
                return;
            }

            if (string.IsNullOrEmpty(this.roomDescBox.Text))
            {
                MessageBox.Show("Please enter Room Description.", "Notice");
                return;
            }


            var banReq = new Dictionary<String, String>
            { {"room_name", addroomnamebox.Text },
                {"room_desc", roomDescBox.Text },
            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(banReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.PostAsync($"http://{domainName}/api/rooms/create", stringContent);

            resp.EnsureSuccessStatusCode();

            string responseText = await resp.Content.ReadAsStringAsync();
            MessageBox.Show(responseText, "Info");
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {

            var banReq = new Dictionary<String, String>
            { {"room_id", deleteRoomIDBox.Text },            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(banReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.PostAsync($"http://{domainName}/api/rooms/delete", stringContent);

            resp.EnsureSuccessStatusCode();

            string responseText = await resp.Content.ReadAsStringAsync();
            MessageBox.Show(responseText, "Info");
        }

        private async void listRoomButton_Click(object sender, EventArgs e)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.GetAsync($"http://{domainName}/api/rooms");

            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(rawstr);

            gridForm gf = new gridForm(dt);
            gf.Show();

        }

        private async void purgePostButton_Click(object sender, EventArgs e)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.DeleteAsync($"http://{domainName}/api/posts/deleteAll");

            string responseText = await resp.Content.ReadAsStringAsync();
            MessageBox.Show(responseText, "Info");
        }

        private async void listModButton_Click(object sender, EventArgs e)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.GetAsync($"http://{domainName}/api/moderators");

            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(rawstr);

            gridForm gf = new gridForm(dt);
            gf.Show();
        }

        private void modButton_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm(domainName, 1);
            rf.Show();
        }

        private async void permitButton_Click(object sender, EventArgs e)
        {

            var banReq = new Dictionary<String, String>
            { {"room_id", roomPermitButton.Value.ToString() },
                {"mod_id",  modIDPermitButton.Value.ToString() },
                { "modifyPerm",  (modBox.Checked ? 1: 0).ToString() },
                { "deletePerm",  (delBox.Checked ? 1: 0).ToString() },
                { "banPerm",  (banBox.Checked? 1: 0).ToString() }
                
            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(banReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.PostAsync($"http://{domainName}/api/moderators/permit", stringContent);

            resp.EnsureSuccessStatusCode();

            string responseText = await resp.Content.ReadAsStringAsync();
            MessageBox.Show(responseText, "Info");
        }

        private async void revokeButton_Click(object sender, EventArgs e)
        {
            var banReq = new Dictionary<String, String>
            { {"room_id", roomPermitButton.Value.ToString() },
                {"mod_id",  modIDPermitButton.Value.ToString() },

            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(banReq), Encoding.UTF8, "application/json");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.PostAsync($"http://{domainName}/api/moderators/revoke", stringContent);

            resp.EnsureSuccessStatusCode();

            string responseText = await resp.Content.ReadAsStringAsync();
            MessageBox.Show(responseText, "Info");
        }
    }
}
