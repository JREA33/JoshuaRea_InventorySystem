using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoshuaRea_InventorySystem
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
            Inventory.createInventory();

            //Initialize DataGridViews

            dgvParts.DataSource = Inventory.AllParts;
            dgvProducts.DataSource = Inventory.Products;

            //Change Column Headers Text and Order
            dgvParts.Columns[0].HeaderText = "Part ID";
            dgvParts.Columns[0].DisplayIndex = 0;
            dgvParts.Columns[3].HeaderText = "Inventory";
            dgvParts.Columns[2].DisplayIndex = 3;

            dgvProducts.Columns[0].HeaderText = "Product ID";
            dgvProducts.Columns[0].DisplayIndex = 0;
            dgvProducts.Columns[3].HeaderText = "Inventory";
            dgvProducts.Columns[2].DisplayIndex = 3;
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            
        }

        //Buttons for Parts
        private void btnAddPart_Click(object sender, EventArgs e)
        {
            new AddPartScreen().ShowDialog();
        }
        private void btnModifyPart_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow.DataBoundItem.GetType() == typeof(InHousePart))
            {
                InHousePart inHousePart = dgvParts.CurrentRow.DataBoundItem as InHousePart;
                new ModifyPartScreen(inHousePart).ShowDialog();
            }
            else
            {
                OutsourcedPart outsourcedPart = dgvParts.CurrentRow.DataBoundItem as OutsourcedPart;
                new ModifyPartScreen(outsourcedPart).ShowDialog();
            }
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            Part removedPart = dgvParts.CurrentRow.DataBoundItem as Part;
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this part?", "Delete Part", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Inventory.deletePart(removedPart);
            }
        }

        //Buttons for Products
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            new AddProductScreen().ShowDialog();
        }

        private void btnModifyProduct_Click(object sender, EventArgs e)
        {
            Product currentProduct = dgvProducts.CurrentRow.DataBoundItem as Product;
            new ModifyProductScreen(currentProduct).ShowDialog();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            Product removedProduct = dgvProducts.CurrentRow.DataBoundItem as Product;
            Inventory.removeProduct(removedProduct.ProductID);
        }

        //Search Buttons

        private void btnPartSearch_Click(object sender, EventArgs e)
        {
            bool found = false;
            dgvParts.ClearSelection();

            if (int.TryParse(txtPartSearch.Text, out int i))
            {
                Part match = Inventory.lookupPart(i);
                foreach (DataGridViewRow dgvRow in dgvParts.Rows)
                {
                    Part part = dgvRow.DataBoundItem as Part;

                    if (part.PartID == match.PartID)
                    {
                        found = true;
                        dgvRow.Selected = true;
                        break;
                    }
                    else
                    {
                        dgvRow.Selected = false;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow dgvRow in dgvParts.Rows)
                {
                    Part part = dgvRow.DataBoundItem as Part;

                    if (part.Name.ToLower().Contains(txtPartSearch.Text.ToLower()))
                    {
                        found = true;
                        dgvRow.Selected = true;
                        break;
                    }
                    else
                    {
                        dgvRow.Selected = false;
                    }
                }
            }

            if (found == false)
            {
                MessageBox.Show("Part could not be found.");
            }
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            bool found = false;
            dgvProducts.ClearSelection();

            if (string.IsNullOrWhiteSpace(txtProductSearch.Text))
            {
                dgvProducts.ClearSelection();
            }

            if (int.TryParse(txtProductSearch.Text, out int i))
            {
                Product match = Inventory.lookupProduct(i);
                foreach (DataGridViewRow dgvRow in dgvProducts.Rows)
                {
                    Product product = dgvRow.DataBoundItem as Product;

                    if (product.ProductID == match.ProductID)
                    {
                        found = true;
                        dgvRow.Selected = true;
                        break;
                    }
                    else
                    {
                        dgvRow.Selected = false;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow dgvRow in dgvProducts.Rows)
                {
                    Product product = dgvRow.DataBoundItem as Product;

                    if (product.Name.ToLower().Contains(txtProductSearch.Text.ToLower()))
                    {
                        found = true;
                        dgvRow.Selected = true;
                        break;
                    }
                    else
                    {
                        dgvRow.Selected = false;
                    }
                }
            }
            
            if (found == false)
            {
                MessageBox.Show("Product could not be found.");
            }
        }

        //Exit Button

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}