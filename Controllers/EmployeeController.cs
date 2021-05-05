using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private const string connectionStr = @"Server=localhost;Database=dbAPI;User ID=sql;Password=1234;";
        private SqlConnection myConnection;
        private SqlDataReader reader = null;
        private SqlCommand sqlCmd;

        // GET: /api/Employee/
        [HttpGet("{id}")]  
        // [ActionName("GetEmployeeByID")] 
        public Employee GetEmployeeByID(int id)  
        {  
            //return list of Emp;  
            myConnection = new SqlConnection();  
            myConnection.ConnectionString = connectionStr;  
        
            sqlCmd = new SqlCommand();  
            sqlCmd.CommandType = CommandType.Text;  
            sqlCmd.CommandText = "Select * from tblEmployee where EmployeeId=" + id + "";  
            sqlCmd.Connection = myConnection; 
            
            myConnection.Open();  
            reader = sqlCmd.ExecuteReader();  

            Employee emp = null;  
            while (reader.Read())  
            {  
                emp = new Employee();  
                emp.EmployeeID = Convert.ToInt32(reader.GetValue(0));  
                emp.Name = reader.GetValue(1).ToString();  
                emp.ManagerID = Convert.ToInt32(reader.GetValue(2));  
            }  

            myConnection.Close();
            return emp;  
        }
        
        // POST: /api/Employee/
        [HttpPost]
        public void AddEmployee(Employee employee)  
        {
            myConnection = new SqlConnection();  
            myConnection.ConnectionString = connectionStr; 
            
            sqlCmd = new SqlCommand();  
            sqlCmd.CommandType = CommandType.Text;  
            sqlCmd.CommandText = "INSERT INTO tblEmployee (EmployeeId,Name,ManagerId) Values (@EmployeeId,@Name,@ManagerId)";  
            sqlCmd.Connection = myConnection;  

            sqlCmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeID);  
            sqlCmd.Parameters.AddWithValue("@Name", employee.Name);  
            sqlCmd.Parameters.AddWithValue("@ManagerId", employee.ManagerID);  

            myConnection.Open();  
            int rowInserted = sqlCmd.ExecuteNonQuery(); //Insert  values (EmployeeId, Name, ManagerId) in a row !!is not a query
            myConnection.Close();  
        } 

        // DELETE: /api/Employee/
        [HttpDelete("{id}")]
        // [ActionName("DeleteEmployee")]  
        public void DeleteEmployeeByID(int id)  
        {  
            myConnection = new SqlConnection();  
            myConnection.ConnectionString = connectionStr;
            
            sqlCmd = new SqlCommand();  
            sqlCmd.CommandType = CommandType.Text;  
            sqlCmd.CommandText = "delete from tblEmployee where EmployeeId=" + id + "";  
            sqlCmd.Connection = myConnection;  
            
            myConnection.Open();  
            int rowDeleted = sqlCmd.ExecuteNonQuery();  
            myConnection.Close();  
        }

        // PUT: /api/Employee/
        [HttpPut]
        // [ActionName("UpdateEmployee")]
        public string UpdateEmployeeByID(int id, Employee employee)
        {
            myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionStr;

            sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update tblEmployee " +
                        "set EmployeeId=@EmployeeId, Name=@Name, ManagerId=@ManagerId " + 
                        "where EmployeeId=@id";
            sqlCmd.Connection = myConnection;

            sqlCmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeID);  
            sqlCmd.Parameters.AddWithValue("@Name", employee.Name);  
            sqlCmd.Parameters.AddWithValue("@ManagerId", employee.ManagerID);
            sqlCmd.Parameters.AddWithValue("@id", id);

            myConnection.Open();
            sqlCmd.ExecuteNonQuery(); //Update a row 
            myConnection.Close();  
            return "El id es: "+id;
        }
    }
}