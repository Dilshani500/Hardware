using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Loginform_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);


            panel1.BackColor = Color.FromArgb(192, 192, 192, 192);
        }

        

       private void ResetBtn_Click(object sender, EventArgs e)
        {
            UsernameTb.Clear();
            PasswordTb.Clear();
        }

        private void LoginBtn_Click_1(object sender, EventArgs e)
        {
            if (UsernameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Data!!");

            }

            else if (UsernameTb.Text == "Admin" && PasswordTb.Text == "Admin123")
            {
                Item obj = new Item();
                obj.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }
        }
    }
}
