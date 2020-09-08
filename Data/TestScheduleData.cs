using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentLabManager.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentLabManager.Data
{
    public class TestScheduleData : DbContext
    {
        public TestScheduleData(DbContextOptions<TestScheduleData> options) : base(options)
        {

        }


        public DbSet<TestSchedule> Schedule { get; set; }
    }
}

