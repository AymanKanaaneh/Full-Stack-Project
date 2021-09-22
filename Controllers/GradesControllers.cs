using Microsoft.AspNetCore.Mvc;
using server_api_demos.Models;
using server_api_demos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_api_demos.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private IGradesRepository gradeRepository = null;
        /// <summary>
        /// Each Request is initializing new Controller 
        /// instance ---> each Request will pass through the constractor
        /// 02 Inject Service into Controller
        /// </summary>


        //01 Constractor
        //02 Dependency Injection of SqlGradeRepository SqlGradeRepository constractor initialized
        public GradesController(IGradesRepository gradeRepository)
        {

            //03 Save REpository in Controller Scope(private Member) 
            //Interface  = new Instance that Implement that interface
            this.gradeRepository = gradeRepository;
        }

        // GET: api/<GradeController>
        //04 Get Request
        [HttpGet()]
        public IEnumerable<GradeModel> Get()
        {
            //05 Call Service
            var grades = gradeRepository.GetAllGrades();

            //07 CONVERTED TO JSON AND REURN to Client
            return grades;
        }

        [HttpPost]
        public IActionResult Post(GradeModel newGrade)
        {

            int createdId = gradeRepository.AddGrade(newGrade);
            newGrade.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newGrade);


        }

        [HttpGet("{studentId}/{examId}")]
        public IActionResult Get(int studentId,int examId)
        {
            GradeModel gradeFound = gradeRepository.GetGradeByStudentExam(studentId, examId);
            if (gradeFound != null)
                return Ok(gradeFound);
            else
                return NotFound(examId);


        }



        [HttpGet("GetByExamId/{examId}")]
        public IEnumerable<GradeModel> GetByExamId(int examId)
        {

            
            var grades = gradeRepository.GetGradeByExam(examId);

            return grades;

        }

        // GET api/<GradeController>/5
        /* [HttpGet("{id}")]
         public IActionResult Get(int id)
         {
             GradeModel gradeFound = gradeRepository.GetGradeById(id);
             if (gradeFound != null)
                 return Ok(gradeFound);
             else
                 return NotFound(id);

         }*/

        /* [HttpGet("GetByGradeUserName/{userName}")]
         public GradeModel GetByTeacherUserName(string userName)
         {
             GradeModel gradeFound = gradeRepository.GetGradeByUserName(userName);
             return gradeFound;
         }
        */

        // GET api/<GradeController>/5
        /*[HttpGet("GetByTeacher/{id}")]
        public IActionResult GetByTeacherId(int id)
        {
            return new EmptyResult();
        }*/

        //201
        // POST api/<GradeController>
        /*[HttpPost]
        public IActionResult Post(GradeModel newGrade)
        {

            int createdId = gradeRepository.AddGrade(newGrade);
            newGrade.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newGrade);


        }*/

        // PUT api/<GradeController>/5
        /*[HttpPut("{id}")]
        public IActionResult Put(int id,GradeModel newGrade)
        {
            bool isUpdated = gradeRepository.UpdateGrade(newGrade);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Grade-" + id + " Not Found");
        }*/

        // DELETE api/<GradeController>/5
        /*[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = gradeRepository.DeleteGrade(id);
            if (isDeleted)
                return Ok("Grade [" + id + "] Deleted");//200
            else

                return NotFound("Grade-" + id + " Not Found");//404
        }*/
    }
}
