using server_api_demos.Models;
using System.Collections.Generic;

namespace server_api_demos.Services
{
    public interface ITeachersRepository
    {
        int AddTeacher(TeacherModel newTeacher);

        bool DeleteTeacher(int id);

        List<TeacherModel> GetAllTeachers();

        bool UpdateTeacher(TeacherModel newTeacher);
        //List<TeacherModel> GetAllTeacher();

        TeacherModel GetTeacherById(int id);
        TeacherModel GetTeacherByUserName(string userName);
        
    }
}