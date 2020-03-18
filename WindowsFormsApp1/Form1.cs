using NewsMaker.AbstractDao;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Service;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Button firstButton = new Button();
        Dao dao;
        public Form1()
        {
            InitializeComponent();
            InitializeComponent();
            Text = "Hello World!";
            this.BackColor = Color.Pink;
            this.Width = 250;
            this.Height = 250;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            firstButton.Text = "Select all";
            ArticleService service = new ArticleService();
            await service.ExecuteAsync();
            
        }
    }
}
