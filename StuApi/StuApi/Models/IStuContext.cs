using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuApi.Models
{
    public interface IStuContext
    {
        List<Student> getStudents();

        void postStudent(Student student);

        Student GetStudentById(int id);

        void loadFile();

        void deleteStudent(Student stu);

        void UpdateStudent(Student stu);

        void saveChanges();
    }
}
