using AdoNet.Task.Services;

namespace AdoNet.Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            DBService dbService = new DBService();
            //dbService.AddStudents();
            //dbService.DeleteStudents();
            //dbService.GetStudents();
            //dbService.UpdateStudents();
            dbService.SearchStudents();
        }
    }
}
