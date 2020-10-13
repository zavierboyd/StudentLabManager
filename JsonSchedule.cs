using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StudentLabManager
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class TimeSlot
    {
        [JsonProperty("startTime")]
        public string StartTime;

        [JsonProperty("studentNo")]
        public int StudentNo;

        [JsonProperty("place")]
        public string Place;
    }

    public class Day
    {
        [JsonProperty("date")]
        public string Date;

        [JsonProperty("timeSlots")]
        public List<TimeSlot> TimeSlots;
    }

    public class ExamDays
    {
        [JsonProperty("MyArray")]
        public List<Day> Days;
    }

}
