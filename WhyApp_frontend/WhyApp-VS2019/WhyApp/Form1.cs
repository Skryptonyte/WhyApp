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

    public partial class RoomForm : Form
    {
        int userID;
        string domainName;
        public RoomForm( string domainName, int userID)
        {
            this.domainName = domainName;
            InitializeComponent();
            this.userID = userID;
            listRooms();

        }

        async void listRooms()
        {
            Console.WriteLine("Async room list");
            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.GetAsync($"http://{domainName}/api/rooms");

            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string rawstr = await content.ReadAsStringAsync();
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(rawstr);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = (e.RowIndex);
            
//MessageBox.Show("Stub for chatroom " + dataGridView1.Rows[index].Cells[1].Value);
            if (index < 0)
                return;
            int roomID = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
            string roomName = Convert.ToString(dataGridView1.Rows[index].Cells[1].Value);

            ChatForm ch = new ChatForm(domainName, roomID, roomName, userID);
            ch.Show();
        }
    }
}
