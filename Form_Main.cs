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
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Image Steganography" + Environment.NewLine + "" + Environment.NewLine + "Encrypting data within an image." + Environment.NewLine + "Steganography is the process of embedding & encrypting" + Environment.NewLine + "a portion of text within an image file.", "About Steganography");
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Plaintext.Clear();
            lbl_msgText.Text = "Plaintext content cleared!";
        }

        private void aboutDeveloperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("© 2015; No rights reserved." +Environment.NewLine+""+Environment.NewLine+ "Developed by Kevin Miranda" + Environment.NewLine + "kevinmiranda26@gmail.com" +Environment.NewLine+ "13-PCA-02", "About Developer");
        }
    }
}
