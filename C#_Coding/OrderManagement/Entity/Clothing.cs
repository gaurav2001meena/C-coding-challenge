using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Entity
{
    internal class Clothing : Product
    {
        private int size;
        private string color;

        public int Size { get { return size; } set { size = value; } }
        public string Color { get { return color; } set { color = value; } }
    }
}
