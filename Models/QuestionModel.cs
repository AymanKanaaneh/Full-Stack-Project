using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_api_demos.Models
{
    public class QuestionModel
    {
        public QuestionModel()
        {
        }

        public int Id { get; set; }
        public string Question { get; set; }
        /// <summary>
        /// Seperator ;
        /// </summary>
        /// <example>
        /// "1;10;3;4;10"
        /// </example>
        public string Choices { get; set; }

        public string Correct { get; set; }
        public int Points { get; set; }
        public int ExamId { get; set; }



    }
}
