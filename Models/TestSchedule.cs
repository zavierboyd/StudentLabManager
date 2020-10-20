using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentLabManager.Models
{
    public class TestSchedule
    {
        public int ID { get; set; }
        public string exam { get; set; }
        public string group { get; set; }
        public int duration { get; set; }
        public string schedule { get; set; } // Is a JSON string of schedule can be converted to C# objects through use of 
                                             // using Newtonsoft.Json;
                                             // ViewBag.Schedule = JsonConvert.DeserializeObject<ExamDays>("{MyArray:" + schedule + "}");
    }
}
