using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaRea_InventorySystem
{
    public class Product
    {
        //Create Attributes
        private int productID;
        private string name;
        private int inStock;
        private decimal price;
        private int min;
        private int max;

        //Create AssociatedParts Binding List
        public BindingList<Part> AssociatedParts = new BindingList<Part>();

        //Create Constructors
        public Product() { }
        public Product(int productID, string name, int inStock, decimal price, int min, int max)
        {
            ProductID = productID;
            Name = name;
            InStock = inStock;
            Price = price;
            Min = min;
            Max = max;
        }

        //Create Getters/Setters
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        //Method to Add Part to AssociatedParts Binding List
        public void addAssociatedPart(Part associatedPart)
        {
            AssociatedParts.Add(associatedPart);
        }

        //Method to Remove Part from AssociatedParts Binding List
        public bool removeAssociatedPart(int associatedPartID)
        {
            bool remove = false;

            foreach (Part associatedPart in AssociatedParts)
            {
                if (associatedPart.PartID == associatedPartID)
                {
                    AssociatedParts.Remove(associatedPart);
                    return remove = true;
                }
                else
                {
                    remove = false;
                }
            }
            return remove;
        }

        //Method to Lookup Part in Associated Parts Binding List
        public Part lookupAssociatedPart(int associatedPartID)
        {
            foreach (Part associatedPart in AssociatedParts)
            {
                if (associatedPart.PartID == associatedPartID)
                {
                    return associatedPart;
                }
            }
            return null;
        }
    }
}
