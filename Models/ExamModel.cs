using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_api_demos.Models
{
    public class ExamModel
    {
        public ExamModel()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int TeachrId { get; set; }
        public DateTime DateStarted { get; set; }
        public int DurationMinutes { get; set; }

    }
}
