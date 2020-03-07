using Models.Exeptions;

namespace BlogApi.Models.Exceptions
{
    public class ServiceException : GeneralException
    {
        public ServiceException(string message) : base(message, 500)
        {

        }
    }
}
