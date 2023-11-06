using November4Practice.ExceptionRelated;
using November4Practice.Helpers;
using November4Practice.Models;

namespace November4Practice
{
    internal class Ui
    {
        public Company Company { get; private set; }

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

        /// <summary>
        /// Prompts the initial commands
        /// </summary>
        public void PromptInitialCommandsAndStart()
        {
            InitialCommand Command = InitialCommand.Quit;
            do
            {
                    here:
                try
                {
                    Command = DisplayAndGetCommand(InitialCommands);

                    switch(Command)
                    {
                        case InitialCommand.Create_Employee:
                            AddEmployee(EmployeeInputHelpers.GetEmployeeFromUser(PrintBuffer));
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
                    goto here;
                }

            } while (Command != InitialCommand.Quit);
        }
        /// <summary>
        /// Asks user to choose prop to be updated and updates the prop
        /// </summary>
        /// <param name="id">Id of the employee</param>
        private void UpdateEmployee(int id)
        {
            var oldEmployee = Company.GetEmployeeById(id);
            startover:
            try
            {
                EmployeeUpdateCommand command = DisplayAndGetCommand(UpdateCommands);
                switch(command){
                    case EmployeeUpdateCommand.Edit_Name:
                        oldEmployee.Name = InputHelper.PromptAndGetNonEmptyString("Name: ");
                        break;
                    case EmployeeUpdateCommand.Edit_Gender:
                        oldEmployee.Gender = EmployeeInputHelpers.GetGenderFromUser(PrintBuffer);
                        break;
                    case EmployeeUpdateCommand.Edit_Salary:
                        oldEmployee.Salary = InputHelper.PromptAndGetPositiveDecimal("Salary: ");
                        break;
                    case EmployeeUpdateCommand.Edit_Position:
                        oldEmployee.Position = EmployeeInputHelpers.GetPositionFromUser(PrintBuffer);
                        break;
                }
            }catch(Exception e)
            {
                BufferError(e.Message);
                goto startover;
            }
        }

        private void RemoveEmployee(int v) => Company.RemoveEmployee(v);
        private void AddEmployee(Employee employee) => Company.Employees.Add(employee);

        /// <summary>
        /// Contains all transient messages (errors,warnings)
        /// </summary>
        public string Buffer { get; private set; } = string.Empty;
        
        /// <summary>
        /// Adds all employees to the buffer
        /// </summary>
        private void BufferAllEmployees()
        {
            Buffer = string.Empty;
            Company.Employees.ForEach(e => Buffer += $">> {e}\n");
        }

        /// <summary>
        /// Assigns error message to the buffer. Adds error prefix (!) to later handle console color
        /// </summary>
        /// <param name="msg">Error messages</param>
        private void BufferError(string msg)
        {
            Buffer = string.Empty;
            Buffer = $"(!) {msg}";
        }

        /// <summary>
        /// Adds Employee to the Buffer. Throws exception if not found
        /// </summary>
        /// <param name="id">Id of the employee</param>
        private void PrintEmployeeById(int id) => Buffer = Company.GetEmployeeById(id).ToString();
        
        /// <summary>
        /// Prints buffer to the console. Handles coloring (warning, error)
        /// </summary>
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

        /// <summary>
        /// Displays interactive UI and waits for input from user
        /// </summary>
        /// <typeparam name="T">Enum type that represents command</typeparam>
        /// <param name="commands">Array of command enum types</param>
        /// <returns>Command Enum</returns>
        public T DisplayAndGetCommand<T>(T[] commands ) where T : Enum
        {
            int commandInt = InputHelper.DisplayAndGetCommandBySelection(commands,PrintBuffer,"Choose command (use up/down arrow keys)");

            if (commandInt > commands.Length - 1)
                throw ExceptionHelper.CommandInvalidException(commandInt);

            return (T)(object)commandInt;
        }

    }
}
