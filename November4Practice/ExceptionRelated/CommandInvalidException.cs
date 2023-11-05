using System.Runtime.Serialization;

namespace November4Practice.ExceptionRelated
{
    internal class CommandInvalidException : Exception
    {
        public CommandInvalidException()
        {
        }

        public CommandInvalidException(string? message) : base(message)
        {
        }

        public CommandInvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CommandInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}