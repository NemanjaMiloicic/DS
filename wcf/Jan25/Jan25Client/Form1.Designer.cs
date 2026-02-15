namespace Jan25Client
{
    partial class Form1
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
            this.nickNameTb = new System.Windows.Forms.TextBox();
            this.Nickname = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextTb = new System.Windows.Forms.TextBox();
            this.SendToTb = new System.Windows.Forms.TextBox();
            this.messagesLb = new System.Windows.Forms.ListBox();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.ErrorLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nickNameTb
            // 
            this.nickNameTb.Location = new System.Drawing.Point(135, 12);
            this.nickNameTb.Name = "nickNameTb";
            this.nickNameTb.Size = new System.Drawing.Size(512, 22);
            this.nickNameTb.TabIndex = 0;
            // 
            // Nickname
            // 
            this.Nickname.AutoSize = true;
            this.Nickname.Location = new System.Drawing.Point(29, 9);
            this.Nickname.Name = "Nickname";
            this.Nickname.Size = new System.Drawing.Size(34, 16);
            this.Nickname.TabIndex = 1;
            this.Nickname.Text = "Nick";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SendBtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TextTb);
            this.groupBox1.Controls.Add(this.SendToTb);
            this.groupBox1.Location = new System.Drawing.Point(13, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 167);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message";
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(122, 107);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(512, 23);
            this.SendBtn.TabIndex = 4;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Send to";
            // 
            // TextTb
            // 
            this.TextTb.Location = new System.Drawing.Point(122, 68);
            this.TextTb.Name = "TextTb";
            this.TextTb.Size = new System.Drawing.Size(512, 22);
            this.TextTb.TabIndex = 1;
            // 
            // SendToTb
            // 
            this.SendToTb.Location = new System.Drawing.Point(122, 22);
            this.SendToTb.Name = "SendToTb";
            this.SendToTb.Size = new System.Drawing.Size(512, 22);
            this.SendToTb.TabIndex = 0;
            // 
            // messagesLb
            // 
            this.messagesLb.FormattingEnabled = true;
            this.messagesLb.ItemHeight = 16;
            this.messagesLb.Location = new System.Drawing.Point(16, 262);
            this.messagesLb.Name = "messagesLb";
            this.messagesLb.Size = new System.Drawing.Size(711, 244);
            this.messagesLb.TabIndex = 3;
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Location = new System.Drawing.Point(135, 40);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(512, 28);
            this.RegisterBtn.TabIndex = 4;
            this.RegisterBtn.Text = "Register";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // ErrorLbl
            // 
            this.ErrorLbl.AutoSize = true;
            this.ErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.ErrorLbl.Location = new System.Drawing.Point(679, 13);
            this.ErrorLbl.Name = "ErrorLbl";
            this.ErrorLbl.Size = new System.Drawing.Size(44, 16);
            this.ErrorLbl.TabIndex = 5;
            this.ErrorLbl.Text = "label3";
            this.ErrorLbl.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 527);
            this.Controls.Add(this.ErrorLbl);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.messagesLb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Nickname);
            this.Controls.Add(this.nickNameTb);
            this.Name = "Form1";
            this.Text = "ChatApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nickNameTb;
        private System.Windows.Forms.Label Nickname;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextTb;
        private System.Windows.Forms.TextBox SendToTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox messagesLb;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Button RegisterBtn;
        private System.Windows.Forms.Label ErrorLbl;
    }
}

