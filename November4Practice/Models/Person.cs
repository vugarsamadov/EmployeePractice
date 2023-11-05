using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.Models
{
    internal abstract class Person
    {
        private static int _id = 0;

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public virtual string FullName()
        {
            return Name + " " + Surname;
        }
        
        public Person(string name, string surname, int age) :base()
        {
            Id = _id;
            _id += 1;

            Name = name;
            Surname = surname;
            Age = age;
        }

        private int id;
        public int Id { get => id; init => id = value; }

    }
}
