using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaRea_InventorySystem
{
    public class OutsourcedPart : Part
    {
        private string companyName;

        public OutsourcedPart() { }

        public OutsourcedPart(int partID, string name, int inStock, decimal price, int min, int max, string companyName)
        {
            PartID = partID;
            Name = name;
            InStock = inStock;
            Price = price;
            Min = min;
            Max = max;
            CompanyName = companyName;
        }

        public string CompanyName { get; set; }
    }
}
