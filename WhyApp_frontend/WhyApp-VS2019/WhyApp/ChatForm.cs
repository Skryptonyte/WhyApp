using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        string attachedFile = "";
        int loadedAttachID;
        public ChatForm(string domainName, int chatroomID, string roomName, int userID)
        {
            InitializeComponent();
            this.FormClosing += FormClosed;

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

                updateChat2(dt);
            });
            client.On("errorMessage", response =>
            {
            string errMsg = response.GetValue<string>();
            MessageBox.Show(errMsg, "Error");

            this.Invoke((MethodInvoker) delegate
                    {
                    this.Close();
            });
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

            updateChat2(dt);

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

        public async void updateChat2(DataTable dt)
        {

            if (chatPanel.InvokeRequired)
            {
                Action safeUpdate = delegate { updateChat2(dt); };
                chatPanel.Invoke(safeUpdate);
                return;
            }
            string chatFull = "";
            int t = dt.Rows.Count;

            chatPanel.SuspendLayout();

            // ib.loadImage("C:/Users/rayha/Pictures/Pictures/V0AE3xf.jpg");

            for (int i = 0; i < t; i++)
            {
                int attach_id;
                
                DataRow dr = dt.Rows[i];

                int.TryParse(dr["attach_id"].ToString(), out attach_id);

                string userStr = dr["username"].ToString() + " [" + dr["rank_name"] + "]";
                //string chatLine = "[" + dr["createDate"] + "]" + " " + dr["username"].ToString() + "[" + dr["rank_name"] +"]" +": " + dr["content"].ToString() + "\n";
                string chatLine = dr["content"].ToString();
                ChatBubble cb = new ChatBubble();
                cb.setMessage(chatLine);
                cb.setUser(userStr, dr["rank_color"].ToString());

                chatPanel.Controls.Add(cb);

                if (attach_id != -1)
                {
                    try
                    {
                        ImageChatBubble ib = new ImageChatBubble();
                        Console.WriteLine("Attach ID: " + attach_id);
                        ib.loadImage($"http://{domainName}/api/download/{attach_id}");
                        chatPanel.Controls.Add(ib);
                    }
                    catch
                    {
                        Console.WriteLine("WARN: Failed to load attachment");
                    }
                }
            }

            
            chatPanel.ResumeLayout();
            chatPanel.VerticalScroll.Value = chatPanel.VerticalScroll.Maximum;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        async Task<int> sendPost(int user_id, int room_id, string content)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
                return -1;

            
            int attach_id = -1;
            if (!string.IsNullOrEmpty(attachedFile))
            {
                Stream stream = File.Open(attachedFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamContent sc = new StreamContent(stream);
                HttpClient hc = new HttpClient();

                HttpResponseMessage response = await hc.PostAsync($"http://{domainName}/api/upload", sc);

                response.EnsureSuccessStatusCode();

                string rawstr = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Receieved Attach ID" + rawstr);
                int.TryParse(rawstr, out attach_id);
            }

            

            var chatPost = new Dictionary<string, string>
            {
                { "user_id", user_id.ToString() },
                { "room_id", room_id.ToString()},
                { "content", content },
                { "attach_id", attach_id.ToString() }
            };
            this.textBox1.Text = "";
            await client.EmitAsync("postChat", JsonConvert.SerializeObject(chatPost));

            this.attachedFile = "";
            this.attachLabel.Text = "";
            return 0;
        }
        private async void postButton_Click(object sender, EventArgs e)
        {
            await sendPost(this.userID, this.chatroomID, this.textBox1.Text);
        }

        private async void attachButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();

            if (of.ShowDialog() == DialogResult.OK)
            {
                string fn = of.FileName;
                this.attachedFile = fn;
                this.attachLabel.Text = fn;
            }
        }

        private void FormClosed(object sender, FormClosingEventArgs e )
        {
            this.client.DisconnectAsync();

        }

    }
}
