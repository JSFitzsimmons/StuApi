using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StuApi.Models
{
    public class StuContext : IStuContext
    {
        string dbPath = "databasefile.csv";
        List<Student> database;

        public StuContext()
        {
            database = new List<Student>();
            this.loadFile();
        }

        public List<Student> getStudents()
        {
            return database;
        }

        public void postStudent(Student student)
        {
            this.database.Add(student);
            this.saveChanges();
        }

        public Student GetStudentById(int id)
        {
            return database.Find((Student stu)=> stu.StudentId == (id.ToString()));
        }

        public void loadFile()
        {
            StreamReader sr = new StreamReader(dbPath);
            string cl;
            while((cl = sr.ReadLine()) != null)
            {
                string[] student = cl.Split(',');
                this.database.Add(new Student { StudentId = student[0], StudentName = student[1], StudentGpa = float.Parse(student[2])});
            }
            sr.Close();
        }

        public void deleteStudent(Student stu)
        {
            database.Remove(GetStudentById(int.Parse(stu.StudentId)));
        }

        public void UpdateStudent(Student stu)
        {
            //remove previous version 
            deleteStudent(stu);
            database.Add(stu);
        }

        public void saveChanges()
        {
            Student[] students = database.ToArray();
            string[] Cout = Array.ConvertAll(students, stu => stu.toString());
            File.WriteAllLines(dbPath, Cout);
        }
    }
}

