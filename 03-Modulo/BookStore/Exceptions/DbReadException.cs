using System.Data.Common;

namespace BookStore.Exceptions
{
    public class DbReadException : DbException
    {
        public override string Message => "Read operation failed."; 
    }
}
