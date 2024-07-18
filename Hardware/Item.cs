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
    public partial class Item : Form
    {
        
        public Item()
        {
            InitializeComponent();
            Con = new Functions();
            GetCategories();
            ShowItem();
            
        }

        Functions Con;
        
        

        private void GetCategories()
        {
            string Query = "select * from CategoryTbl";
            CatCb.ValueMember = Con.GetData(Query).Columns["CatCode"].ToString();
            CatCb.DisplayMember= Con.GetData(Query).Columns["CatName"].ToString();
            CatCb.DataSource = Con.GetData(Query);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(170, 78, 127);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(247, 194, 222);
        }

       
        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditBtn.BackColor = Color.FromArgb(247, 194, 222);
        }

        private void ItenBtn_Click(object sender, EventArgs e)
        {
            ItenBtn.BackColor = Color.FromArgb(170, 78, 127);

            if (NameTb.Text == "" || CatCb.SelectedIndex == -1 || ManufacturerTb.Text == "" || PriceTb.Text == "" || StockTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }

            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Cat = CatCb.SelectedValue.ToString();
                    string Price = PriceTb.Text;
                    string Stock = StockTb.Text;
                    string Man = ManufacturerTb.Text;
                    string Query = "insert into ItemTbl values ('{0}',{1},{2},{3},'{4}')";
                    Query = string.Format(Query, Name,Cat,Price,Stock,Man);
                    Con.SetData(Query);
                    ShowItem();
                    MessageBox.Show("Item Added!!!");
                    NameTb.Text = "";
                    ManufacturerTb.Text = "";
                    CatCb.SelectedIndex = -1;
                    PriceTb.Text = "";
                    StockTb.Text = "";

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ShowItem()
        {
            string Query = "select * from ItemTbl";
            ItemList.DataSource= Con.GetData(Query);
        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || CatCb.SelectedIndex == -1 || ManufacturerTb.Text == "" || PriceTb.Text == "" || StockTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    int ItCode = key;
                    string Name = NameTb.Text;
                    string Cat = CatCb.SelectedValue.ToString();
                    string Price = PriceTb.Text;
                    string Stock = StockTb.Text;
                    string Man = ManufacturerTb.Text;
                    string Query = "UPDATE ItemTbl SET ItName='{0}', ItCategory={1}, Price={2}, Stock={3}, Manufacturer='{4}' WHERE ItCode={5}";
                    Query = string.Format(Query, Name, Cat, Price, Stock, Man,key);
                    Con.SetData(Query);
                    ShowItem();
                    MessageBox.Show("Item Updated!!!");
                    NameTb.Text = "";
                    ManufacturerTb.Text = "";
                    CatCb.SelectedIndex = -1;
                    PriceTb.Text = "";
                    StockTb.Text = "";
                   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }




        }
        int key = 0;
        private void ItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ItemList.SelectedRows.Count > 0)
            {
                NameTb.Text = ItemList.SelectedRows[0].Cells[1].Value.ToString();
                CatCb.Text = ItemList.SelectedRows[0].Cells[2].Value.ToString();
                PriceTb.Text = ItemList.SelectedRows[0].Cells[3].Value.ToString();
                StockTb.Text = ItemList.SelectedRows[0].Cells[4].Value.ToString();
                ManufacturerTb.Text = ItemList.SelectedRows[0].Cells[5].Value.ToString();
                if (NameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(ItemList.SelectedRows[0].Cells[0].Value.ToString());
                }
            }




        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select an Item to Delete!!!");
            }
            else
            {
                try
                {
                    string Query = "DELETE FROM ItemTbl WHERE ItCode={0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    ShowItem();
                    MessageBox.Show("Item Deleted!!!");
                    NameTb.Text = "";
                    ManufacturerTb.Text = "";
                    CatCb.SelectedIndex = -1;
                    PriceTb.Text = "";
                    StockTb.Text = "";
                    key = 0;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
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

        private void Item_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);


            ItemList.ColumnHeadersDefaultCellStyle.Font= new Font("Franklin Gothic", 14,FontStyle.Bold);
            ItemList.Columns["ItCode"].DefaultCellStyle.Font = new Font("Franklin Gothic", 13, FontStyle.Bold);
            ItemList.Columns["ItName"].DefaultCellStyle.Font = new Font("Franklin Gothic", 13, FontStyle.Bold);
            ItemList.Columns["ItCategory"].DefaultCellStyle.Font = new Font("Franklin Gothic", 13, FontStyle.Bold);
            ItemList.Columns["Price"].DefaultCellStyle.Font = new Font("Franklin Gothic", 13, FontStyle.Bold);
            ItemList.Columns["Stock"].DefaultCellStyle.Font = new Font("Franklin Gothic", 13, FontStyle.Bold);
            ItemList.Columns["Manufacturer"].DefaultCellStyle.Font = new Font("Franklin Gothic", 13, FontStyle.Bold);

        }

        private void ItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 & e.Value != null)
            {
                int sum1 = Convert.ToInt32(e.Value);
                if (sum1 < 50)
                {
                    e.CellStyle.BackColor = Color.Red;
                }
                else if (sum1 >= 50 & sum1 < 85)
                {
                    e.CellStyle.BackColor = Color.Yellow;
                }
                else if (sum1 >= 85)
                {
                    e.CellStyle.BackColor = Color.Green;
                }
            }
        }

        private void EmailBtn_Click(object sender, EventArgs e)
        {
            Email EmailForm = new Email();
            EmailForm.Show();
            this.Hide();
        }
    }
}
