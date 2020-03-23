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

        private async void download_ClickAsync(object sender, EventArgs e)
        {
            ArticleService service = new ArticleService();
            service.Create();
            
        }

        private void select_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
