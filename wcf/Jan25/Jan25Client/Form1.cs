using Jan25Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = Jan25Client.ServiceReference1.Message;

namespace Jan25Client
{
    public partial class Form1 : Form
    {
        ChatClient chatClient;
        bool registered;
        string nickname;
        ChatCallback callback;
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowError(string error) 
        {
            ErrorLbl.Visible = true;
            ErrorLbl.Text = error;
            return;
        }

        //public void ReceiveMessage(Message message) ovako ako public partial class Form1 : Form , IChatCallback i onda nemas unutrasnju klasu ChatCallback
        //{
        //    messagesLb.Items.Add($"[{message.SentAt}] {message.FromUser} -> {message.ToUser}: {message.Text}");
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            registered = false;
            callback = new ChatCallback(this);
            InstanceContext instanceContext = new InstanceContext(callback);
            chatClient = new ChatClient(instanceContext);
            

        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nickNameTb.Text)) 
            {
                ShowError("Enter nickname");
                return;
            }

            chatClient.Register(nickNameTb.Text);
            nickname = nickNameTb.Text;
            registered = true;
            ErrorLbl.Visible = false;

        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            if(!registered)
            {
                ShowError("Not registered");
                return;
            }
            if (string.IsNullOrEmpty(SendToTb.Text)) 
            {
                ShowError("Enter recipient");
                return;
            }
            if (string.IsNullOrEmpty(TextTb.Text)) 
            {
                ShowError("Enter text");
                return;
            }
            Message message = new Message
            {
                FromUser = nickname,
                ToUser = SendToTb.Text,
                Text = TextTb.Text
            };
            chatClient.SendMessage(message);
            ErrorLbl.Visible = false;
        }
        public class ChatCallback : IChatCallback
        {
            private readonly Form1 form;

            public ChatCallback(Form1 form) 
            {
                this.form = form;
            }
            public void ReceiveMessage(Message message)
            {
                form.messagesLb.Items.Add($"[{message.SentAt}] {message.FromUser} -> {message.ToUser}: {message.Text}");
            }
        }
    }
}
