using server_api_demos.Models;
using System.Collections.Generic;

namespace server_api_demos.Services
{
    public interface IExamsRepository
    {
        int AddExam(ExamModel newExam);
        bool DeleteExam(int id);

        bool UpdateExam(ExamModel newExam);
        List<ExamModel> GetAllExams();
        List<ExamModel> GetAllExamsByTeacherId(int teacherId);
        List<ExamModel> GetAllExamsByTitle(string subTitle);
        ExamModel GetExamById(int id);
        ExamModel GetExamByIdWithQuestions(int id);
    }
}