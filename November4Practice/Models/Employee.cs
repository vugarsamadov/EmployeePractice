using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.Models
{
    internal class Employee : Person
    {
        public Employee(string name, string surname, int age,decimal salary, string position, Gender gender)
            :base(name,surname,age)
        {
           
            Salary = salary;
            Position = position;
            Gender = gender;
        }

        public decimal Salary { get; set; }
        public string Position { get; set; }
        public Gender Gender { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Surname} {Gender} {Salary}";
        }
    }
}
