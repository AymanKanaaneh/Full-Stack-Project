using server_api_demos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace server_api_demos.Services
{
    public class SqlTeacherRepository : ITeachersRepository
    {

        /// <summary>
        /// Property Connection String (WHERE THE FILE /URL FOR SQL)
        /// </summary>
        private string ConnectionString { get; set; }


        /// <summary>
        /// Empty
        /// </summary>
        public SqlTeacherRepository()
        {
            this.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TeachersDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        //ConnectionString
        //public SqlTeacherRepository(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}

        public List<TeacherModel> GetAllTeachers()
        {
            List<TeacherModel> teachersList = new List<TeacherModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Teachers", connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        TeacherModel teacherModel = new TeacherModel();
                        teacherModel.Id = reader.GetInt32(0);
                        teacherModel.Name = reader.GetString(1);
                        teachersList.Add(teacherModel);
                    }
                }
            }

            return teachersList;
        }



        public TeacherModel GetTeacherById(int id)
        {
            TeacherModel teacherModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Teachers WHERE Id =" + id.ToString(), connection);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        teacherModel = new TeacherModel();
                        teacherModel.Id = reader.GetInt32(0);
                        teacherModel.Name = reader.GetString(1);
                    }
                }
            }

            return teacherModel;
        }


        public TeacherModel GetTeacherByUserName(string userName)
        {
            TeacherModel teacherModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
               // SqlCommand allCommand = new SqlCommand("SELECT * FROM Teachers WHERE UserName = 123456", connection);
                string selecteQuery = "SELECT * FROM Teachers WHERE UserName = @UserName";
                SqlCommand allCommand = new SqlCommand(selecteQuery, connection);
                allCommand.Parameters.AddWithValue("@UserName", userName);



                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        teacherModel = new TeacherModel();
                        teacherModel.Id = reader.GetInt32(0);
                        teacherModel.Name = reader.GetString(1);
                        teacherModel.Password = reader.GetString(2);
                    }
                }
            }

            return teacherModel;
        }

        public List<TeacherModel> GetAllTeachersByTeacherId(int teacherId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTeacher"></param>
        /// <returns></returns>
        public int AddTeacher(TeacherModel newTeacher)
        {

            int teacherId = newTeacher.Id;
            string name = newTeacher.Name;
            string pass = newTeacher.Password;
            try
            {
                var teacherFound = GetTeacherById(teacherId);
                if (teacherFound == null)

                {
                    using (var connection = new SqlConnection(this.ConnectionString))
                    {
                        connection.Open();

                        //SqlCommand insertCommand = new SqlCommand("INSERT INTO Teachers (FName,LName) " +
                        //    "VALUES ('" + Fname + "','" + Lname + "')", connection);
                        //insertCommand.ExecuteNonQuery();

                        SqlCommand insertCommand = new SqlCommand("INSERT INTO Teachers (Id,UserName,Password) " +
                            "VALUES (@Id,@UserName ,@Password); " +
                                         "SELECT SCOPE_IDENTITY()", connection);
                        insertCommand.Parameters.AddWithValue("@Id", newTeacher.Id);
                        insertCommand.Parameters.AddWithValue("@UserName", newTeacher.Name);
                        insertCommand.Parameters.AddWithValue("@Password", newTeacher.Password);
                        insertCommand.ExecuteNonQuery();
                        //teacherId = Convert.ToInt32(insertCommand.ExecuteScalar());


                    }
                }
                else
                    teacherId = -1;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return teacherId;
        }

        public bool DeleteTeacher(int id)
        {
            var isDeleted = false;

            try
            {
                var teacherFound = GetTeacherById(id);
                if (teacherFound != null)
                {
                    using (var connection = new SqlConnection(this.ConnectionString))
                    {
                        connection.Open();
                        //DELETE FROM Customers WHERE CustomerName='Alfreds Futterkiste'
                        SqlCommand deleteCommand = new SqlCommand("DELETE FROM Teachers Where Id =@Id", connection);
                        deleteCommand.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            isDeleted = true;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }



            return isDeleted;
        }



        public List<TeacherModel> GetAllTeachersByTitle(string subTitle)
        {
            throw new NotImplementedException();
        }



        public TeacherModel GetTeacherByIdWithQuestions(int id)
        {
            throw new NotImplementedException();
        }
        public bool UpdateTeacher(TeacherModel newTeacher)
        {
            int teacherId = newTeacher.Id;
            
            try
            {
                var teacherFound = GetTeacherById(teacherId);
                if (teacherFound != null)

                {
                    using (var connection = new SqlConnection(this.ConnectionString))
                    {
                        connection.Open();

                        SqlCommand updateCommand = new SqlCommand("UPDATE Teachers SET UserName=@UserName,Password=@Password WHERE Id =@Id", connection);
                        updateCommand.Parameters.AddWithValue("@UserName", newTeacher.Name);
                        updateCommand.Parameters.AddWithValue("@Password", newTeacher.Password);
                        updateCommand.Parameters.AddWithValue("@Id", newTeacher.Id);
                        updateCommand.ExecuteNonQuery();

                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }
    


    }
}
