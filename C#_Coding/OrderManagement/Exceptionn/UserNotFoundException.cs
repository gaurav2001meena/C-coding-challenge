using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Exceptionn
{
    internal class UserNotFoundException:Exception
    {
        public UserNotFoundException(String msg) : base(msg)
        {

        }
    }
}
