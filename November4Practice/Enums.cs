using System;
using System.Collections.Generic;
using System.Linq;
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
}
