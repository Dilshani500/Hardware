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
    public partial class Categories : Form
    {
        

        public Categories()
        {
            InitializeComponent();
            Con =new Functions();
            ShowCategories();

        }
        Functions Con;

        private void ShowCategories()
        {
            string Query = "select * from CategoryTbl";
            CategoriesList.DataSource = Con.GetData(Query);
        }

        private void AddBtn_Click_1(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }

            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Query = "insert into CategoryTbl values('{0}')";
                    Query = string.Format(Query, Name);
                    Con.SetData(Query);
                    ShowCategories();
                    MessageBox.Show("Category Added!!!");
                    NameTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }


        int Key =0;
        private void CategoriesList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = CategoriesList.SelectedRows[0].Cells[1].Value.ToString();

            if (NameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CategoriesList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }


        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditBtn.BackColor = Color.FromArgb(247, 194, 222);
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }

            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Query = "Update CategoryTbl set CatName = '{0}' where CatCode = {1}";
                    Query = string.Format(Query, Name,Key);
                    Con.SetData(Query);
                    ShowCategories();
                    MessageBox.Show("Category Updated!!!");
                    NameTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }

            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Query = "Delete from CategoryTbl where CatCode = {0}";
                    Query = string.Format(Query,Key);
                    Con.SetData(Query);
                    ShowCategories();
                    MessageBox.Show("Category Deleted!!!");
                    NameTb.Text = "";
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

        private void Categoriesform_load(object sender, EventArgs e)
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
