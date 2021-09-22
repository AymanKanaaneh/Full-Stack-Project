using Microsoft.AspNetCore.Mvc;
using server_api_demos.Models;
using server_api_demos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_api_demos.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentsRepository studentRepository = null;
        /// <summary>
        /// Each Request is initializing new Controller 
        /// instance ---> each Request will pass through the constractor
        /// 02 Inject Service into Controller
        /// </summary>


        //01 Constractor
        //02 Dependency Injection of SqlStudentRepository SqlStudentRepository constractor initialized
        public StudentsController(IStudentsRepository studentRepository)
        {

            //03 Save REpository in Controller Scope(private Member) 
            //Interface  = new Instance that Implement that interface
            this.studentRepository = studentRepository;
        }

        // GET: api/<StudentController>
        //04 Get Request
        /*[HttpGet()]
        public IEnumerable<StudentModel> Get()
        {
            //05 Call Service
            var students = studentRepository.GetAllStudents();

            //07 CONVERTED TO JSON AND REURN to Client
            return students;
        }*/

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            StudentModel studentFound = studentRepository.GetStudentById(id);
            if (studentFound != null)
                return Ok(studentFound);
            else
                return NotFound(id);

        }

        [HttpGet("GetByStudentUserName/{userName}")]
        public StudentModel GetByTeacherUserName(string userName)
        {
            StudentModel studentFound = studentRepository.GetStudentByUserName(userName);
            return studentFound;
        }


        // GET api/<StudentController>/5
        /*[HttpGet("GetByTeacher/{id}")]
        public IActionResult GetByTeacherId(int id)
        {
            return new EmptyResult();
        }*/

        //201
        // POST api/<StudentController>
        /*[HttpPost]
        public IActionResult Post(StudentModel newStudent)
        {

            int createdId = studentRepository.AddStudent(newStudent);
            newStudent.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newStudent);


        }*/

        // PUT api/<StudentController>/5
        /*[HttpPut("{id}")]
        public IActionResult Put(int id,StudentModel newStudent)
        {
            bool isUpdated = studentRepository.UpdateStudent(newStudent);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Student-" + id + " Not Found");
        }*/

        // DELETE api/<StudentController>/5
        /*[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = studentRepository.DeleteStudent(id);
            if (isDeleted)
                return Ok("Student [" + id + "] Deleted");//200
            else

                return NotFound("Student-" + id + " Not Found");//404
        }*/
    }
}
