using server_api_demos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace server_api_demos.Services
{

    /// <summary>
    /// Access SQL And return C# Objects
    /// </summary>
    public class SqlQuestionsRepository : IQuestionsRepository
    {

        /// <summary>
        /// Property Connection String (WHERE THE FILE /URL FOR SQL)
        /// </summary>
        private string ConnectionString { get; set; }

        /// <summary>
        /// Empty
        /// </summary>
        public SqlQuestionsRepository()
        {
            this.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuestionsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public int AddQuestion(QuestionModel newQuestion)
        {
            QuestionModel QuestionModel = null;
            SqlConnection connection = null;
            int newId = -1;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();

                    //string timeText =  newExam.DateStarted.ToString("yyyy-MM-dd HH:mm:ss");
                    // //Command (SQL QUEERY)
                    // string addExam = "INSERT INTO Exams (Title, DateStarted, DurationMinutes ,TeacherId)" +
                    //                  $" VALUES ('{newExam.Title}','{timeText}',{newExam.DurationMinutes},{newExam.TeachrId})";

                    string addQuestion = "INSERT INTO Questions (QuestionText, Choices, Examid ,CorrectAnswer, Points)" +
                                     " VALUES (@QuestionText,@Choices,@Examid,@CorrectAnswer,@Points); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCopmmand = new SqlCommand(addQuestion, connection);
                    addCopmmand.Parameters.AddWithValue("@QuestionText", newQuestion.Question);
                    addCopmmand.Parameters.AddWithValue("@Choices", newQuestion.Choices);
                    addCopmmand.Parameters.AddWithValue("@Examid", newQuestion.ExamId);
                    addCopmmand.Parameters.AddWithValue("@CorrectAnswer", newQuestion.Correct);
                    addCopmmand.Parameters.AddWithValue("@Points", newQuestion.Points);

                    newId = Convert.ToInt32(addCopmmand.ExecuteScalar());


                }

                return newId;

            }
            catch (Exception ex)
            {
                //Page.Response.Write("<script>console.log('" + ex.ToString() + "');</script>");
                Debug.WriteLine("********************************************");
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("********************************************");
                //Console.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public List<QuestionModel> GetAllQuestinByExamId(int examId)
        {
            List<QuestionModel> questionsInExam = new List<QuestionModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Questions WHERE Examid = @examId", connection);
                allCommand.Parameters.AddWithValue("@examId", examId);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {
                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        QuestionModel questionModel = new QuestionModel();
                        questionModel.Id = reader.GetInt32(0);
                        questionModel.Question = reader.GetString(1);
                        questionModel.Choices = reader.GetString(2);
                        questionModel.ExamId = reader.GetInt32(3);
                        questionModel.Correct = reader.GetString(4);
                        questionModel.Points = reader.GetInt32(5);
                        questionsInExam.Add(questionModel);
                    }
                }
            }
            return questionsInExam;
        }

        public List<QuestionModel> GetAllActiveQuestinByExamId(int examId)
        {
            //Do we need this?
            throw new NotImplementedException();
        }

        public QuestionModel GetQuestionById(int QuestionId)
        {
            //throw new NotImplementedException();
            QuestionModel questionModel = new QuestionModel();
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand getAllCommand = new SqlCommand("SELECT * FROM Questions Where Id =" + QuestionId, connection);

                    using (var reader = getAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            questionModel.Id = reader.GetInt32(0);
                            questionModel.Question = reader.GetString(1);
                            questionModel.Choices = reader.GetString(2);
                            questionModel.ExamId = reader.GetInt32(3);
                            questionModel.Correct = reader.GetString(4);
                            questionModel.Points = reader.GetInt32(5);
                        }

                    }


                }

            }
            catch (Exception)
            {

                throw;
            }
            return questionModel;
        }

       

        public bool DeleteQuestion(int id)
        {
            SqlConnection connection = null;
            bool isDeleted = false;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();
                    string deleteQuery = "DELETE FROM Questions WHERE Id = @Id";
                    SqlCommand DeleteCommand = new SqlCommand(deleteQuery, connection);
                    DeleteCommand.Parameters.AddWithValue("@Id", id);

                    int roesAffected = DeleteCommand.ExecuteNonQuery();
                    if (roesAffected > 0)
                        isDeleted = true;
                }

                return isDeleted;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public bool UpdateQuestion(QuestionModel QuestionToUpdate)
        {
            SqlConnection connection = null;
            bool isUpdated = false;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();

                    string updatedQuery = "UPDATE Questions SET " +
                                          "QuestionText = @QuestionText, " +
                                          "Choices = @Choices, " +
                                          "Examid = @Examid, " +
                                          "CorrectAnswer = @CorrectAnswer, " +
                                          "Points = @Points " +
                                          "WHERE Id = @Id";


                    SqlCommand updateCommand = new SqlCommand(updatedQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", QuestionToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@QuestionText", QuestionToUpdate.Question);
                    updateCommand.Parameters.AddWithValue("@Choices", QuestionToUpdate.Choices);
                    updateCommand.Parameters.AddWithValue("@Examid", QuestionToUpdate.ExamId);
                    updateCommand.Parameters.AddWithValue("@CorrectAnswer", QuestionToUpdate.Correct);
                    updateCommand.Parameters.AddWithValue("@Points", QuestionToUpdate.Points);

                    int roesAffected = updateCommand.ExecuteNonQuery();
                    if (roesAffected > 0)
                        isUpdated = true;

                }

                return isUpdated;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("********************************************");
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("********************************************");
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
    }
}
