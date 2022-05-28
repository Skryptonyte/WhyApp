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
    public partial class RegisterForm : Form
    {
        string domainName;
        int mode = 0;
        public RegisterForm(string domainName, int mode = 0)
        {
            this.mode = mode;
            this.domainName = domainName;
            InitializeComponent();
        }

        private async Task<int> sendRegistration(string username, string password)
        {
            var registrationDict = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password},
            };

            var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(registrationDict), Encoding.UTF8, "application/json");
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient hc = new HttpClient();
            HttpResponseMessage response;
            if (mode == 1)
                response = await hc.PostAsync($"http://{domainName}/api/moderators/register", stringContent);
            else
                response = await hc.PostAsync($"http://{domainName}/api/register", stringContent);
            response.EnsureSuccessStatusCode();

            HttpContent resultString = response.Content;
            string rawstr = await resultString.ReadAsStringAsync();

            int r = 0;
            int.TryParse(rawstr, out r);

            return r;
        }
        private async void registerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userBox.Text))
            {
                MessageBox.Show("Username is empty");
                return;
            }

            if (string.IsNullOrEmpty(passBox.Text))
            {
                MessageBox.Show("Password is empty");
                return;
            }
            if (string.IsNullOrEmpty(confirmPassBox.Text))
            {
                MessageBox.Show("Please confirm password");
                return;
            }

            if (confirmPassBox.Text != passBox.Text)
            {
                MessageBox.Show("Password confirmation does not match");
            }

            int result = await sendRegistration(userBox.Text, passBox.Text);

            if (result == 1)
            {
                MessageBox.Show("User created successfully");
            }
            else
            {
                MessageBox.Show("Couldn't create user :/");
            }
            
       }
    }
}
