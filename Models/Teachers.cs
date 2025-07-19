﻿namespace School.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }
}