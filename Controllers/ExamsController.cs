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
    public class ExamsController : ControllerBase
    {
        private IExamsRepository examRepository = null;
        /// <summary>
        /// Each Request is initializing new Controller 
        /// instance ---> each Request will pass through the constractor
        /// 02 Inject Service into Controller
        /// </summary>


        //01 Constractor
        //02 Dependency Injection of SqlExamRepository SqlExamRepository constractor initialized
        public ExamsController(IExamsRepository examRepository)
        {

            //03 Save REpository in Controller Scope(private Member) 
            //Interface  = new Instance that Implement that interface
            this.examRepository = examRepository;
        }

        // GET: api/<ExamController>
        //04 Get Request
        [HttpGet()]
        public IEnumerable<ExamModel> Get()
        {
            //05 Call Service
            var exams = examRepository.GetAllExams();

            //07 CONVERTED TO JSON AND REURN to Client
            return exams;
        }

        // GET api/<ExamController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ExamModel examFound = examRepository.GetExamById(id);
            if (examFound != null)
                return Ok(examFound);
            else
                return NotFound(id);

        }

        // GET api/<ExamController>/5
        [HttpGet("GetByTeacher/{id}")]
        public IActionResult GetByTeacherId(int id)
        {
            var  exams = examRepository.GetAllExamsByTeacherId(id);
            if (exams != null)
                return Ok(exams);
            else
                return NotFound(id);
        }

        //201
        // POST api/<ExamController>
        [HttpPost]
        public IActionResult Post(ExamModel newExam)
        {

            int createdId = examRepository.AddExam(newExam);
            newExam.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newExam);


        }

        // PUT api/<ExamController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,ExamModel newExam)
        {
            bool isUpdated = examRepository.UpdateExam(newExam);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Exam-" + id + " Not Found");
        }

        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = examRepository.DeleteExam(id);
            if (isDeleted)
                return Ok("Exam [" + id + "] Deleted");//200
            else

                return NotFound("Exam-" + id + " Not Found");//404
        }
    }
}
