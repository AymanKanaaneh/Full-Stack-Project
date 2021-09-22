using server_api_demos.Models;
using System.Collections.Generic;

namespace server_api_demos.Services
{
    public interface IGradesRepository
    {
        int AddGrade(GradeModel newGrade);

        //bool DeleteGrade(int id);

        //bool UpdateGrade(GradeModel newGrade);
        List<GradeModel> GetAllGrades();
        GradeModel GetGradeByStudentExam(int studentId,int examId);
        List<GradeModel> GetGradeByExam(int examId);

        // List<GradeModel> GetAllGradesByTeacherId(int teacherId);
        // GradeModel GetGradeById(int id);

    }
}