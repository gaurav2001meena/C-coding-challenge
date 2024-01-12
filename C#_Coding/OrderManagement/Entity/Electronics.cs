using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Entity
{
    internal class Electronics : Product
    {
        private string brand;
        private int warranty_period;

        public string Brand { get { return brand; } set { brand = value; } }
        public int Warranty_period { get { return warranty_period; } set {warranty_period = value; } }
    }
}
