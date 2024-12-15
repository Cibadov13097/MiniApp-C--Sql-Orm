namespace Restaurant.Service.Exceptions
{
    public class DoesNotExistException:Exception
    {
        public DoesNotExistException(string message) :base(message){
                
        }
    }
}
