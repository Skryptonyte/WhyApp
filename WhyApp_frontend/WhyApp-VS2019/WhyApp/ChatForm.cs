using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Websocket.Client;

namespace WhyApp
{
    public partial class ChatForm : Form
    {
  
        int chatroomID;
        int userID;
        SocketIO client;
        string domainName;
        public ChatForm(string domainName, int chatroomID, string roomName, int userID)
        {
            InitializeComponent();
            this.domainName = domainName;
            this.chatroomID = chatroomID;
            this.userID = userID;
            this.Text = "Chat Room: " + roomName;
            this.postButton.Enabled = false;
            testChat();
        }

        public async Task<int> testChat()
        {
            int i = await fillChatRequest();

            if (i != 1)
            {
                MessageBox.Show("Couldn't connect to server :(");
                return 0;
            }
            this.client = new SocketIO($"ws://{domainName}/");
            client.OnConnected += async (sender, e) =>
            {
                toggleButtonEnable(true);
                var joinDetails = new Dictionary<string, string>
                {
                { "user_id", userID.ToString() },
                { "room_id", chatroomID.ToString()},
                };

                string joinJSONStr = JsonConvert.SerializeObject(joinDetails);
                Console.WriteLine("Connected to Server");
                await client.EmitAsync("joinChat",joinJSONStr);
            };

            client.OnDisconnected += async (sender, e) =>
            {

                toggleButtonEnable(false);
                Console.WriteLine("WARN: Backend server disconnected");
            };
            client.On("recievePost",  response =>
            {
                var jsonstr = response.GetValue<string>();
                //var jsonstr = response.ToString();
                Console.WriteLine("Got receieved Chat: "+jsonstr);
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonstr);

                updateChat(dt);
            });

            await client.ConnectAsync();
            return 1;
        }

        public async Task<int> fillChatRequest()
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.GetAsync($"http://{domainName}/api/posts/{chatroomID}");

            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(rawstr);

            int t = dt.Rows.Count;

            updateChat(dt);

            return 1;
        }

        public void toggleButtonEnable(bool val)
        {
            if (postButton.InvokeRequired)
            {
                Action safeToggle = delegate { toggleButtonEnable(val); };
                postButton.Invoke(safeToggle);
                return;
            }

            postButton.Enabled = val;
        }
        public void updateChat( DataTable dt)
        {

            if (richTextBox1.InvokeRequired)
            {
                Action safeUpdate = delegate { updateChat(dt); };
                richTextBox1.Invoke(safeUpdate);
                return;
            }
            string chatFull = "";
            int t = dt.Rows.Count;
            for (int i = 0; i < t; i++)
            {
                DataRow dr = dt.Rows[i];
                chatFull += "[" + dr["createDate"] + "]" + " " + dr["username"].ToString() + ": " + dr["content"].ToString() + "\n";

            }

            richTextBox1.Text += chatFull;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        async Task<int> sendPost(int user_id, int room_id, string content)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
                return -1;
            var chatPost = new Dictionary<string, string>
            {
                { "user_id", user_id.ToString() },
                { "room_id", room_id.ToString()},
                { "content", content }
            };
            this.textBox1.Text = "";
            await client.EmitAsync("postChat", JsonConvert.SerializeObject(chatPost));
            return 0;
        }
        private async void postButton_Click(object sender, EventArgs e)
        {
            await sendPost(this.userID, this.chatroomID, this.textBox1.Text);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
