using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace Hardware
{
    public partial class Email : Form
    {
        public Email()
        {
            InitializeComponent();
        }

        private void ItemBtn_Click(object sender, EventArgs e)
        {
            Item itemForm = new Item();
            itemForm.Show();
            this.Hide();
        }

        private void CategoryTb_Click(object sender, EventArgs e)
        {
            Categories categoryForm = new Categories();
            categoryForm.Show();
            this.Hide();
        }

        private void CustomerTb_Click(object sender, EventArgs e)
        {
            Customer customerForm = new Customer();
            customerForm.Show();
            this.Hide();
        }

        private void BillingBtn_Click(object sender, EventArgs e)
        {
            Billing billingForm = new Billing();
            billingForm.Show();
            this.Hide();
        }

        private void Email_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(170, 78, 127);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(247, 194, 222);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.ShowDialog();
            lblLocation.Text = openFileDialog1.FileName;
        }
        private void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(textFrom.Text);
                mail.To.Add(textTo.Text);
                mail.Subject = texTitle.Text;
                mail.Body = TxtBody.Text;

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(lblLocation.Text);
                mail.Attachments.Add(attachment);


                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(textFrom.Text,textPassword.Text);
                smtp.EnableSsl= true;
                smtp.Send(mail);
                MessageBox.Show("Mail has been successfully sent!","Email sent",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
