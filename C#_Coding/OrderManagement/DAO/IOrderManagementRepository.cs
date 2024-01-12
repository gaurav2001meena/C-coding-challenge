using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Entity;

namespace OrderManagement.DAO
{
    internal interface IOrderManagementRepository
    {
        bool CreateOrder(User user, List<Product> products,int quantity);
        bool CancelOrder(int userid, int orderid);
        bool CreateProduct(User user, Product product);
        bool CreateUser(User user);
        List<Product> GetAllProducts();
        List<Tuple<Product,int>> GetOrderByUser();
    }
}
