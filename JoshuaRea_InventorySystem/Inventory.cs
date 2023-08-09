using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoshuaRea_InventorySystem
{
    public class Inventory
    {
        //Create Binding Lists
        public static BindingList<Product> Products = new BindingList<Product>();
        public static BindingList<Part> AllParts = new BindingList<Part>();

        //Create Sample Data
        public static void createInventory()
        {
            //Create Test Parts
            Part testPartA = new InHousePart(0, "Wheel", 15, 12.11m, 5, 25, 1);
            Part testPartB = new InHousePart(1, "Pedal", 11, 8.22m, 5, 25, 2);
            Part testPartC = new OutsourcedPart(2, "Chain", 12, 8.33m, 5, 25, "SS Supply");
            Part testPartD = new InHousePart(3, "Seat", 8, 4.55m, 2, 15, 3);

            //Add Test Parts
            AllParts.Add(testPartA);
            AllParts.Add(testPartB);
            AllParts.Add(testPartC);
            AllParts.Add(testPartD);

            //Create Test Products
            Product testProductA = new Product(0, "Red Bicycle", 15, 11.44m, 1, 25);
            Product testProductB = new Product(1, "Yellow Bicycle", 19, 9.66m, 1, 20);
            Product testProductC = new Product(2, "Blue Bicycle", 5, 12.77m, 1, 25);

            //Add Test Products
            Products.Add(testProductA);
            Products.Add(testProductB);
            Products.Add(testProductC);

            //Add Associated Parts to Products
            testProductA.AssociatedParts.Add(testPartA);
            testProductB.AssociatedParts.Add(testPartB);
            testProductC.AssociatedParts.Add(testPartC);
        }

        //Methods for Parts
        public static void addPart(Part part)
        {
            AllParts.Add(part);
        }

        public static bool deletePart(Part part)
        {
            AllParts.Remove(part);
            return true;
        }

        public static Part lookupPart(int partID)
        {
            foreach (Part part in AllParts)
            {
                if (part.PartID == partID)
                {
                    return part;
                }
            }
            return null;
        }

        public static void updatePart(int partID, Part part)
        {
            for (int i = 0; i < AllParts.Count; i++)
            {
                if (AllParts[i].PartID == partID)
                {
                    deletePart(AllParts[i]);
                    addPart(part);
                }
            }
        }


        //Product Methods
        public static void addProduct(Product product)
        {
            Products.Add(product);
        }
        public static bool removeProduct(int productID)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Product deletedProduct = Products[i];

                if (deletedProduct.ProductID == productID)
                {
                    if (deletedProduct.AssociatedParts.Count == 0)
                    {
                        DialogResult dialog = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            Products.Remove(deletedProduct);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product cannot be deleted because a part is assigned to the product.");
                    }
                }
            }
            return false;

        }
        public static Product lookupProduct(int productID)
        {
            foreach (Product product in Products)
            {
                if (product.ProductID == productID)
                {
                    return product;
                }
            }
            return null;
        }

        public static void updateProduct(int productID, Product product)
        {
            foreach (Product updatedProduct in Products)
            {
                if (updatedProduct.ProductID == productID)
                {
                    updatedProduct.Name = product.Name;
                    updatedProduct.InStock = product.InStock;
                    updatedProduct.Price = product.Price;
                    updatedProduct.Max = product.Max;
                    updatedProduct.Min = product.Min;

                    updatedProduct.AssociatedParts = product.AssociatedParts;
                }
            }
        }
    }
}
