using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaRea_InventorySystem
{
    public abstract class Part
    {
        private int partID;
        private string name;
        private int inStock;
        private decimal price;
        private int min;
        private int max;
        public Part() { }

        public Part(int PartID, string Name, int InStock, decimal Price, int Min, int Max)
        {
            partID = PartID;
            name = Name;
            inStock = InStock;
            price = Price;
            min = Min;
            max = Max;
        }

        public int PartID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
