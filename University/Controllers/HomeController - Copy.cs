using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Subject> subjects = new List<Subject>();
            using (UniversityContext db = new UniversityContext())
            {
                subjects = db.Subjects.ToList();
                //db.Entry(m).Collection(c => c.colOfTimeValuesToDownloadTestPage).Load();
            }

            ViewBag.Subjects = subjects;

            return View();
        }

        public List<Subject> GetSubjects(int studentId)
        {
            List<Subject> colOfSubjects = new List<Subject>();
            using (UniversityContext db = new UniversityContext())
            {
                Student student = db.Students.Find(studentId);
                db.Entry(student).Collection(c => c.Subjects).Load();
                colOfSubjects.AddRange(student.Subjects.ToList());
            }

            return colOfSubjects;
        }
        public List<Student> GetStudents(int lecturerId)
        {
            List<Student> colOfStudents = new List<Student>();
            using (UniversityContext db = new UniversityContext())
            {
                Lecturer lecturer = db.Lecturers.Find(lecturerId);
                db.Entry(lecturer).Collection(c => c.Students).Load();
                colOfStudents.AddRange(lecturer.Students.ToList());
            }

            return colOfStudents;
        }

        public ActionResult ShowSubjectsOfStudent(int studentId)
        {
            //List<Subject> colOfSubjects = new List<Subject>();
            //using (UniversityContext db = new UniversityContext())
            //{
            //    Student student = db.Students.Find(studentId);
            //    db.Entry(student).Collection(c => c.Subjects).Load();
            //    colOfSubjects.AddRange(student.Subjects.ToList());
            //}

            ViewBag.studentId = studentId;
            //ViewBag.colOfStudents = colOfStudents;
            //return colOfStudents;
            return PartialView(/*colOfSubjects*/);
        }
        public ActionResult ShowStudentsOfLecturer(int lecturerId)
        {
            List<Student> colOfStudents = new List<Student>();
            using (UniversityContext db = new UniversityContext())
            {
                Lecturer lecturer = db.Lecturers.Find(lecturerId);
                db.Entry(lecturer).Collection(c => c.Students).Load();
                colOfStudents.AddRange(lecturer.Students.ToList());
            }

            //ViewBag.colOfStudents = colOfStudents;
            //return colOfStudents;
            return PartialView(colOfStudents);
        }
        public ActionResult ShowStudentsOfSubject(string subjectName)
        {
            //List<Student> colOfStudents = new List<Student>();
            //using (UniversityContext db = new UniversityContext())
            //{
            //    List<Subject> subjects = db.Subjects.Where(s=>s.Name == subjectName).ToList();

            //    foreach (Subject subject in subjects)
            //    {
            //        db.Entry(subject).Collection(c => c.Students).Load();
            //        colOfStudents.AddRange(subject.Students.ToList());

            //    }

            //}

            ViewBag.subjectName = subjectName;

            //return colOfStudents;
            //return PartialView(colOfStudents);
            return PartialView();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}