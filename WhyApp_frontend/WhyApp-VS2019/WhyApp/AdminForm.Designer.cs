
namespace WhyApp
{
    partial class AdminForm
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
            this.addRoom = new System.Windows.Forms.GroupBox();
            this.addroomnamebox = new System.Windows.Forms.TextBox();
            this.roomDescBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addRoomButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteRoomIDBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.listRoomButton = new System.Windows.Forms.Button();
            this.purgePostButton = new System.Windows.Forms.Button();
            this.listModButton = new System.Windows.Forms.Button();
            this.addRoom.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addRoom
            // 
            this.addRoom.Controls.Add(this.addRoomButton);
            this.addRoom.Controls.Add(this.label2);
            this.addRoom.Controls.Add(this.label1);
            this.addRoom.Controls.Add(this.roomDescBox);
            this.addRoom.Controls.Add(this.addroomnamebox);
            this.addRoom.Location = new System.Drawing.Point(12, 29);
            this.addRoom.Name = "addRoom";
            this.addRoom.Size = new System.Drawing.Size(294, 190);
            this.addRoom.TabIndex = 0;
            this.addRoom.TabStop = false;
            this.addRoom.Text = "Add Chatroom";
            // 
            // addroomnamebox
            // 
            this.addroomnamebox.Location = new System.Drawing.Point(78, 73);
            this.addroomnamebox.Name = "addroomnamebox";
            this.addroomnamebox.Size = new System.Drawing.Size(210, 20);
            this.addroomnamebox.TabIndex = 0;
            // 
            // roomDescBox
            // 
            this.roomDescBox.Location = new System.Drawing.Point(78, 108);
            this.roomDescBox.Name = "roomDescBox";
            this.roomDescBox.Size = new System.Drawing.Size(210, 20);
            this.roomDescBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Room Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Room Desc:";
            // 
            // addRoomButton
            // 
            this.addRoomButton.Location = new System.Drawing.Point(213, 161);
            this.addRoomButton.Name = "addRoomButton";
            this.addRoomButton.Size = new System.Drawing.Size(75, 23);
            this.addRoomButton.TabIndex = 4;
            this.addRoomButton.Text = "Add";
            this.addRoomButton.UseVisualStyleBackColor = true;
            this.addRoomButton.Click += new System.EventHandler(this.addRoomButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deleteButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.deleteRoomIDBox);
            this.groupBox1.Location = new System.Drawing.Point(336, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Delete Chatroom";
            // 
            // deleteRoomIDBox
            // 
            this.deleteRoomIDBox.Location = new System.Drawing.Point(101, 19);
            this.deleteRoomIDBox.Name = "deleteRoomIDBox";
            this.deleteRoomIDBox.Size = new System.Drawing.Size(100, 20);
            this.deleteRoomIDBox.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Room ID:";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(126, 71);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // listRoomButton
            // 
            this.listRoomButton.Location = new System.Drawing.Point(336, 170);
            this.listRoomButton.Name = "listRoomButton";
            this.listRoomButton.Size = new System.Drawing.Size(228, 23);
            this.listRoomButton.TabIndex = 6;
            this.listRoomButton.Text = "List Rooms";
            this.listRoomButton.UseVisualStyleBackColor = true;
            this.listRoomButton.Click += new System.EventHandler(this.listRoomButton_Click);
            // 
            // purgePostButton
            // 
            this.purgePostButton.ForeColor = System.Drawing.Color.Red;
            this.purgePostButton.Location = new System.Drawing.Point(12, 236);
            this.purgePostButton.Name = "purgePostButton";
            this.purgePostButton.Size = new System.Drawing.Size(552, 23);
            this.purgePostButton.TabIndex = 7;
            this.purgePostButton.Text = "Purge all Posts";
            this.purgePostButton.UseVisualStyleBackColor = true;
            this.purgePostButton.Click += new System.EventHandler(this.purgePostButton_Click);
            // 
            // listModButton
            // 
            this.listModButton.Location = new System.Drawing.Point(336, 199);
            this.listModButton.Name = "listModButton";
            this.listModButton.Size = new System.Drawing.Size(228, 23);
            this.listModButton.TabIndex = 8;
            this.listModButton.Text = "List Moderators";
            this.listModButton.UseVisualStyleBackColor = true;
            this.listModButton.Click += new System.EventHandler(this.listModButton_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 450);
            this.Controls.Add(this.listModButton);
            this.Controls.Add(this.purgePostButton);
            this.Controls.Add(this.listRoomButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.addRoom);
            this.Name = "AdminForm";
            this.Text = "Administration Portal";
            this.addRoom.ResumeLayout(false);
            this.addRoom.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox addRoom;
        private System.Windows.Forms.Button addRoomButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox roomDescBox;
        private System.Windows.Forms.TextBox addroomnamebox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox deleteRoomIDBox;
        private System.Windows.Forms.Button listRoomButton;
        private System.Windows.Forms.Button purgePostButton;
        private System.Windows.Forms.Button listModButton;
    }
}