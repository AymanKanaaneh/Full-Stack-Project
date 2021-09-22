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
    public class TeachersController : ControllerBase
    {
        private ITeachersRepository teacherRepository = null;
        /// <summary>
        /// Each Request is initializing new Controller 
        /// instance ---> each Request will pass through the constractor
        /// 02 Inject Service into Controller
        /// </summary>

        public TeachersController(ITeachersRepository teacherRepository)
        {
            //Interface  = new Instance that Implement that interface
            this.teacherRepository = teacherRepository;
        }

        // GET: api/<TeacherController>
        [HttpGet()]
        public IEnumerable<TeacherModel> Get()
        {
            var teachers = teacherRepository.GetAllTeachers();
            return teachers;
        }

        // GET api/<TeacherController>/5
        [HttpGet("{id}")]
        public TeacherModel Get(int id)
        {
            TeacherModel teacherFound = teacherRepository.GetTeacherById(id);
            return teacherFound;
        }

       
        [HttpGet("GetByTeacherUserName/{userName}")]
        public TeacherModel GetByTeacherUserName(string userName)
        {
            TeacherModel teacherFound = teacherRepository.GetTeacherByUserName(userName);
            return teacherFound;
        }

        // POST api/<TeacherController>
        [HttpPost]
        public int Post(TeacherModel newTeacher)
        {
            int id = teacherRepository.AddTeacher(newTeacher);
            return id;
        }

        // PUT api/<TeacherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TeacherModel newTeacher)
        {
            teacherRepository.UpdateTeacher(newTeacher);
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            bool isDeleted = teacherRepository.DeleteTeacher(id);
            return isDeleted;

        }
    }
}
