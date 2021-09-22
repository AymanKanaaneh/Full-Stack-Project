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
    public class SqlGradeRepository : IGradesRepository
    {

        /// <summary>
        /// Property Connection String (WHERE THE FILE /URL FOR SQL)
        /// </summary>
        private string ConnectionString { get; set; }


        /// <summary>
        /// Empty
        /// </summary>
        public SqlGradeRepository()
        {
            this.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GradesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        //ConnectionString
        //public SqlGradeRepository(string connectionString)
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

        public List<GradeModel> GetAllGrades()
        {
            List<GradeModel> gradesList = new List<GradeModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Grades", connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        GradeModel gradeModel = new GradeModel();
                        gradeModel.Id = reader.GetInt32(0);
                        gradeModel.StudentId = reader.GetInt32(1);
                        gradeModel.ExamId = reader.GetInt32(2);
                        gradeModel.Score = reader.GetInt32(3);
                        gradesList.Add(gradeModel);
                    }
                }
            }

            return gradesList;
        }

        public GradeModel GetGradeByStudentExam(int studentId,int examId)
        {
            GradeModel gradeModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Grades WHERE StudentId =" + studentId.ToString()+" AND ExamId ="+examId.ToString() , connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        gradeModel = new GradeModel();
                        gradeModel.Id = reader.GetInt32(0);
                        gradeModel.StudentId = reader.GetInt32(1);
                        gradeModel.ExamId = reader.GetInt32(2);
                        gradeModel.Score = reader.GetInt32(3);

                    }
                }
            }

            return gradeModel;
        }

        public List<GradeModel> GetGradeByExam(int examId)
        {
            List<GradeModel> gradesList = new List<GradeModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Grades WHERE ExamId =" + examId.ToString(), connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        GradeModel gradeModel = new GradeModel();
                        gradeModel.Id = reader.GetInt32(0);
                        gradeModel.StudentId = reader.GetInt32(1);
                        gradeModel.ExamId = reader.GetInt32(2);
                        gradeModel.Score = reader.GetInt32(3);
                        gradesList.Add(gradeModel);
                    }
                }
            }

            return gradesList;
        }

        /*
        public GradeModel GetGradeByUserName(string userName)
        {
            GradeModel gradeModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                // SqlCommand allCommand = new SqlCommand("SELECT * FROM Grades WHERE UserName = 123456", connection);
                string selecteQuery = "SELECT * FROM Grades WHERE UserName = @UserName";
                SqlCommand allCommand = new SqlCommand(selecteQuery, connection);
                allCommand.Parameters.AddWithValue("@UserName", userName);



                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        gradeModel = new GradeModel();
                        gradeModel.Id = reader.GetInt32(0);
                        gradeModel.ExamsId = reader.GetString(1);
                        gradeModel.Name = reader.GetString(2);
                        gradeModel.Password = reader.GetString(3);
                    }
                }
            }

            return gradeModel;
        }*/

        /*public List<GradeModel> GetAllGradesByTeacherId(int teacherId)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGrade"></param>
        /// <returns></returns>
        /// 

        */
        public int AddGrade(GradeModel newGrade)
        {
            GradeModel gradeModel = null;
            SqlConnection connection = null;
            int newId = -1;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();

                    //string timeText =  newGrade.DateStarted.ToString("yyyy-MM-dd HH:mm:ss");
                    // //Command (SQL QUEERY)
                    // string addGrade = "INSERT INTO Grades (Title, DateStarted, DurationMinutes ,TeacherId)" +
                    //                  $" VALUES ('{newGrade.Title}','{timeText}',{newGrade.DurationMinutes},{newGrade.TeachrId})";

                    string addGrade = "INSERT INTO Grades (StudentId, ExamId, Score)" +
                                     " VALUES (@StudentId,@ExamId,@Score); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCopmmand = new SqlCommand(addGrade, connection);
                    addCopmmand.Parameters.AddWithValue("@StudentId", newGrade.StudentId);
                    addCopmmand.Parameters.AddWithValue("@ExamId", newGrade.ExamId);
                    addCopmmand.Parameters.AddWithValue("@Score", newGrade.Score);


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

        /*
        public bool DeleteGrade(int id)
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
                    string deleteQuery = "DELETE FROM Grades WHERE Id = @Id";
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
        /// <param name="gradeToUpdate"></param>
        /// <returns></returns>
        public bool UpdateGrade(GradeModel gradeToUpdate)
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

                    string updatedQuery = "UPDATE Grades SET " +
                                          "Title = @Title, " +
                                          "DateStarted = @DateStarted, " +
                                          "DurationMinutes = @DurationMinutes," +
                                          "TeacherId = @TeacherId " +
                                          "WHERE Id = @Id";

                    SqlCommand updateCommand = new SqlCommand(updatedQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", gradeToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@Title", gradeToUpdate.Title);
                    updateCommand.Parameters.AddWithValue("@DateStarted", gradeToUpdate.DateStarted);
                    updateCommand.Parameters.AddWithValue("@DurationMinutes", gradeToUpdate.DurationMinutes);
                    updateCommand.Parameters.AddWithValue("@TeacherId", gradeToUpdate.TeachrId);

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





        public List<GradeModel> GetAllGradesByTitle(string subTitle)
        {
            throw new NotImplementedException();
        }

        public GradeModel GetGradeByIdWithQuestions(int id)
        {
            throw new NotImplementedException();
        }*/

    }
}
