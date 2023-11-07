using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.ExceptionRelated
{
    internal class InvalidSurnameException : Exception
    {
        public InvalidSurnameException()
        {
        }

        public InvalidSurnameException(string? message) : base(message)
        {
        }

        public InvalidSurnameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidSurnameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
