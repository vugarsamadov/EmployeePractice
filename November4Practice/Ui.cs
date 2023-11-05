using November4Practice.ExceptionRelated;
using November4Practice.Helpers;
using November4Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace November4Practice
{
   
    public enum InitialCommand
    {
        Create_Employee,
        Get_Employee_By_Id,
        Get_All_Employees,
        Update_Employee,
        Remove_Employee,
        Quit
    }
    public enum EmployeeUpdateCommand
    {
        Edit_Name,
        Edit_Gender,
        Edit_Salary,
        Edit_Position,
        Quit
    }

    internal class Ui
    {
        public Company Company { get; private set; }
        public string Buffer { get; private set; } = string.Empty;

        public InitialCommand[] InitialCommands { get; init; } = 
            new InitialCommand[] {
                InitialCommand.Create_Employee,
                InitialCommand.Get_Employee_By_Id,
                InitialCommand.Get_All_Employees,
                InitialCommand.Update_Employee,
                InitialCommand.Remove_Employee,
                InitialCommand.Quit
            };

public EmployeeUpdateCommand[] UpdateCommands { get; init; } = 
            new EmployeeUpdateCommand[] {
                EmployeeUpdateCommand.Edit_Name,
                EmployeeUpdateCommand.Edit_Gender,
                EmployeeUpdateCommand.Edit_Salary,
                EmployeeUpdateCommand.Edit_Position,
                EmployeeUpdateCommand.Quit
            };

        public Ui()
        {
            Company = new(" E Corp");
        }

        public void Start()
        {
            InitialCommand Command = InitialCommand.Quit;
            startover:
            do
            {
                try
                {
                    
                    Command = DisplayAndGetCommand();

                    switch(Command)
                    {
                        case InitialCommand.Create_Employee:
                            AddEmployee(InputHelper.GetEmployeeFromUser());
                            break;
                        case InitialCommand.Get_Employee_By_Id:
                            PrintEmployeeById(InputHelper.PromptAndGetPositiveInt("Employee Indx: "));
                            break;
                        case InitialCommand.Get_All_Employees:
                            BufferAllEmployees();
                            break;
                        case InitialCommand.Update_Employee:
                            BufferAllEmployees();
                            Display();
                            TryUpdateEmployee(InputHelper.PromptAndGetPositiveInt("Employee Id: "));
                            break;
                        case InitialCommand.Remove_Employee:
                            RemoveEmployee(InputHelper.PromptAndGetPositiveInt("Employee Id: "));
                            break;
                    }
                }
                catch (Exception ex) 
                {
                    BufferError(ex.Message);
                    goto startover;
                }

            } while (Command != InitialCommand.Quit);
        }
        private void TryUpdateEmployee(int id)
        {
            var oldEmployee = Company.GetEmployeeById(id);
            UpdateEmployee(oldEmployee);
        }
        private void UpdateEmployee(Employee oldEmployee)
        {
            EmployeeUpdateCommand command = DisplayAndGetUpdateEmployeeCmd();
            switch(command){
                case EmployeeUpdateCommand.Edit_Name:
                    oldEmployee.Name = InputHelper.PromptAndGetNonEmptyString("Name: ");
                    break;
                case EmployeeUpdateCommand.Edit_Gender:
                    oldEmployee.Gender = InputHelper.GetGenderFromUser();
                    break;
                case EmployeeUpdateCommand.Edit_Salary:
                    oldEmployee.Salary = InputHelper.PromptAndGetPositiveDecimal("Salary: ");
                    break;
                case EmployeeUpdateCommand.Edit_Position:
                    oldEmployee.Position = InputHelper.PromptAndGetNonEmptyString("Position: ");
                    break;
            }
        }

        private void RemoveEmployee(int v)
        {
            Company.RemoveEmployee(v);
        }

        private void AddEmployee(Employee employee)
        {
            Company.Employees.Add(employee);
        }

        private void BufferAllEmployees()
        {
            foreach (var employee in Company.Employees)
            {
                Buffer += $">> {employee}\n";
            }
        }
        private void BufferError(string msg)
        {
            Buffer += $"(!) {msg}";
        }

        private void PrintEmployeeById(int id)
        {
            var employee = Company.GetEmployeeById(id);
            Buffer += employee;
        }


        public void PrintCompanyInfo()
        {
            Console.WriteLine(@$"
================================================================================
            {Company.Name} has {Company.Employees.Count()} employees
================================================================================");
        }
        public void PrintBuffer()
        {
            if(Buffer != string.Empty)
            {
                if(Buffer.StartsWith("(!)"))
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"\n{Buffer}\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Buffer = string.Empty;
        }

        public InitialCommand DisplayAndGetCommand()
        {
            Display();
            int commandInt = InputHelper.PromptAndGetPositiveInt("Enter command: ");
                        if( commandInt > InitialCommands.Length - 1 )
                throw ExceptionHelper.CommandInvalidException( commandInt );

            return (InitialCommand) commandInt;
        }
        public void Display()
        {
            Console.Clear();

            PrintCompanyInfo();
            PrintBuffer();
            foreach (var command in InitialCommands)
            {
                Console.WriteLine($"{(int)command} > {command}");
            }
        }

        public EmployeeUpdateCommand DisplayAndGetUpdateEmployeeCmd()
        {
            
            int commandInt;
            do
            {
                Console.Clear();
                PrintCompanyInfo();
                PrintBuffer();
                foreach (var command in UpdateCommands)
            {
                Console.WriteLine($"{(int)command} > {command}");
            }

            commandInt = InputHelper.PromptAndGetPositiveInt("Enter command: ");

            if (commandInt > UpdateCommands.Length - 1)
                    BufferError("Invalid command");

            } while (commandInt>4);

            return (EmployeeUpdateCommand)commandInt;
        }



    }



}
