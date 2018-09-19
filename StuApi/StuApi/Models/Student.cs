using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuApi.Models
{
    public class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public float StudentGpa { get; set; }

        public String toString()
        {
            return StudentId+","+StudentName+","+StudentGpa;
        }
    }
}
