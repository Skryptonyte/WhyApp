namespace WhyApp
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.postButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.attachButton = new System.Windows.Forms.Button();
            this.chatPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.attachLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fetchButton = new System.Windows.Forms.Button();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 538);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(743, 20);
            this.textBox1.TabIndex = 1;
            // 
            // postButton
            // 
            this.postButton.Location = new System.Drawing.Point(902, 519);
            this.postButton.Name = "postButton";
            this.postButton.Size = new System.Drawing.Size(75, 23);
            this.postButton.TabIndex = 2;
            this.postButton.Text = "Submit";
            this.postButton.UseVisualStyleBackColor = true;
            this.postButton.Click += new System.EventHandler(this.postButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(927, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Users";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // attachButton
            // 
            this.attachButton.Location = new System.Drawing.Point(1012, 519);
            this.attachButton.Name = "attachButton";
            this.attachButton.Size = new System.Drawing.Size(75, 23);
            this.attachButton.TabIndex = 5;
            this.attachButton.Text = "Attach";
            this.attachButton.UseVisualStyleBackColor = true;
            this.attachButton.Click += new System.EventHandler(this.attachButton_Click);
            // 
            // chatPanel
            // 
            this.chatPanel.AutoScroll = true;
            this.chatPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chatPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.chatPanel.Location = new System.Drawing.Point(12, 12);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(867, 499);
            this.chatPanel.TabIndex = 6;
            this.chatPanel.WrapContents = false;
            // 
            // attachLabel
            // 
            this.attachLabel.AutoSize = true;
            this.attachLabel.Location = new System.Drawing.Point(899, 545);
            this.attachLabel.Name = "attachLabel";
            this.attachLabel.Size = new System.Drawing.Size(0, 13);
            this.attachLabel.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User,
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(930, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 8;
            // 
            // fetchButton
            // 
            this.fetchButton.Location = new System.Drawing.Point(930, 227);
            this.fetchButton.Name = "fetchButton";
            this.fetchButton.Size = new System.Drawing.Size(75, 23);
            this.fetchButton.TabIndex = 9;
            this.fetchButton.Text = "Fetch users";
            this.fetchButton.UseVisualStyleBackColor = true;
            this.fetchButton.Click += new System.EventHandler(this.fetchButton_Click);
            // 
            // User
            // 
            this.User.DataPropertyName = "username";
            this.User.HeaderText = "User";
            this.User.Name = "User";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "postcount";
            this.Column1.HeaderText = "Post Count";
            this.Column1.Name = "Column1";
            // 
            // ChatForm
            // 
            this.AcceptButton = this.postButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 581);
            this.Controls.Add(this.fetchButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.attachLabel);
            this.Controls.Add(this.chatPanel);
            this.Controls.Add(this.attachButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.postButton);
            this.Controls.Add(this.textBox1);
            this.Name = "ChatForm";
            this.Text = "Chat";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button postButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.FlowLayoutPanel chatPanel;
        private System.Windows.Forms.Label attachLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button fetchButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}