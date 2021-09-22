using server_api_demos.Models;
using System.Collections.Generic;

namespace server_api_demos.Services
{
    public interface IStudentsRepository
    {
        //int AddStudent(StudentModel newStudent);
        //bool DeleteStudent(int id);

        //bool UpdateStudent(StudentModel newStudent);
        //List<StudentModel> GetAllStudents();
       // List<StudentModel> GetAllStudentsByTeacherId(int teacherId);
        StudentModel GetStudentById(int id);

        StudentModel GetStudentByUserName(string userName);
    }
}