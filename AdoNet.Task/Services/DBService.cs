
using Microsoft.Data.SqlClient;
using System;
using AdoNet.Task.Interfaces;
using System.Threading.Channels;
using AdoNet.Task.Models;

namespace AdoNet.Task.Services 
{
    public class DBService : IService
    {
        List<Students>students = new List<Students>();
        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Students;TrustServerCertificate=True;MultipleActiveResultSets=True;";
        
        public SqlConnection ConnectService()
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        public void AddStudents()
        {
            Console.WriteLine("Enter student name!");
            string name = Console.ReadLine().ToString();
            Console.WriteLine("Enter student age!");
            int age = Convert.ToInt32(Console.ReadLine());
            using var connect = ConnectService();
            string query = $"INSERT INTO Students(Name,Age) VALUES(@Name,@Age)";
            using SqlCommand command = new(query, connect);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("Age", age);
            int affected = command.ExecuteNonQuery();
            if (affected > 0) Console.WriteLine("Student added succesfully");
            else Console.WriteLine("Error!");
        }
        public void GetStudents()
        {
            Students students = new Students();
            using var connect = ConnectService();
            string query = "SELECT Id,Name,Age FROM Students";
            using SqlCommand command = new(query, connect);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                students.Id = reader.GetInt32(0);
                students.Name = reader.GetString(1);
                students.Age = reader.GetInt32(2);
                Console.WriteLine($"Id: {students.Id}, Name: {students.Name}, Age: {students.Age}");
            }
            
        }
        public void UpdateStudents()
        {
            Console.WriteLine("Enter student id which you want update ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new name");
            string name = Console.ReadLine().ToString();
            Console.WriteLine("Enter new age");
            int age = Convert.ToInt32(Console.ReadLine());
            using var connect = ConnectService();
            string query = "UPDATE Students SET Name = @Name, Age = @Age WHERE Id = @Id";
            using SqlCommand command = new(query, connect);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Age", age);
            command.Parameters.AddWithValue("@Id", id);
            var affected = command.ExecuteNonQuery();
            if(affected > 0) Console.WriteLine("Student updated");
            else Console.WriteLine("Error!");
        }
        public void SearchStudents()
        {
            Console.WriteLine("Enter student data");
            string data = Console.ReadLine();
            using var connect = ConnectService();
            using SqlCommand command = new();
            command.Connection = connect;
            if (int.TryParse(data, out int id))
            {
                string idQuery = "SELECT Id,Name,Age FROM Students WHERE Id = @Id";
                command.CommandText = idQuery;
                command.Parameters.AddWithValue("@Id", id);
            }
            else 
            {
                string nameQuery = "SELECT Id,Name,Age FROM Students WHERE Name LIKE '%' + @Name + '%'";
                command.CommandText = nameQuery;
                command.Parameters.AddWithValue("@Name", data.ToString());
            }
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Id: {reader.GetInt32(0)}, Name: {reader.GetString(1)}, Age: {reader.GetInt32(2)}");
                }
            }
            else Console.WriteLine("Student not found!");
        }
        public void DeleteStudents()
        {
            int id = Convert.ToInt32(Console.ReadLine());
            using var conn = ConnectService();
            string query = "DELETE FROM Students WHERE Id = @id";
            using SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("Id", id);
            int affected = command.ExecuteNonQuery();
            if (affected > 0) Console.WriteLine("Student deleted");
            else Console.WriteLine("Error!");
        }
        
        
    }
}
