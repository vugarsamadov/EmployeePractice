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

        public void PrintCompanyInfo()
        {
            Console.WriteLine(@$"
================================================================================
            {Company.Name} has {Company.Employees.Count()} employees
================================================================================");
        }

        public void PromptInitialCommandsAndStart()
        {

            //s
            InitialCommand Command = InitialCommand.Quit;
            do
            {
                try
                {
                    Command = DisplayAndGetCommand(InitialCommands);

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
                            UpdateEmployee(InputHelper.PromptAndGetPositiveInt("Employee Id: "));
                            break;
                        case InitialCommand.Remove_Employee:
                            RemoveEmployee(InputHelper.PromptAndGetPositiveInt("Employee Id: "));
                            break;
                    }
                }
                catch (Exception ex) 
                {
                    BufferError(ex.Message);
                    PromptInitialCommandsAndStart();
                }

            } while (Command != InitialCommand.Quit);
        }

        private void UpdateEmployee(int id)
        {
            var oldEmployee = Company.GetEmployeeById(id);
            try
            {
                EmployeeUpdateCommand command = DisplayAndGetCommand(UpdateCommands);
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
            }catch(Exception e)
            {
                BufferError(e.Message);
                UpdateEmployee(id);
            }
        }

        private void RemoveEmployee(int v) => Company.RemoveEmployee(v);

        private void AddEmployee(Employee employee) => Company.Employees.Add(employee);

        private void BufferAllEmployees()
        {
            Buffer = string.Empty;
            Company.Employees.ForEach(e => Buffer += $">> {e}\n");
        }

        private void BufferError(string msg)
        {
            Buffer = string.Empty;
            Buffer = $"(!) {msg}";
        }

            private void PrintEmployeeById(int id) => Buffer = Company.GetEmployeeById(id).ToString();
        
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
            Console.ResetColor();
            Buffer = string.Empty;
        }

        public void Display<T>(T[] commands) where T: Enum
        {
            Console.Clear();
            PrintCompanyInfo();
            PrintBuffer();
            foreach (T command in commands)
            {
                Console.WriteLine($"{Convert.ToInt32(command)} > {command}");
            }
        }

        public T DisplayAndGetCommand<T>(T[] commands ) where T : Enum
        {
            Display(commands);
            int commandInt = InputHelper.PromptAndGetPositiveInt("Enter command: ");

            if (commandInt > commands.Length - 1)
                throw ExceptionHelper.CommandInvalidException(commandInt);

            return (T)(object)commandInt;
        }

    }
}
