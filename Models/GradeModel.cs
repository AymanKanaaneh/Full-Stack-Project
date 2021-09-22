using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_api_demos.Models
{
    public class GradeModel
    {
        public GradeModel()
        {

        }

        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ExamId { get; set; }

        public int Score { get; set; }

    }
}
