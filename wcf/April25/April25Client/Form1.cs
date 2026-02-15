using April25Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April25Client
{
    public partial class Form1 : Form , IKalkulatorCallback
    {
        KalkulatorClient client;
       
        public Form1()
        {
            InitializeComponent();
        }

        public void Greska(string poruka)
        {
            MessageBox.Show($"Error: {poruka}");

        }

        public void Rezultat( Rezultat rezultat)
        {
    
            izraz.Text = rezultat.Izraz;
            rezultatTb.Text = rezultat.Vrednost.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            client = new KalkulatorClient(instanceContext);
            
        }

        private void plus_Click(object sender, EventArgs e)
        {
            client.Dodaj(numericUpDown.Value);
        }

        private void minus_Click(object sender, EventArgs e)
        {
            client.Oduzmi(numericUpDown.Value);
        }

        private void puta_Click(object sender, EventArgs e)
        {
            client.Pomnozi(numericUpDown.Value);
        }

        private void podeljeno_Click(object sender, EventArgs e)
        {
            client.Podeli(numericUpDown.Value);
        }

        
    }
}
