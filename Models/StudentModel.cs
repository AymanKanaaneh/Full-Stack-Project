using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_api_demos.Models
{
    public class StudentModel
    {
        public StudentModel()
        {

        }

        public int Id { get; set; }
        public string ExamsId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

    }
}
