using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentLabManager.Models
{
    public class TestSchedule
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Group { get; set; }

        public DateTime TestTime { get; set; }

        public string location { get; set; }

        public int MaxNoStudents { get; set; }

        public DateTime DueDate { get; set; }

    }
}
