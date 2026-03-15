
using Microsoft.Data.SqlClient;

namespace AdoNet.Task.Interfaces
{
    public interface IService
    {
        public SqlConnection ConnectService();
        public void AddStudents() { }
        public void GetStudents() { }
        public void UpdateStudents() { }
        public void SearchStudents() { }
        public void DeleteStudents() { }
        public void PaginateStudents() { }
    }
}
