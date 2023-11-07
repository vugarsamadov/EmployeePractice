using November4Practice.ExceptionRelated;
using November4Practice.Helpers;
using November4Practice.Models;
using November4Practice.Services;

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
            startover:
            try
            {
                EmployeeUpdateCommand command = DisplayAndGetCommand(UpdateCommands);
                switch(command){
                    case EmployeeUpdateCommand.Edit_Name:
                        var name = InputHelper.PromptAndGetNonEmptyString("Name: ");
                        EmployeeService.UpdateEmployeeName(id,name);
                        break;
                    case EmployeeUpdateCommand.Edit_Gender:
                        var gender = EmployeeInputHelpers.GetGenderFromUser(PrintBuffer);
                        EmployeeService.UpdateEmployeeGender(id,gender);
                        break;
                    case EmployeeUpdateCommand.Edit_Salary:
                        var salary = InputHelper.PromptAndGetPositiveDecimal("Salary: ");
                        EmployeeService.UpdateEmployeeSalary(id,salary);
                        break;
                    case EmployeeUpdateCommand.Edit_Position:
                        var position = EmployeeInputHelpers.GetPositionFromUser(PrintBuffer);
                        EmployeeService.UpdateEmployeePosition(id,position);
                        break;
                }
            }catch(Exception e)
            {
                BufferError(e.Message);
                goto startover;
            }
        }

        private void RemoveEmployee(int Id) => EmployeeService.RemoveEmployee(Id);
        private void AddEmployee(Employee employee) => EmployeeService.AddEmployee(employee);

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
            EmployeeService.GetAllEmployees().ForEach(e => Buffer += $">> {e}\n");
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
        private void PrintEmployeeById(int id) => Buffer = EmployeeService.GetEmployeeById(id).ToString();
        
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
