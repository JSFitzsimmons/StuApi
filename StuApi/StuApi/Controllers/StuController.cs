using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StuApi.Models;

namespace StuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StuController : ControllerBase
{
        private IStuContext _context;

        public StuController(IStuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult <List<Student>>GetAll()
        {
            return _context.getStudents();
        }

        [HttpGet]
        [Route("range")]
         public ActionResult<float[]> GetRange()
        {
            List<float> listOfGpas = new List<float>();
            float[] listOfGpasToReturn = new float[2];

            _context.getStudents().ForEach((student) => listOfGpas.Add(student.StudentGpa));

            listOfGpas.Sort();
            listOfGpasToReturn[0] = listOfGpas.First<float>();
            listOfGpasToReturn[1] = listOfGpas.Last<float>();

            return listOfGpasToReturn;
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public ActionResult<Student> GetById(int id)
        {
            var stu = _context.GetStudentById(id);
            if (stu == null)
            {
                return NotFound();
            }
            
            return stu;
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.postStudent(student);
            _context.saveChanges();

            return CreatedAtRoute("GetStudent", new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {

            var stu = _context.GetStudentById(id);
            if (stu == null)
            {
                return NotFound();
            }

            stu.StudentId = student.StudentId;
            stu.StudentGpa = student.StudentGpa;
            stu.StudentName = student.StudentName;

            _context.UpdateStudent(stu);
            _context.saveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var stu = _context.GetStudentById(id);
            if (stu == null)
            {
                return NotFound();
            }

            _context.deleteStudent(stu);
            _context.saveChanges();
            return NoContent();
        }

    }
}
