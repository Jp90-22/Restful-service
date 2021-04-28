using System;

namespace WebAPI.Models
{
    public class Employee
    {
        public int EmployeeID {get; set;}
        public string Name {get; set;}
        public int ManagerID {get; set;}
    }
}