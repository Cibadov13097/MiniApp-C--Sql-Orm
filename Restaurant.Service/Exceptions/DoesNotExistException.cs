using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Service.Exceptions
{
    public class DoesNotExistException:Exception
    {
        public DoesNotExistException(string message) :base(message){
                
        }
    }
}
