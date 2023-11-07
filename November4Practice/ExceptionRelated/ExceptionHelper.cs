using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.ExceptionRelated
{
    internal static class ExceptionHelper
    {
        public static EmployeeNotFoundException EmployeeNotFoundException(int? id = null)
            => new EmployeeNotFoundException($"Employee with {id} is not found!");

        public static CommandInvalidException CommandInvalidException(int? command = null)
            => new CommandInvalidException($"Input command ({command}) is invalid!");

        public static InvalidNameException InvalidNameException(string? name = null)
            => new InvalidNameException($"Name ({name}) is invalid!");

        public static InvalidSurnameException InvalidSurnameException(string? surname = null)
            => new InvalidSurnameException($"Surname ({surname}) is invalid!");


    }
}
