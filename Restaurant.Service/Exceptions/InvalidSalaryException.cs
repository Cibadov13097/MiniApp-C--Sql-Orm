using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Exceptions
{
    internal class InvalidSalaryException:Exception
    {
        public InvalidSalaryException(string message):base(message)
        {
                
        }
    }
}
