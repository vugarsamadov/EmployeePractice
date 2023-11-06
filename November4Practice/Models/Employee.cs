namespace November4Practice.Models
{
    internal class Employee : Person
    {
        public Employee(string name, string surname, int age,decimal salary, Position position, Gender gender)
            :base(name,surname,age)
        {
           
            Salary = salary;
            Position = position;
            Gender = gender;
        }

        public decimal Salary { get; set; }
        public Position Position { get; set; }
        public Gender Gender { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Surname} {Gender} {Salary} {Position}";
        }
    }
}
