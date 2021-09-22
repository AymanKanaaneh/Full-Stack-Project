using Microsoft.AspNetCore.Mvc;
using server_api_demos.Models;
using server_api_demos.Models.Api;
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
    public class QuestionsController : ControllerBase
    {

        IQuestionsRepository questionRepository;

        /// <summary>
        /// Depency 
        /// </summary>
        /// <param name="questionRepository"></param>
        public QuestionsController(IQuestionsRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        // GET: api/<QuestionController>
        [HttpGet("ByExam/{examId}")]
        public IEnumerable<ApiResponseQuestionModel> ByExam(int examId)
        {
            List<QuestionModel> questionsInExam = questionRepository.GetAllQuestinByExamId(examId);

            //Convert Listy Of DBMODEL To LIST OF APIMODEL
            var apiList = questionsInExam.Select(db =>
             new ApiResponseQuestionModel()
             {
                 Id = db.Id,
                 ChoicesList = db.Choices.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                 Correct = db.Correct,
                 ExamId = db.ExamId,
                 Points = db.Points,
                 Question = db.Question
             });

            return apiList.ToList();
        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public QuestionModel Get(int id)
        {
            QuestionModel questionFound = questionRepository.GetQuestionById(id);
            return questionFound;
        }

        // POST api/<QuestionController>
        [HttpPost]
        public IActionResult Post(QuestionModel newQuestion)
        {
            int createdId = questionRepository.AddQuestion(newQuestion);
            newQuestion.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newQuestion);
        }

        // PUT api/<QuestionController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] QuestionModel newQuestion)
        {
            bool isUpdated = questionRepository.UpdateQuestion(newQuestion);
            if 
                (isUpdated) return Ok(id);
            else
                return NotFound("Question-" + id + " Not Found");
        }

        // DELETE api/<QuestionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = questionRepository.DeleteQuestion(id);
            if (isDeleted)
                return Ok("Question [" + id + "] Deleted");//200
            else
                return NotFound("Question-" + id + " Not Found");//404
        }
    }
}
