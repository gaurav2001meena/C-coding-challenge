using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Entity
{
    internal class Product
    {
        private int product_id;
        private string product_name;
        private string description;
        private decimal price;
        private int quantity_in_stock;
        private string type;

        public int Product_id { get { return product_id; } set { product_id = value; } }
        public string Product_name { get { return product_name; } set { product_name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public decimal Price { get { return price; } set { price = value; } }
        public int Quantity_in_stock { get { return quantity_in_stock; } set { quantity_in_stock = value; } }
        public string Type { get { return type; } set { type = value; } }

     }
}
