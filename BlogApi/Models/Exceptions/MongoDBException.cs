using Models.Exeptions;

namespace BlogApi.Models.Exceptions
{
    public class MongoDBException : GeneralException
    {
        public MongoDBException(string message) : base(message, 500)
        {

        }
    }
}
