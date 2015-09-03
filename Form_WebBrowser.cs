using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Steganography_Kevin
{
    public partial class Form_WebBrowser : Form
    {
        public Form_WebBrowser()
        {
            InitializeComponent();
        }

        private void Form_WebBrowser_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://encrypted.google.com/");
            txt_Address.Text = "https://encrypted.google.com/";
        }

        private void btn_Forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://encrypted.google.com/");
        }

        private void btn_Go_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(txt_Address.Text);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }


    }
}
