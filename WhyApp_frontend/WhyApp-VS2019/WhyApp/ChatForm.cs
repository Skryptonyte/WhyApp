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
        public ChatForm(int chatroomID, int userID)
        {
            InitializeComponent();
            this.chatroomID = chatroomID;
            this.userID = userID;
            testChat();
        }

        public async Task<int> testChat()
        {
            this.client = new SocketIO("ws://127.0.0.1:5000/");
            client.OnConnected += async (sender, e) =>
            {
                var joinDetails = new Dictionary<string, string>
                {
                { "user_id", userID.ToString() },
                { "room_id", chatroomID.ToString()},
                };

                string joinJSONStr = JsonConvert.SerializeObject(joinDetails);
                Console.WriteLine("Connected to Server");
                await client.EmitAsync("joinChat",joinJSONStr);
            };

            client.On("recievePost", response =>
            {
                var jsonstr = response.GetValue<string>();
                //var jsonstr = response.ToString();
                Console.WriteLine("Got receieved Chat: "+jsonstr);
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonstr);

                updateChat(dt);
            });

            await client.ConnectAsync();
            return 0;
        }
        public void updateChat( DataTable dt)
        {

            if (richTextBox1.InvokeRequired)
            {
                Action safeUpdate = delegate { updateChat(dt); };
                richTextBox1.Invoke(safeUpdate);
                return;
            }

            int t = dt.Rows.Count;
            for (int i = 0; i < t; i++)
            {
                DataRow dr = dt.Rows[i];
                richTextBox1.Text += "[" + dr["createDate"] + "]" + " " + dr["username"].ToString() + ": " + dr["content"].ToString() + "\n";

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        async Task<int> sendPost(int user_id, int room_id, string content)
        {
            var chatPost = new Dictionary<string, string>
            {
                { "user_id", user_id.ToString() },
                { "room_id", room_id.ToString()},
                { "content", content }
            };
            
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
