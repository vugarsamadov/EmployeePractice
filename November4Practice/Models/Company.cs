using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using November4Practice.ExceptionRelated;

namespace November4Practice.Models
{
    internal class Company
    {
        public string Name { get; init; }
        public Company(string name)
        {
            Name = name;
            Employees = new(){new Employee("Vugar","Samadov",20,3444,"D",Gender.Male),
            new Employee("Saddam","Hussein",23,3444,"D",Gender.Male) };
        }

        public List<Employee> Employees { get; private set; }


        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            Employees.Remove(employee);
        }
        public void RemoveEmployee(int id)
        {
            Employees.Remove(GetEmployeeById(id));
        }


        public Employee GetEmployeeById(int id)
        {
            foreach (Employee employee in Employees)
            {
                if (employee.Id == id)
                    return employee;
            }
            throw ExceptionHelper.EmployeeNotFoundException(id);
        }

        //public void UpdateEmployee(Employee employee)
        //{
        //    var oldEmployee = GetEmployeeById(employee.Id);

        //    oldEmployee.Name = employee.Name;
        //    oldEmployee.Surname = employee.Surname;
        //    oldEmployee.Age = employee.Age;
        //    oldEmployee.Gender = employee.Gender;
        //    oldEmployee.Salary = employee.Salary;

        //}

        public List<Employee> GetAllEmployees() => Employees;

    }
}
