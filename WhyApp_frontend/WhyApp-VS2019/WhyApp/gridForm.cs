using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyApp
{
    public partial class gridForm : Form
    {
        public gridForm(DataTable dt)
        {
            InitializeComponent();

            this.dataGridView1.DataSource = dt;
        }

        public DataGridView GetGridView()
        {
            return this.dataGridView1;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
