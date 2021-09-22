using server_api_demos.Models;
using System.Collections.Generic;

namespace server_api_demos.Services
{
    public interface IQuestionsRepository
    {
        int AddQuestion(QuestionModel newExam);
        bool DeleteQuestion(int id);

        bool UpdateQuestion(QuestionModel newExam);
        List<QuestionModel> GetAllQuestinByExamId(int examId);

        List<QuestionModel> GetAllActiveQuestinByExamId(int examId);

        QuestionModel GetQuestionById(int id);

    }
}