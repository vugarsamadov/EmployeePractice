﻿using November4Practice.ExceptionRelated;
using November4Practice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.Helpers
{
    internal static class InputHelper
    {
        public static string PromptAndGetNonEmptyString(string prompt)
        {
            string input = null;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(input) || input == string.Empty)
                    Console.WriteLine("Empty input is not allowed!");

            } while (String.IsNullOrWhiteSpace(input) || input == string.Empty);

            return input;
        }

        public static int PromptAndGetPositiveInt(string prompt)
        {
            int input = 0;
            do
            {
                input = Convert.ToInt32(PromptAndGetNonEmptyString(prompt));

                if (input < 0)
                    Console.WriteLine("Positive input is required!");

            } while (input < 0);

            return input;
        }

        public static int PromptAndGetPositiveRangedInt(string prompt, int maxRange)
        {
            int input = 0;
            do
            {
                input = PromptAndGetPositiveInt(prompt);

                if (input > maxRange)
                    Console.WriteLine($"Positive input with {maxRange} max value is required!");

            } while (input > maxRange);

            return input;
        }

        public static int PromptAndGetPositiveNzInt(string prompt)
        {
            int input = 0;
            do
            {
                input = Convert.ToInt32(PromptAndGetNonEmptyString(prompt));

                if (input <= 0)
                    Console.WriteLine("Positive non zero input is required!");

            } while (input <= 0);

            return input;
        }

        public static decimal PromptAndGetPositiveDecimal(string prompt)
        {
            decimal input = 0;
            do
            {
                input = Convert.ToDecimal(PromptAndGetNonEmptyString(prompt));

                if (input <= 0m)
                    Console.WriteLine("Positive non zero input is required!");

            } while (input < 0m);

            return input;
        }


        public static Employee GetEmployeeFromUser()
        {
            var Name = PromptAndGetNonEmptyString("Name: ");
            var Surname = PromptAndGetNonEmptyString("Surname: ");
            var Age = PromptAndGetPositiveInt("Age: ");
            var Salary = PromptAndGetPositiveDecimal("Salary: ");
            var Position = PromptAndGetNonEmptyString("Position: ");
            var Gender = GetGenderFromUser();

            return new Employee(Name, Surname, Age, Salary, Position, Gender);
        }


        public static Gender[] Genders = new Gender[] { Gender.Male, Gender.Female, Gender.Other };
        public static Gender GetGenderFromUser()
        {
            Console.WriteLine();
            foreach (var gender in Genders)
            {
                Console.WriteLine($"{(int)gender} > {gender}");
            }
            
            int genderInt = PromptAndGetPositiveRangedInt("Gender: ",Genders.Length - 1);
            
            return (Gender)genderInt;
        }

    }
}
