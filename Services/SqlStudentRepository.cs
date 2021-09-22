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
    public class SqlStudentRepository : IStudentsRepository
    {

        /// <summary>
        /// Property Connection String (WHERE THE FILE /URL FOR SQL)
        /// </summary>
        private string ConnectionString { get; set; }


        /// <summary>
        /// Empty
        /// </summary>
        public SqlStudentRepository()
        {
            this.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        //ConnectionString
        //public SqlStudentRepository(string connectionString)
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

        /*public List<StudentModel> GetAllStudents()
        {
            List<StudentModel> studentsList = new List<StudentModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Students", connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.Id = reader.GetInt32(0);
                        studentModel.Title = reader.GetString(1);
                        studentModel.DateStarted = reader.GetDateTime(2);
                        studentModel.DurationMinutes = reader.GetInt32(3);
                        studentModel.TeachrId = reader.GetInt32(4);
                        studentsList.Add(studentModel);
                    }
                }
            }

            return studentsList;
        }*/

        public StudentModel GetStudentById(int id)
        {
            StudentModel studentModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Students WHERE Id =" + id.ToString(), connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        studentModel = new StudentModel();
                        studentModel.Id = reader.GetInt32(0);
                        studentModel.ExamsId = reader.GetString(1);
                        studentModel.Name = reader.GetString(2);
                        studentModel.Password = reader.GetString(3);

                    }
                }
            }

            return studentModel;
        }

        public StudentModel GetStudentByUserName(string userName)
        {
            StudentModel studentModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                // SqlCommand allCommand = new SqlCommand("SELECT * FROM Students WHERE UserName = 123456", connection);
                string selecteQuery = "SELECT * FROM Students WHERE UserName = @UserName";
                SqlCommand allCommand = new SqlCommand(selecteQuery, connection);
                allCommand.Parameters.AddWithValue("@UserName", userName);



                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        studentModel = new StudentModel();
                        studentModel.Id = reader.GetInt32(0);
                        studentModel.ExamsId = reader.GetString(1);
                        studentModel.Name = reader.GetString(2);
                        studentModel.Password = reader.GetString(3);
                    }
                }
            }

            return studentModel;
        }

        /*public List<StudentModel> GetAllStudentsByTeacherId(int teacherId)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newStudent"></param>
        /// <returns></returns>
        public int AddStudent(StudentModel newStudent)
        {
            StudentModel studentModel = null;
            SqlConnection connection = null;
            int newId = -1;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();

                    //string timeText =  newStudent.DateStarted.ToString("yyyy-MM-dd HH:mm:ss");
                    // //Command (SQL QUEERY)
                    // string addStudent = "INSERT INTO Students (Title, DateStarted, DurationMinutes ,TeacherId)" +
                    //                  $" VALUES ('{newStudent.Title}','{timeText}',{newStudent.DurationMinutes},{newStudent.TeachrId})";

                    string addStudent = "INSERT INTO Students (Title, DateStarted, DurationMinutes ,TeacherId)" +
                                     " VALUES (@Title,@DateStarted,@DurationMinutes,@TeacherId); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCopmmand = new SqlCommand(addStudent, connection);
                    addCopmmand.Parameters.AddWithValue("@Title", newStudent.Title);
                    addCopmmand.Parameters.AddWithValue("@DateStarted", newStudent.DateStarted);
                    addCopmmand.Parameters.AddWithValue("@DurationMinutes", newStudent.DurationMinutes);
                    addCopmmand.Parameters.AddWithValue("@TeacherId", newStudent.TeachrId);


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
        public bool DeleteStudent(int id)
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
                    string deleteQuery = "DELETE FROM Students WHERE Id = @Id";
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
        /// <param name="studentToUpdate"></param>
        /// <returns></returns>
        public bool UpdateStudent(StudentModel studentToUpdate)
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

                    string updatedQuery = "UPDATE Students SET " +
                                          "Title = @Title, " +
                                          "DateStarted = @DateStarted, " +
                                          "DurationMinutes = @DurationMinutes," +
                                          "TeacherId = @TeacherId " +
                                          "WHERE Id = @Id";

                    SqlCommand updateCommand = new SqlCommand(updatedQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", studentToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@Title", studentToUpdate.Title);
                    updateCommand.Parameters.AddWithValue("@DateStarted", studentToUpdate.DateStarted);
                    updateCommand.Parameters.AddWithValue("@DurationMinutes", studentToUpdate.DurationMinutes);
                    updateCommand.Parameters.AddWithValue("@TeacherId", studentToUpdate.TeachrId);

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





        public List<StudentModel> GetAllStudentsByTitle(string subTitle)
        {
            throw new NotImplementedException();
        }

        public StudentModel GetStudentByIdWithQuestions(int id)
        {
            throw new NotImplementedException();
        }*/

    }
}
