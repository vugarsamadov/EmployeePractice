using November4Practice.ExceptionRelated;
using November4Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice.Services
{
    internal static class EmployeeService
    {

        public static void AddEmployee(Employee employee)
        {
            EmployeeDatabase.Employees.Add(employee);
        }

        public static void RemoveEmployee(Employee employee)
        {
            EmployeeDatabase.Employees.Remove(employee);
        }

        public static void RemoveEmployee(int Id) 
        {
            EmployeeDatabase.Employees.Remove(GetEmployeeById(Id));
        }

        public static Employee GetEmployeeById(int Id)
        {
            var employee = EmployeeDatabase.Employees.SingleOrDefault(e => e.Id == Id);
            if (employee != null)
                return employee;
            throw ExceptionHelper.EmployeeNotFoundException(Id);
        }

        public static void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public static void UpdateEmployeeName(int id,string name)
        {
            var oldEmployee = EmployeeService.GetEmployeeById(id);
            oldEmployee.Name = name;
        }
        public static void UpdateEmployeeSurname(int id, string surname)
        {
            var oldEmployee = EmployeeService.GetEmployeeById(id);
            oldEmployee.Surname = surname;
        }
        public static void UpdateEmployeeGender(int id, Gender gender)
        {
            var oldEmployee = EmployeeService.GetEmployeeById(id);
            oldEmployee.Gender = gender;
        }
        public static void UpdateEmployeePosition(int id, Position poistion)
        {
            var oldEmployee = EmployeeService.GetEmployeeById(id);
            oldEmployee.Position = poistion;
        }
        public static void UpdateEmployeeAge(int id, int age)
        {
            var oldEmployee = EmployeeService.GetEmployeeById(id);
            oldEmployee.Age = age;
        }
        internal static void UpdateEmployeeSalary(int id, decimal salary)
        {
            var oldEmployee = EmployeeService.GetEmployeeById(id);
            oldEmployee.Salary = salary;
        }





        public static List<Employee> GetEmployeesByValue(string value)
        {
            return EmployeeDatabase.Employees.FindAll(e => e.Name.Contains(value) || e.Surname.Contains(value)).ToList();
        }
        public static List<Employee> GetLatestEmployees()
        {
            return EmployeeDatabase.Employees.FindAll(e => e.AccountAge <= 7).ToList();
        }

        public static List<Employee> GetAllEmployees() => EmployeeDatabase.Employees;

    }
}
