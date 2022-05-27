﻿
namespace WhyApp
{
    partial class ModeratorForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.reasonTB = new System.Windows.Forms.TextBox();
            this.roomIDBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.hourBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.minuteBox = new System.Windows.Forms.NumericUpDown();
            this.unbanButton = new System.Windows.Forms.Button();
            this.banButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.banUserTextbos = new System.Windows.Forms.TextBox();
            this.displayUserButton = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roomIDBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 338);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Post Deletion";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.reasonTB);
            this.groupBox2.Controls.Add(this.roomIDBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.hourBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.minuteBox);
            this.groupBox2.Controls.Add(this.unbanButton);
            this.groupBox2.Controls.Add(this.banButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.banUserTextbos);
            this.groupBox2.Location = new System.Drawing.Point(13, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 131);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ban";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Reason: ";
            // 
            // reasonTB
            // 
            this.reasonTB.Location = new System.Drawing.Point(74, 63);
            this.reasonTB.Name = "reasonTB";
            this.reasonTB.Size = new System.Drawing.Size(100, 20);
            this.reasonTB.TabIndex = 8;
            // 
            // roomIDBox
            // 
            this.roomIDBox.Location = new System.Drawing.Point(276, 25);
            this.roomIDBox.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.roomIDBox.Name = "roomIDBox";
            this.roomIDBox.Size = new System.Drawing.Size(99, 20);
            this.roomIDBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "H:";
            // 
            // hourBox
            // 
            this.hourBox.Location = new System.Drawing.Point(276, 66);
            this.hourBox.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hourBox.Name = "hourBox";
            this.hourBox.Size = new System.Drawing.Size(34, 20);
            this.hourBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "M:";
            // 
            // minuteBox
            // 
            this.minuteBox.Location = new System.Drawing.Point(341, 66);
            this.minuteBox.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minuteBox.Name = "minuteBox";
            this.minuteBox.Size = new System.Drawing.Size(34, 20);
            this.minuteBox.TabIndex = 4;
            // 
            // unbanButton
            // 
            this.unbanButton.Location = new System.Drawing.Point(260, 102);
            this.unbanButton.Name = "unbanButton";
            this.unbanButton.Size = new System.Drawing.Size(75, 23);
            this.unbanButton.TabIndex = 3;
            this.unbanButton.Text = "Unban";
            this.unbanButton.UseVisualStyleBackColor = true;
            this.unbanButton.Click += new System.EventHandler(this.unbanButton_Click);
            // 
            // banButton
            // 
            this.banButton.Location = new System.Drawing.Point(10, 102);
            this.banButton.Name = "banButton";
            this.banButton.Size = new System.Drawing.Size(75, 23);
            this.banButton.TabIndex = 2;
            this.banButton.Text = "Ban";
            this.banButton.UseVisualStyleBackColor = true;
            this.banButton.Click += new System.EventHandler(this.ban_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User ID:";
            // 
            // banUserTextbos
            // 
            this.banUserTextbos.Location = new System.Drawing.Point(74, 22);
            this.banUserTextbos.Name = "banUserTextbos";
            this.banUserTextbos.Size = new System.Drawing.Size(107, 20);
            this.banUserTextbos.TabIndex = 0;
            // 
            // displayUserButton
            // 
            this.displayUserButton.Location = new System.Drawing.Point(479, 34);
            this.displayUserButton.Name = "displayUserButton";
            this.displayUserButton.Size = new System.Drawing.Size(289, 23);
            this.displayUserButton.TabIndex = 2;
            this.displayUserButton.Text = "Display Users";
            this.displayUserButton.UseVisualStyleBackColor = true;
            this.displayUserButton.Click += new System.EventHandler(this.displayUserButton_Click);
            // 
            // ModeratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.displayUserButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ModeratorForm";
            this.Text = "ModeratorForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roomIDBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button unbanButton;
        private System.Windows.Forms.Button banButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox banUserTextbos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown hourBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minuteBox;
        private System.Windows.Forms.NumericUpDown roomIDBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox reasonTB;
        private System.Windows.Forms.Button displayUserButton;
    }
}