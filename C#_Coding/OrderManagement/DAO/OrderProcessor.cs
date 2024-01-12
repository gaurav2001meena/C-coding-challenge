using OrderManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OrderManagement.Util;
using OrderManagement.Exceptionn;

namespace OrderManagement.DAO
{
    
    internal class OrderProcessor : IOrderManagementRepository
    {
        static SqlConnection conn = null;
        public bool CancelOrder(int userid, int orderid)
        {
            try
            {
                bool che=checkUserExist(userid);
                bool che2 = checkorderExist(orderid);
                if (che==true && che2==true)
                {
                    conn = DButil.GetConnection();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"delete from orders where orderid={orderid} and userid={userid}; ";
                    conn.Open();

                    int rowcount = cmd.ExecuteNonQuery();

                    if (rowcount > 0)
                    {
                        return true;
                    }
                    else 
                    { 
                        return false; 
                    }
                }
                else if(che==false)
                {
                    
                    throw new UserNotFoundException("No user id found");
                    
                    
                }
                else 
                {
                    throw new OrderNotFoundException("orderid id not found");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool CreateOrder(User user, List<Product> products,int quantity)
        {
            try
            {
                conn = DButil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $" select price from product where productid = {products[0].Product_id};";
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                decimal price = (decimal)dr[0];
                decimal total_price = price * quantity;
                dr.Close();

                cmd.CommandText = $"INSERT INTO orders VALUES( {user.UserId},{products[0].Product_id},{quantity},{total_price});";

                int rowcount = cmd.ExecuteNonQuery();
                cmd.CommandText = "select SCOPE_IDENTITY();";
                object NewId = cmd.ExecuteScalar();
                Console.WriteLine($"your order id{NewId}");



                if (rowcount > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }



        }

        public bool CreateProduct(User user, Product product)
        {
            try
            {
                bool ad=adminExist(user.UserId);
                if (ad == true)
                {
                    conn = DButil.GetConnection();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"insert into product values('{product.Product_name}','{product.Description}','{product.Price}','{product.Quantity_in_stock}','{product.Type}'); ";
                    conn.Open();

                    int rowcount = cmd.ExecuteNonQuery();
                    if (rowcount > 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
                else
                {
                    throw new Exception("admin not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool CreateUser(User user)
        {
            try
            {
                conn = DButil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"insert into userr values('{user.Username}','{user.Password}','{user.Role}'); ";
                conn.Open();

                int rowcount = cmd.ExecuteNonQuery();
                if (rowcount > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                List<Product> products = new List<Product>();
                conn = DButil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"select * from product ";
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    products.Add(new Product()
                    {
                        Product_id = (int)dr[0],
                        Product_name = dr[1].ToString(),
                        Description = dr[2].ToString(),
                        Price = (decimal)dr[3],
                        Quantity_in_stock = (int)dr[4],
                        Type = dr[5].ToString()
                    });
                }
                dr.Close();
                conn.Close();
                return products;
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<Tuple<Product, int>> GetOrderByUser()
        {
            try
            {
                Console.WriteLine("Enter the user id:");
                int id = Convert.ToInt32(Console.ReadLine());
                List<Tuple<Product, int>> tuples = new List<Tuple<Product, int>>();

                conn = DButil.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"select o.productid,p.productname,o.quantity from orders o  inner join product p on o.productid=p.productid where o.userid={id}; ";
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Product product = new Product();

                        product.Product_id = (int)dr[0];
                        product.Product_name = dr[1].ToString();
                        int quantity = (int)dr[2];

                        tuples.Add(new Tuple<Product, int>(product, quantity));
                    }

                    dr.Close();
                    conn.Close();
                    return tuples;
                }
                else
                {
                    throw new Exception("No data present");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //other methods for exception
        static public bool checkUserExist(int userid)
        {
            conn = DButil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM userr WHERE userid = {userid}) THEN 1 ELSE 0 END";
            conn.Open();
            int customerexistance = (int)cmd.ExecuteScalar();
            if (customerexistance == 1)
            {
                return true;
            }
            else
            {
                return false;
                
            }

        }

        static public bool checkorderExist(int orderid)
        {
            conn = DButil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM orders WHERE orderid = {orderid}) THEN 1 ELSE 0 END";
            conn.Open();
            int customerexistance = (int)cmd.ExecuteScalar();
            if (customerexistance == 1)
            {
                return true;
            }
            else
            {
                return false;

            }

        }

        static public bool adminExist(int userid)
        {
            conn = DButil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"SELECT role FROM userr WHERE userid = {userid};";
            conn.Open();
            string role = cmd.ExecuteScalar().ToString();
            if (role=="admin")
            {
                return true;
            }
            else
            {
                return false;

            }

        }

        static public bool userExist(int userid)
        {
            conn = DButil.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"SELECT role FROM userr WHERE userid = {userid}";
            conn.Open();
            string role = cmd.ExecuteScalar().ToString();
            if (role.Equals("user")) 
            {
                return true;
            }
            else
            {
                return false;

            }

        }
    }
}
