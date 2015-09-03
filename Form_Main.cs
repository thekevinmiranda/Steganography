using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Steganography_Kevin
{
    public partial class Form_Main : Form
    {
        private Bitmap bmp = null;
        private string extractedText = string.Empty;

        public Form_Main()
        {
            InitializeComponent();
        }

        //EXIT TOOL STRIP CLICK (30-7-15)
        

        //ABOUT STEGANOGRAPHY TOOL STRIP CLICK (30-7-15)
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Image Steganography." + Environment.NewLine + "" + Environment.NewLine + "Encrypting data within an image." + Environment.NewLine + "Steganography is the process of embedding & encrypting" + Environment.NewLine + "a portion of text within an image file.", "About Steganography");
        }

        //ABOUT DEVELOPER TOOL STRIP CLICK (30-7-15)
        private void aboutDeveloperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("© 2015; No rights reserved." + Environment.NewLine + "" + Environment.NewLine + "Developed by Kevin Miranda" + Environment.NewLine + "kevinmiranda26@gmail.com" + Environment.NewLine + "13-PCA-02", "About the Developer");
        }

        //CLEAR BUTTON CLICK (30-7-15)
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Plaintext.Clear();
            txt_Password.Clear();            
            //pictureBox_Preview.Image = null;
            //pictureBox_Preview.Update();            
            //pictureBox_Preview.Invalidate();
            pictureBox_Preview.Image = Steganography_Kevin.Properties.Resources.Lock_Image_Preview;
            lbl_msgText.Text = "Content cleared!";
        }        

        //LOAD IMAGE BUTTON CLICK (30-7-15)
        private void btn_loadImage_Click(object sender, EventArgs e)
        {
            lbl_msgText.Text = "---";
            
            OpenFileDialog loadImage = new OpenFileDialog();
            loadImage.Filter = "Image Files (*.jpeg; *.jpg; *.png; *.bmp)| *.jpeg; *.jpg; *.png; *.bmp";

            if (loadImage.ShowDialog() == DialogResult.OK)
            {
                pictureBox_Preview.Image = Image.FromFile(loadImage.FileName);
                lbl_msgText.Text = "Image file loaded successfully!";
            }
        }

        //LOAD TEXT BUTTON CLICK (30-7-15)
        private void btn_loadText_Click(object sender, EventArgs e)
        {
            lbl_msgText.Text = "---";

            OpenFileDialog loadText = new OpenFileDialog();
            loadText.Filter = "Text Files (*.txt)|*.txt";
            if (loadText.ShowDialog() == DialogResult.OK)
            {
                txt_Plaintext.Text = File.ReadAllText(loadText.FileName);
                lbl_msgText.Text = "Text file loaded succesfully!";
            }
        }

        //SAVE IMAGE BUTTON CLICK (30-7-15) (DOES NOT WORK TILL AFTER ENCRYPTION!)
        private void btn_saveImage_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveImage = new SaveFileDialog();
                saveImage.Filter = "Portable Network Graphics Image|*.png|Bitmap Image|*.bmp";

                if (saveImage.ShowDialog() == DialogResult.OK)
                {

                    switch (saveImage.FilterIndex)
                    {
                        case 0:
                            {
                                bmp.Save(saveImage.FileName, ImageFormat.Png);
                            } break;
                        case 1:
                            {
                                bmp.Save(saveImage.FileName, ImageFormat.Bmp);
                            } break;
                    }

                    lbl_msgText.Text = "Image saved successfully.";
                    lbl_msgText.ForeColor = Color.Green;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Program Error!"+Environment.NewLine+""+Environment.NewLine+"You can't save an image without embedding data into it!","Warning!");
            }
        }

        //SAVE TEXT BUTTON CLICK (30-7-15)
        private void btn_saveText_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Text Files|*.txt";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save_dialog.FileName, txt_Plaintext.Text);
                lbl_msgText.Text = "Text file saved successfully!";
            }
            else
            {
                lbl_msgText.Text = "Text file not saved!";
            }
        }

 
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openEmailClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mail = new Form_EmailClient();
            mail.Show();
        }

        private void openWebBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var web_Browser = new Form_WebBrowser();
            web_Browser.Show();

        }

        //EMBED DATA BUTTON CLICK
        private void btn_embedData_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox_Preview.Image;
            string text = txt_Plaintext.Text;
            if (text.Equals(""))
            {
                MessageBox.Show("The text you want to embed cannot be empty!","Error!");
                return;
            }
            if (chk_enableEncryption.Checked)
            {
                if (txt_Password.Text.Length < 5)
                {
                    MessageBox.Show("The password length cannot be lesser than 5 characters!", "Error!");
                    return;
                }
                else
                {
                    text = Crypto.EncryptStringAES(text, txt_Password.Text);
                }
            }
            bmp = SteganographyHelper.embedText(text,bmp);
            MessageBox.Show("Your text was successsfully embedded within the image!","Success!");
            lbl_msgLable.Text = "";
            lbl_msgLable.Text = "Data Embedded. Please save your image.";
            lbl_msgLable.ForeColor = Color.OrangeRed;
        }

        //EXTRACT BUTTON CLICK
        private void btn_extractData_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox_Preview.Image;
            string extractedText = SteganographyHelper.extractText(bmp);

            if (chk_enableEncryption.Checked)
            {
                try
                {
                    extractedText = Crypto.DecryptStringAES(extractedText, txt_Password.Text);
                }
                catch
                {
                    MessageBox.Show("Password Invalid!","Error!");
                    return;
                }
            }
            txt_Plaintext.Text = extractedText;
        }
    }
}
