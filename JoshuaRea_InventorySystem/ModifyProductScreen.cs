using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoshuaRea_InventorySystem
{
    public partial class ModifyProductScreen : Form
    {
        MainScreen mainScreen = (MainScreen)Application.OpenForms["MainScreen"];
        public BindingList<Part> addedParts = new BindingList<Part>();
        public ModifyProductScreen(Product product)
        {
            InitializeComponent();


            dgvAllParts.DataSource = Inventory.AllParts;
            dgvAssociatedParts.DataSource = addedParts;

            TxtProductID = product.ProductID;
            TxtName = product.Name;
            TxtInventory = product.InStock;
            TxtPrice = product.Price;
            TxtMin = product.Min;
            TxtMax = product.Max;

            foreach (Part part in product.AssociatedParts)
            {
                addedParts.Add(part);
            }
        }

        //Validate Input Values
        private bool validateValues()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name must not be empty.");
                return true;
            }
            if (!int.TryParse(txtInventory.Text, out int i))
            {
                MessageBox.Show("Inventory must be a numeric value.");
                return true;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal j))
            {
                MessageBox.Show("Price must be a decimal value.");
                return true;
            }
            if (!int.TryParse(txtMax.Text, out i))
            {
                MessageBox.Show("Max must be a numeric value.");
                return true;
            }
            if (!int.TryParse(txtMin.Text, out i))
            {
                MessageBox.Show("Min must be a numeric value.");
                return true;
            }
            if (TxtMax < TxtMin)
            {
                MessageBox.Show("Minimum cannot be greater than maximum.");
                return true;
            }
            if (TxtInventory > TxtMax || TxtInventory < TxtMin)
            {
                MessageBox.Show("Inventory is outside of the Min/Max Range.");
                return true;
            }
            else
            {
                return false;
            }
        }

        //Add Button

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            Part currentPart = dgvAllParts.CurrentRow.DataBoundItem as Part;
            addedParts.Add(currentPart);
        }

        //Delete Button

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to remove this part?", "Remove Part", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Part associatedPart = dgvAssociatedParts.CurrentRow.DataBoundItem as Part;
                addedParts.Remove(associatedPart);
            }
        }

        //Check Fields to Set as Red and Enable/Disable Save Button

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                txtName.BackColor = System.Drawing.Color.White;
            }
        }

        private void txtInventory_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace (txtInventory.Text))
            {
                txtInventory.BackColor = System.Drawing.Color.Salmon;
            }
            if (!int.TryParse(txtInventory.Text, out int i))
            {
                txtInventory.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                txtInventory.BackColor= System.Drawing.Color.White;
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                txtPrice.BackColor = System.Drawing.Color.Salmon;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal i))
            {
                txtPrice.BackColor= System.Drawing.Color.Salmon;
            }
            else
            {
                txtPrice.BackColor = System.Drawing.Color.White;
            }
        }

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMin.Text))
            {
                txtMin.BackColor = System.Drawing.Color.Salmon;
            }
            if (!int.TryParse (txtMin.Text, out int i))
            {
                txtMin.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                txtMin.BackColor = System.Drawing.Color.White;
            }
        }

        private void txtMax_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMax.Text))
            {
                txtMax.BackColor = System.Drawing.Color.Salmon;
            }
            if (!int.TryParse(txtMax.Text, out int i))
            {
                txtMax.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                txtMax.BackColor = System.Drawing.Color.White;
            }
        }

        //Search Button

        private void btnPartSearch_Click(object sender, EventArgs e)
        {
            bool found = false;
            dgvAllParts.ClearSelection();

            if (int.TryParse(txtPartSearch.Text, out int i))
            {
                Part match = Inventory.lookupPart(i);
                foreach (DataGridViewRow dgvRow in dgvAllParts.Rows)
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
                foreach (DataGridViewRow dgvRow in dgvAllParts.Rows)
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

        //Save Button Event on Click

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validateValues()) { }
            else
            {
                Product updatedProduct = new Product(TxtProductID, TxtName, TxtInventory, TxtPrice, TxtMin, TxtMax);

                foreach (Part part in addedParts)
                {
                    updatedProduct.addAssociatedPart(part);
                }
                Inventory.updateProduct(updatedProduct.ProductID, updatedProduct);

                mainScreen.dgvProducts.Refresh();

                this.Close();
            }
        }

        //Cancel Button Event on Click

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}