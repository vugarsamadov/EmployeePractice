using November4Practice.ExceptionRelated;
using November4Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.Helpers
{
    internal static class EmployeeInputHelpers
    {
        public static Gender[] Genders = new Gender[] { Gender.Male, Gender.Female, Gender.Other };
        public static Position[] Positions = new Position[] { Position.Staff, Position.Manager, Position.Executive };
        
        /// <summary>
        /// Prompts And Gets Employee Props From User
        /// </summary>
        /// <param name="printBuffer">Used for interactive pickers</param>
        /// <returns>User created Employee</returns>
        public static Employee GetEmployeeFromUser(Action printBuffer)
        {
            var Name = InputHelper.PromptAndGetNonEmptyString("Name: ");
            var Surname = InputHelper.PromptAndGetNonEmptyString("Surname: ");
            var Age = InputHelper.PromptAndGetPositiveInt("Age: ");
            var Salary = InputHelper.PromptAndGetPositiveDecimal("Salary: ");
            var Position = GetPositionFromUser(printBuffer);
            var Gender = GetGenderFromUser(printBuffer);

            return new Employee(Name, Surname, Age, Salary, Position, Gender);
        }

        /// <summary>
        /// Display interactive UI to choose Gender
        /// </summary>
        /// <param name="printBuffer">Method is executed before printing commands</param>
        /// <returns>Gender Enum Type</returns>
        public static Gender GetGenderFromUser(Action printBuffer)
        {
            Console.WriteLine();

            int genderInt = InputHelper.DisplayAndGetCommandBySelection(Genders, printBuffer, "Choose gender");

            if (genderInt > Genders.Length - 1)
                throw ExceptionHelper.CommandInvalidException(genderInt);

            return (Gender)genderInt;
        }

        /// <summary>
        /// Display interactive UI to choose Position
        /// </summary>
        /// <param name="printBuffer"></param>
        /// <returns></returns>
        public static Position GetPositionFromUser(Action printBuffer)
        {
            Console.WriteLine();

            int positionInt = InputHelper.DisplayAndGetCommandBySelection(Positions, printBuffer, "Choose position");

            if (positionInt > Positions.Length - 1)
                throw ExceptionHelper.CommandInvalidException(positionInt);

            return (Position)positionInt;
        }
    }
}
