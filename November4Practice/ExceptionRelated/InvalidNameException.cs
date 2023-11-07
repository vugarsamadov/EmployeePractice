﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.ExceptionRelated
{
    internal class InvalidNameException : Exception
    {
        public InvalidNameException()
        {
        }

        public InvalidNameException(string? message) : base(message)
        {
        }

        public InvalidNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
