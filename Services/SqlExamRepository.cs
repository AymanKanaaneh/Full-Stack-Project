using server_api_demos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace server_api_demos.Services
{

    /// <summary>
    /// Access SQL And return C# Objects
    /// </summary>
    public class SqlExamRepository : IExamsRepository
    {

        /// <summary>
        /// Property Connection String (WHERE THE FILE /URL FOR SQL)
        /// </summary>
        private string ConnectionString { get; set; }


        /// <summary>
        /// Empty
        /// </summary>
        public SqlExamRepository()
        {
            this.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exmas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
                                    //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exmas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

        //ConnectionString
        //public SqlExamRepository(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}


        /// <summary>
        /// Return List Of Objects 
        /// By rows in table
        /// EACH DB ROW --> C# Model Object
        /// </summary>
        /// <returns></returns>
        ///06 Goto DataBase REturn as OBJECT 

        public List<ExamModel> GetAllExams()
        {
            List<ExamModel> examsList = new List<ExamModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Exams", connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        ExamModel examModel = new ExamModel();
                        examModel.Id = reader.GetInt32(0);
                        examModel.Title = reader.GetString(1);
                        examModel.DateStarted = reader.GetDateTime(2);
                        examModel.DurationMinutes = reader.GetInt32(3);
                        examModel.TeachrId = reader.GetInt32(4);
                        examsList.Add(examModel);
                    }
                }
            }

            return examsList;
        }

        public ExamModel GetExamById(int id)
        {
            ExamModel examModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Exams WHERE Id =" + id.ToString(), connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        examModel = new ExamModel();
                        examModel.Id = reader.GetInt32(0);
                        examModel.Title = reader.GetString(1);
                        examModel.DateStarted = reader.GetDateTime(2);
                        examModel.DurationMinutes = reader.GetInt32(3);
                        examModel.TeachrId = reader.GetInt32(4);

                    }
                }
            }

            return examModel;
        }

        public List<ExamModel> GetAllExamsByTeacherId(int teacherId)
        {
            List<ExamModel> examsList = new List<ExamModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Exams WHERE TeacherId ="+ teacherId.ToString(), connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        ExamModel examModel = new ExamModel();
                        examModel.Id = reader.GetInt32(0);
                        examModel.Title = reader.GetString(1);
                        examModel.DateStarted = reader.GetDateTime(2);
                        examModel.DurationMinutes = reader.GetInt32(3);
                        examModel.TeachrId = reader.GetInt32(4);
                        examsList.Add(examModel);
                    }
                }
            }

            return examsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newExam"></param>
        /// <returns></returns>
        public int AddExam(ExamModel newExam)
        {
            ExamModel examModel = null;
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

                    string addExam = "INSERT INTO Exams (Title, DateStarted, DurationMinutes ,TeacherId)" +
                                     " VALUES (@Title,@DateStarted,@DurationMinutes,@TeacherId); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCopmmand = new SqlCommand(addExam, connection);
                    addCopmmand.Parameters.AddWithValue("@Title", newExam.Title);
                    addCopmmand.Parameters.AddWithValue("@DateStarted", newExam.DateStarted);
                    addCopmmand.Parameters.AddWithValue("@DurationMinutes", newExam.DurationMinutes);
                    addCopmmand.Parameters.AddWithValue("@TeacherId", newExam.TeachrId);


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
        public bool DeleteExam(int id)
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
                    string deleteQuery = "DELETE FROM Exams WHERE Id = @Id";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="examToUpdate"></param>
        /// <returns></returns>
        public bool UpdateExam(ExamModel examToUpdate)
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

                    string updatedQuery = "UPDATE Exams SET " +
                                          "Title = @Title, " +
                                          "DateStarted = @DateStarted, " +
                                          "DurationMinutes = @DurationMinutes," +
                                          "TeacherId = @TeacherId " +
                                          "WHERE Id = @Id";

                    SqlCommand updateCommand = new SqlCommand(updatedQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", examToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@Title", examToUpdate.Title);
                    updateCommand.Parameters.AddWithValue("@DateStarted", examToUpdate.DateStarted);
                    updateCommand.Parameters.AddWithValue("@DurationMinutes", examToUpdate.DurationMinutes);
                    updateCommand.Parameters.AddWithValue("@TeacherId", examToUpdate.TeachrId);

                    int roesAffected = updateCommand.ExecuteNonQuery();
                    if (roesAffected > 0)
                        isUpdated = true;

                }

                return isUpdated;
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





        public List<ExamModel> GetAllExamsByTitle(string subTitle)
        {
            throw new NotImplementedException();
        }

        public ExamModel GetExamByIdWithQuestions(int id)
        {
            throw new NotImplementedException();
        }

    }
}
