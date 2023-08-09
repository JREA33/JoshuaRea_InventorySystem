using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaRea_InventorySystem
{
    public class InHousePart : Part
    {
        private int machineID;

        public InHousePart() { }
        public InHousePart(int partID, string name, int inStock, decimal price, int min, int max, int machineID) 
        { 
            PartID = partID;
            Name = name;
            InStock = inStock;
            Price = price;
            Min = min;
            Max = max;
            MachineID = machineID;
        }

        public int MachineID { get; set; }
    }
}
