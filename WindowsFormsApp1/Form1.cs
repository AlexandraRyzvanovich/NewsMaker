using NewsMaker.AbstractDao;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Service;
using WindowsFormsApp1.Utils;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Button firstButton = new Button();
        public Form1()
        {
            InitializeComponent();
            Text = "Hello World!";
            this.BackColor = Color.Pink;
            this.Width = 350;
            this.Height = 350;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            firstButton.Text = "Select all";
            ArticleService service = new ArticleService();
            await service.GetLinks();
            
        }
    }
}
