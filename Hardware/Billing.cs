using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware
{
    public partial class Billing : Form
    {
        public Billing()
        {
            Con = new Functions();
            InitializeComponent();
            ShowItem();
            SetupClientBill();
            
        }
        Functions Con;
        private void ShowItem()
        {
            string Query = "select * from ItemTbl";
            ItemList.DataSource = Con.GetData(Query);
        }

        private void Billing_Load(object sender, EventArgs e)
        {

        }
        int key = 0;
        int Stock;
        private void ItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*NameTb.Text = ItemList.SelectedRows[0].Cells[1].Value.ToString();
            //CatCb.Text = ItemList.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ItemList.SelectedRows[0].Cells[3].Value.ToString();
            Stock = Convert.ToInt32(ItemList.SelectedRows[0].Cells[4].Value.ToString());
            //ManufacturerTb.Text = ItemList.SelectedRows[0].Cells[5].Value.ToString();
            if (NameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ItemList.SelectedRows[0].Cells[0].Value.ToString());
            }*/

            if (ItemList.SelectedRows.Count > 0)
            {
                try
                {
                    NameTb.Text = ItemList.SelectedRows[0].Cells[1].Value?.ToString() ?? string.Empty;
                    PriceTb.Text = ItemList.SelectedRows[0].Cells[3].Value?.ToString() ?? string.Empty;
                    Stock = int.TryParse(ItemList.SelectedRows[0].Cells[4].Value?.ToString(), out int stockValue) ? stockValue : 0;
                    key = int.TryParse(ItemList.SelectedRows[0].Cells[0].Value?.ToString(), out int keyValue) ? keyValue : 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing row data: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No row selected.");
            }

            
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            NameTb.Text = "";
            PriceTb.Text = "";
        }

        string PMethod = "";
        int n = 0;
        int GrdTotal = 0;

        private void UpdateStock()
        {
            
                try
                {

                 int NewStock = Stock - Convert.ToInt32(QtyTb.Text);
                 string Query = "Update ItemTbl set Stock = {0} where ItCode = {1}";
                 Query = string.Format(Query,NewStock,key);
                 Con.SetData(Query);


                
                  ShowItem();
                  MessageBox.Show("Stock Updated!!!");

                

            }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            
    }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            /*if (PriceTb.Text == "" || QtyTb.Text == ""  || NameTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }

            else if (Stock < Convert.ToInt32(QtyTb.Text))
            {
                MessageBox.Show("Not Enough Stock!!!");
            }

            else
            {
                int Qte = Convert.ToInt32(QtyTb.Text);
                int total = Convert.ToInt32(PriceTb.Text) * Qte;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ClientBill);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = NameTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = "Rs" + total;
                ClientBill.Rows.Add(newRow);
                n++;
                GrdTotal = GrdTotal + total;
                GrdTotalLbl.Text = "Rs" + GrdTotal;
                UpdateStock();
                ShowItem();
            }*/

            if (string.IsNullOrWhiteSpace(PriceTb.Text) || string.IsNullOrWhiteSpace(QtyTb.Text) || string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MessageBox.Show("Missing Data");
            }
            else if (!int.TryParse(QtyTb.Text, out int qty))
            {
                MessageBox.Show("Invalid Quantity");
            }
            else if (Stock < qty)
            {
                MessageBox.Show("Not Enough Stock!!!");
            }
            else
            {
                try
                {
                    int total = int.Parse(PriceTb.Text) * qty;
                    DataGridViewRow newRow = new DataGridViewRow();

                    // Ensure ClientBill has the correct number of columns
                    if (ClientBill.ColumnCount < 5)
                    {
                        MessageBox.Show("The ClientBill DataGridView does not have the expected number of columns.");
                        return;
                    }

                    newRow.CreateCells(ClientBill);

                    // Check if newRow has the correct number of cells
                    if (newRow.Cells.Count < 5)
                    {
                        MessageBox.Show("The newRow does not have the expected number of cells.");
                        return;
                    }

                    newRow.Cells[0].Value = ++n;
                    newRow.Cells[1].Value = NameTb.Text;
                    newRow.Cells[2].Value = PriceTb.Text;
                    newRow.Cells[3].Value = QtyTb.Text;
                    newRow.Cells[4].Value = "Rs" + total;

                    ClientBill.Rows.Add(newRow);
                    GrdTotal += total;
                    GrdTotalLbl.Text = "Rs" + GrdTotal;
                    UpdateStock();
                    ShowItem();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please ensure the quantity and price are valid numbers.");
                }
            }
        }


        private void SetupClientBill()
        {
            ClientBill.Columns.Clear();
            ClientBill.Columns.Add("Column1", "No");
            ClientBill.Columns.Add("Column2", "Name");
            ClientBill.Columns.Add("Column3", "Price");
            ClientBill.Columns.Add("Column4", "Quantity");
            ClientBill.Columns.Add("Column5", "Total");
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            /*if (NameTb.Text == "" || CustTb.Text =="")
            {
                MessageBox.Show("Missing Data!!!");
            }

            else
            {
                try
                {
                    if (MobileRadio.Checked == true)
                    {
                        PMethod = "Mobile";
                    }

                    else if (CardRadio.Checked == true)
                    {
                        PMethod = "Card";
                    }
                    else
                    {
                        PMethod = "Cash";
                    }
                    string Name = NameTb.Text;
                    
                    string Query = "insert into BillTbl values ('{0}',{1},{2},'{3}')";
                    Query = string.Format(Query, DateTime.Today.Date, CustTb.Text,GrdTotal,PMethod);
                    Con.SetData(Query);
                    ShowItem();
                    MessageBox.Show("Bill Added!!!");
                    

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }*/

            if (string.IsNullOrWhiteSpace(NameTb.Text) || string.IsNullOrWhiteSpace(CustTb.Text))
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    PMethod = MobileRadio.Checked ? "Mobile" : CardRadio.Checked ? "Card" : "Cash";
                    string Query = "insert into BillTbl values ('{0}', '{1}', {2}, '{3}')";
                    Query = string.Format(Query, DateTime.Today.Date, CustTb.Text, GrdTotal, PMethod);
                    Con.SetData(Query);
                    ShowItem();
                    MessageBox.Show("Bill Added!!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding bill: {ex.Message}");
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

        private void CategoryTb_Click(object sender, EventArgs e)
        {
            Categories categoryForm = new Categories();
            categoryForm.Show();
            this.Hide();
        }

        private void Billingform_Load(object sender, EventArgs e)
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

        private void ItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 & e.Value != null) 
            {
                int sum1 = Convert.ToInt32(e.Value);
                if (sum1 <50)
                {
                    e.CellStyle.BackColor = Color.Red;
                }
                else if (sum1>=50 & sum1 <85) 
                {
                    e.CellStyle.BackColor = Color.Yellow;
                }
                else if (sum1 >=85)
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
