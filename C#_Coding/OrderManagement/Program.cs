using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Entity;
using OrderManagement.DAO;

namespace OrderManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int flag = 1, ch;
            Boolean res;

            OrderProcessor orderProcessor = new OrderProcessor();


            do
            {
                Console.WriteLine("1.Create User.......");
                Console.WriteLine("2.Create Product.......");
                Console.WriteLine("3.Create Order.......");
                Console.WriteLine("4.Cancel Order.......");
                Console.WriteLine("5.get all products.......");
                Console.WriteLine("6.get order by user.......");

                Console.WriteLine("7.Exit........\n");
                ch = Convert.ToInt32(Console.ReadLine());

                switch (ch)
                {
                    case 1:createuser();
                        
                        break;

                    case 2:
                        createproduct();
                        
                        break;


                    case 3:


                        createorder();
                        

                        break;
                    case 4:
                        cancelorder();

                        break;


                    case 5:
                        getallproduct();
                        
                        break;

                    case 6:


                        getorderbyuser();

                        break;



                    case 7:
                        flag = 0;
                        break;

                    default:
                        Console.WriteLine("Invalid input.....");
                        break;
                }




            } while (flag == 1);

            void createuser()
            {
                try
                {
                    User user = new User();

                    Console.Write("Enter user Name::");
                    user.Username = Console.ReadLine();

                    Console.Write("Enter User password::");
                    user.Password = Console.ReadLine();

                    Console.Write("Enter User Role::");
                    user.Role = Console.ReadLine();

                    res = orderProcessor.CreateUser(user);
                    if (res)
                    {
                        Console.WriteLine("succefully inserted\n\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    
                }

            }

            void createorder()
            {
                try
                {
                    List<Product> list2 = new List<Product>();
                    Product product1 = new Product();
                    User user2 = new User();

                    Console.WriteLine("Enter user id");
                    user2.UserId = Convert.ToInt32(Console.ReadLine());
                    //checking user exist
                    bool ress = OrderProcessor.checkUserExist(user2.UserId);
                    if (ress)
                    {


                        Console.WriteLine("Enter product id");
                        product1.Product_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter quantity");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        list2.Add(product1);
                        res = orderProcessor.CreateOrder(user2, list2, quantity);
                        if (res)
                        {
                            Console.WriteLine("succefully inserted");
                        };
                    }
                    else
                    {
                        createuser();
                        createorder();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }

            void createproduct()
            {
                try
                {
                    User user1 = new User();
                    Product product = new Product();

                    Console.Write("Enter user id::");
                    user1.UserId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Product name::");
                    product.Product_name = Console.ReadLine();

                    Console.Write("Enter Product description::");
                    product.Description = Console.ReadLine();

                    Console.Write("Enter Product price::");
                    product.Price = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter stock::");
                    product.Quantity_in_stock = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Product type::");
                    product.Type = Console.ReadLine();

                    res = orderProcessor.CreateProduct(user1, product);
                    if (res)
                    {
                        Console.WriteLine("succefully inserted");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }

            void cancelorder()
            {
                try
                {
                    Console.Write("Enter user ID::");
                    int userid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter orderid ::");
                    int orderid = Convert.ToInt32(Console.ReadLine());


                    res = orderProcessor.CancelOrder(userid, orderid);
                    if (res)
                    {
                        Console.WriteLine("succefully deleted");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }

            void getallproduct()
            {
                try
                {

                    List<Product> list1 = orderProcessor.GetAllProducts();
                    foreach (Product p in list1)
                    {
                        Console.WriteLine($"\nproductid={p.Product_id}\tprice={p.Price} \tname={p.Product_name}\t description={p.Description}\t stock= {p.Quantity_in_stock}\t type={p.Type}\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }

            void getorderbyuser()
            {
                try
                {
                    List<Tuple<Product, int>> tuples = orderProcessor.GetOrderByUser();
                    foreach (Tuple<Product, int> tuple in tuples)
                    {
                        Console.WriteLine($"Product_ID={tuple.Item1.Product_id} \tProduct Name={tuple.Item1.Product_name} \t Quantity={tuple.Item2}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
        }

    }
}
