using November4Practice.ExceptionRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.Validator
{
    internal static class Validators
    {
        public static void ValidateName(string name)
        {
            if (name.Length <= 2)
                throw ExceptionHelper.InvalidNameException(name);
        }

        public static void ValidateSurname(string surname)
        {
            if (surname.Length <= 2)
                throw ExceptionHelper.InvalidSurnameException(surname);
        }
    }
}
