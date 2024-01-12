using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Exceptionn
{
    internal class OrderNotFoundException:Exception
    {
        public OrderNotFoundException(String msg) : base(msg)
        {

        }
    }
}
