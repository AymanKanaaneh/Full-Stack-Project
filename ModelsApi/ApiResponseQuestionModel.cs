using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_api_demos.Models.Api
{
    public class ApiResponseQuestionModel
    {
        public ApiResponseQuestionModel()
        {
        }
        
        public int Id { get; set; }
        public string Question { get; set; }
        public string Correct{ get; set; }
        public int Points {get; set;}
        public int ExamId { get; set; }
        public List<string> ChoicesList { get; set; }


    }
}
