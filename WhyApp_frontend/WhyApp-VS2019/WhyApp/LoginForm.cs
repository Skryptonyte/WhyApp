using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WhyApp
{
    public partial class LoginForm : Form
    {
        string domainName = "localhost:5000";
        public LoginForm()
        {
            InitializeComponent();
            loginAltBox.SelectedIndex = 0;
        }

        async Task<int> loginRequest(string username, string password, int mode=0)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage response;
            
            if (mode==1)
                response = await hc.GetAsync($"http://{domainName}/api/moderator/login?username={username}&password={password}");
            else if (mode == 2)
                response = await hc.GetAsync($"http://{domainName}/api/administrator/login?username={username}&password={password}");
            else
                response = await hc.GetAsync($"http://{domainName}/api/login?username={username}&password={password}");
            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string resultCode = await content.ReadAsStringAsync();

            int userID = -1;
            int.TryParse(resultCode, out userID);


            return userID;
        }
        private async void loginButton_Click(object sender, EventArgs e)
        {

                if (string.IsNullOrEmpty(usernameBox.Text) || string.IsNullOrEmpty(passBox.Text))
                {
                    MessageBox.Show("Both username and password are mandatory", "Notice");
                    return;
                }
                int userID = -1;
            if (loginAltBox.SelectedIndex == 0)
            {
                try
                {
                    userID = await loginRequest(usernameBox.Text, passBox.Text);
                }
                catch
                {
                    MessageBox.Show($"Unable to connect to domain {domainName}", "Error");
                    return;
                }
                if (userID >= 0)
                {
                    RoomForm rf = new RoomForm(domainName, userID);
                    rf.Show();
                }
                else if (userID == -1)
                {
                    MessageBox.Show("Incorrect username/password!", "Auth Failed");
                }
            }

            else if (loginAltBox.SelectedIndex == 1)
            {
                try
                {
                    userID = await loginRequest(usernameBox.Text, passBox.Text,1);
                }
                catch
                {
                    MessageBox.Show($"Unable to connect to domain {domainName}", "Error");
                    return;
                }
                if (userID >= 0)
                {
                    ModeratorForm mf = new ModeratorForm(domainName);
                    mf.Show();
                }
                else if (userID == -1)
                {
                    MessageBox.Show("Incorrect username/password!", "Auth Failed");
                }
            }
            else if (loginAltBox.SelectedIndex == 2)
            {
                try
                {
                    userID = await loginRequest(usernameBox.Text, passBox.Text, 2);
                }
                catch
                {
                    MessageBox.Show($"Unable to connect to domain {domainName}", "Error");
                    return;
                }
                if (userID >= 0)
                {
                    AdminForm mf = new AdminForm(domainName);
                    mf.Show();
                }
                else if (userID == -1)
                {
                    MessageBox.Show("Incorrect username/password!", "Auth Failed");
                }
            }
        }

        

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm(domainName);
            rf.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.domainName = this.textBox1.Text;
        }


    }

    
}
