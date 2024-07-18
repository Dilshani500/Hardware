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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCustomers();
        }

        Functions Con;

        private void ShowCustomers()
        {
            string Query = "select * from CustomerTbl";
            CustomerList.DataSource = Con.GetData(Query);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || GenderCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenderCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string Query = "insert into CustomerTbl values('{0}','{1}','{2}')";
                    Query = string.Format(Query, Name, Gender, Phone);
                    Con.SetData(Query);
                    ShowCustomers();
                    MessageBox.Show("Customer Added!!");
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCb.SelectedIndex = -1;
                }

                catch (Exception Ex) 
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;

        private void CustomerList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = CustomerList.SelectedRows[0].Cells[1].Value.ToString();
            GenderCb.Text = CustomerList.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = CustomerList.SelectedRows[0].Cells[3].Value.ToString();

            if (NameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || GenderCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenderCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string Query = "update CustomerTbl set Name = '{0}', Gender = '{1}', Phone = '{2}' where CustCode = '{3}'";
                    Query = string.Format(Query, Name, Gender, Phone,key);
                    Con.SetData(Query);
                    ShowCustomers();
                    MessageBox.Show("Customer Added!!");
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCb.SelectedIndex = -1;
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || GenderCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenderCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string Query = "Delete from CustomerTbl where CustCode = {0}";
                    Query = string.Format(Query,key);
                    Con.SetData(Query);
                    ShowCustomers();
                    MessageBox.Show("Customer Deleted");
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCb.SelectedIndex = -1;
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void ItemBtn_Click(object sender, EventArgs e)
        {
            Item itemForm = new Item();
            itemForm.Show();
            this.Hide();
        }

        

        private void BillingBtn_Click(object sender, EventArgs e)
        {
            Billing billingForm = new Billing();
            billingForm.Show();
            this.Hide();
        }

        

        private void CategoryTb_Click(object sender, EventArgs e)
        {
            Categories categoryForm = new Categories();
            categoryForm.Show();
            this.Hide();
        }

        private void Customerform_load(object sender, EventArgs e)
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

        private void EmailBtn_Click(object sender, EventArgs e)
        {
            Email EmailForm = new Email();
            EmailForm.Show();
            this.Hide();
        }
    }
    
}
