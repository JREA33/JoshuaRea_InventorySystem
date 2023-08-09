using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoshuaRea_InventorySystem
{
    public partial class AddPartScreen : Form
    {
        public AddPartScreen()
        {
            InitializeComponent();
            int partID = Inventory.AllParts.Max(r => r.PartID);
            TxtPartID = partID + 1;
        }

        //Validate Values

        private bool validateValues()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name must not be empty.");
                return true;
            }
            if (!int.TryParse(txtInventory.Text,out int i))
            {
                MessageBox.Show("Inventory must be a numeric value.");
                return true;
            }
            if (!decimal.TryParse(txtPrice.Text,out decimal j))
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
            if (rbInHouse.Checked)
            {
                if (!int.TryParse(txtPartType.Text, out i))
                {
                    MessageBox.Show("Machine ID must be a numeric value.");
                    return true;
                }
            }
            if (rbOutsourced.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPartType.Text))
                {
                    MessageBox.Show("Company Name must not be empty.");
                    return true;
                }
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

        //Check Radio Buttons and Change Label

        private void rbInHouse_CheckedChanged(object sender, EventArgs e)
        {
            lblPartType.Text = "Machine ID";
        }

        private void rbOutsourced_CheckedChanged(object sender, EventArgs e)
        {
            lblPartType.Text = "Company Name";
        }

        //Set Field Color Red for Empty or Invalid Fields

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
            if (string.IsNullOrWhiteSpace(txtInventory.Text))
            {
                txtInventory.BackColor = System.Drawing.Color.Salmon;
            }
            if (!int.TryParse(txtInventory.Text, out int i))
            {
                txtInventory.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                txtInventory.BackColor = System.Drawing.Color.White;
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
                txtPrice.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                txtPrice.BackColor = System.Drawing.Color.White;
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

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMin.Text))
            {
                txtMin.BackColor = System.Drawing.Color.Salmon;
            }
            if (!int.TryParse(txtMin.Text, out int i))
            {
                txtMin.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                txtMin.BackColor = System.Drawing.Color.White;
            }
        }

        private void txtPartType_TextChanged(object sender, EventArgs e)
        {
            if (rbInHouse.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPartType.Text))
                {
                    txtPartType.BackColor = System.Drawing.Color.Salmon;
                }
                if (!int.TryParse(txtPartType.Text, out int i))
                {
                    txtPartType.BackColor = System.Drawing.Color.Salmon;
                }
                else
                {
                    txtPartType.BackColor = System.Drawing.Color.White;
                }
            }
            if (rbOutsourced.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPartType.Text))
                {
                    txtPartType.BackColor = System.Drawing.Color.Salmon;
                }
                else
                {
                    txtPartType.BackColor = System.Drawing.Color.White;
                }
            }
        }

        //Save and Cancel Buttons

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validateValues()) { }
            else
            {
                if (rbInHouse.Checked == true)
                {
                    InHousePart inHousePart = new InHousePart((TxtPartID), TxtName, TxtInventory, TxtPrice, TxtMin, TxtMax, TxtMachineID);
                    Inventory.addPart(inHousePart);
                }
                if (rbOutsourced.Checked == true)
                {
                    OutsourcedPart outsourcedPart = new OutsourcedPart((TxtPartID), TxtName, TxtInventory, TxtPrice, TxtMin, TxtMax, TxtCompanyName);
                    Inventory.addPart(outsourcedPart);
                }

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
