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
            //Employees = new(){new Employee("Vugar","Samadov",20,3444,Position.Staff,Gender.Male),
            //new Employee("Saddam","Hussein",23,3444,Position.Executive,Gender.Male) };
        }

        //public List<Employee> Employees { get; private set; }


        //public void AddEmployee(Employee employee)
        //{
        //    Employees.Add(employee);
        //}

        //public void RemoveEmployee(Employee employee)
        //{
        //    Employees.Remove(employee);
        //}
        //public void RemoveEmployee(int id)
        //{
        //    Employees.Remove(GetEmployeeById(id));
        //}


        //public Employee GetEmployeeById(int id)
        //{
        //    var employee = Employees.SingleOrDefault(e => e.Id == id);
        //    if (employee != null)
        //        return employee;
        //    throw ExceptionHelper.EmployeeNotFoundException(id);
        //}

        //public List<Employee> GetAllEmployees() => Employees;

    }
}
