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
        CreateEmployee,
        GetEmployeeById,
        GetAllEmployees,
        UpdateEmployee,
        RemoveEmployee,
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
        public string Status { get; private set; } = string.Empty;

        public InitialCommand[] InitialCommands { get; init; } = 
            new InitialCommand[] {
                InitialCommand.CreateEmployee,
                InitialCommand.GetEmployeeById,
                InitialCommand.GetAllEmployees,
                InitialCommand.UpdateEmployee,
                InitialCommand.RemoveEmployee,
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
                        case InitialCommand.CreateEmployee:
                            CreateAndAddEmployee(InputHelper.GetEmployeeFromUser());
                            break;
                        case InitialCommand.GetEmployeeById:
                            PrintEmployeeById(InputHelper.PromptAndGetPositiveInt("Employee Indx: "));
                            break;
                        case InitialCommand.GetAllEmployees:
                            PrintAllEmployees();
                            break;
                        case InitialCommand.UpdateEmployee:
                            UpdateEmployee(InputHelper.PromptAndGetPositiveRangedInt("Employee Id: ",4));
                            break;
                        case InitialCommand.RemoveEmployee:
                            RemoveEmployee(InputHelper.PromptAndGetPositiveInt("Employee Id: "));
                            break;
                    }
                }
                catch (Exception ex) 
                {
                    Status = ex.Message;
                    goto startover;
                }

            } while (Command != InitialCommand.Quit);
        }

        private void UpdateEmployee(int id)
        {
            var oldEmployee = Company.GetEmployeeById(id);
         
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

        private void CreateAndAddEmployee(Employee employee)
        {
            Company.Employees.Add(employee);
        }

        private void PrintAllEmployees()
        {
            foreach (var employee in Company.Employees)
            {
                Status += $"{employee} \n";
            }
        }
        private void PrintEmployeeById(int id)
        {
            var employee = Company.GetEmployeeById(id);
            Status += employee;
        }


        public void PrintCompanyInfo()
        {
            Console.WriteLine(@$"
================================================================================
            {Company.Name} has {Company.Employees.Count()} employees
================================================================================");
        }
        public void PrintStatus()
        {
            if(Status != string.Empty)
                Console.WriteLine($"\n>> {Status}\n");
            Status = string.Empty;
        }

        public InitialCommand DisplayAndGetCommand()
        {
            Console.Clear();

            PrintCompanyInfo();
            PrintStatus();
            foreach(var command in InitialCommands)
            {
                Console.WriteLine($"{(int)command} > {command}");
            }
            
            int commandInt = InputHelper.PromptAndGetPositiveInt("Enter command: ");
                        if( commandInt > InitialCommands.Length - 1 )
                throw ExceptionHelper.CommandInvalidException( commandInt );

            return (InitialCommand) commandInt;
        }


        public EmployeeUpdateCommand DisplayAndGetUpdateEmployeeCmd()
        {
            
            int commandInt;
            do
            {
                Console.Clear();
                PrintCompanyInfo();
                PrintStatus();
                foreach (var command in UpdateCommands)
            {
                Console.WriteLine($"{(int)command} > {command}");
            }

            commandInt = InputHelper.PromptAndGetPositiveInt("Enter command: ");

            if (commandInt > UpdateCommands.Length - 1)
                    Status+="invalid command";

            } while (commandInt>4);

            return (EmployeeUpdateCommand)commandInt;
        }



    }



}
