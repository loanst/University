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

            }

            ViewBag.Subjects = subjects;

            return View();
        }
        public ActionResult ShowSubjectsOfStudent(int studentId)
        {
            ViewBag.studentId = studentId;

            return PartialView();
        }
        public ActionResult ShowStudentsOfLecturer(int lecturerId)
        {
            ViewBag.lecturerId = lecturerId;

            return PartialView();
        }
        public ActionResult ShowMarks(string subjectName)
        {
            ViewBag.subjectName = subjectName;

            return PartialView();
        }


        public ActionResult Manage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddLecturer()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddLecturer(Lecturer lecturer)
        {

            using (UniversityContext db = new UniversityContext())
            {
                db.Lecturers.Add(lecturer);

                db.SaveChanges();
            }

            return RedirectToAction("Manage");
        }
        [HttpGet]
        public ActionResult AddStudent()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            using (UniversityContext db = new UniversityContext())
            {
                db.Students.Add(student);

                db.SaveChanges();
            }

            return RedirectToAction("Manage");
        }
        [HttpGet]
        public ActionResult AddSubject()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddSubject(Subject subject)
        {
            using (UniversityContext db = new UniversityContext())
            {
                db.Subjects.Add(subject);

                db.SaveChanges();
            }

            return RedirectToAction("Manage");
        }
        [HttpGet]
        public ActionResult AddMark(int studentId, int subjectId)
        {
            Mark mark = new Mark();
            mark.StudentId = studentId;
            mark.SubjectId = subjectId;

            ViewBag.mark = mark;

            return View();
        }
        [HttpPost]
        public ActionResult AddMark(Mark newMark)
        {
            Mark mark = new Mark();

            using (UniversityContext db = new UniversityContext())
            {
                mark = db.Marks.Add(newMark);

                db.SaveChanges();
            }

            return RedirectToAction("EditStudent", new { studentId = mark.StudentId });
        }

        [HttpGet]
        public ActionResult EditLecturer(int lecturerId)
        {
            Lecturer lecturer = new Lecturer();

            List<Subject> colOfSubjects = new List<Subject>();
            List<Student> colOfStudents = new List<Student>();
            using (UniversityContext db = new UniversityContext())
            {
                lecturer = db.Lecturers.Find(lecturerId);
                db.Entry(lecturer).Collection(c => c.Subjects).Load();


                colOfSubjects = db.Subjects.Where(s => s.Lecturer == null).ToList().Distinct().ToList();
            }

            ViewBag.lecturer = lecturer;
            ViewBag.colOfSubjects = colOfSubjects;

            return View();
        }
        public ActionResult LecturerLeaveSubject(int lecturerId, string subjectName)
        {
            using (UniversityContext db = new UniversityContext())
            {
                Lecturer lecturer = db.Lecturers.Find(lecturerId);
                db.Entry(lecturer).Collection(c => c.Subjects).Load();
                lecturer.Subjects.Clear();

                db.Entry(lecturer).Collection(c => c.Students).Load();
                lecturer.Students.Clear();

                db.SaveChanges();
            }

            return RedirectToAction("EditLecturer", new { lecturerId = lecturerId });
        }
        public ActionResult LecturerAddSubject(int lecturerId, string subjectName)
        {
            using (UniversityContext db = new UniversityContext())
            {
                Lecturer lecturer = db.Lecturers.Find(lecturerId);
                List<Subject> colOfSubjects = db.Subjects.Where(s => s.Name == subjectName).ToList();
                foreach (Subject subject in colOfSubjects)
                {
                    lecturer.Subjects.Add(subject);
                }

                List<Student> colOfStudentss = db.Students.Where(s => s.Subjects.Any(sbj=>sbj.Name == subjectName)).ToList();
                foreach (Student student in colOfStudentss)
                {
                    lecturer.Students.Add(student);
                }

                db.SaveChanges();
            }

            return RedirectToAction("EditLecturer", new { lecturerId = lecturerId });
        }
        [HttpGet]
        public ActionResult EditStudent(int studentId)
        {
            Student student = new Student();
            using (UniversityContext db = new UniversityContext())
            {
                student = db.Students.Find(studentId);
                db.Entry(student).Collection(c => c.Marks).Load();
                db.Entry(student).Collection(c => c.Subjects).Load();

            }

            ViewBag.student = student;

            return View();
        }
        [HttpGet]
        public ActionResult EditMark(int markId)
        {
            Mark mark = new Mark();
            using (UniversityContext db = new UniversityContext())
            {
                mark = db.Marks.Find(markId);
            }

            ViewBag.mark = mark;

            return View();
        }
        [HttpPost]
        public ActionResult EditMark(Mark markWithNewValue)
        {
            Mark mark = new Mark();

            using (UniversityContext db = new UniversityContext())
            {
                mark = db.Marks.Find(markWithNewValue.Id);
                mark.Value = markWithNewValue.Value;
                db.SaveChanges();
            }

            return RedirectToAction("EditStudent", new { studentId = mark.StudentId });
        }


        public ActionResult DeleteLecturer(int lecturerId)
        {
            using (UniversityContext db = new UniversityContext())
            {
                Lecturer lecturer = db.Lecturers.Find(lecturerId);

                db.Entry(lecturer).Collection(c => c.Students).Load();
                lecturer.Students.Clear();

                db.Entry(lecturer).Collection(c => c.Subjects).Load();
                //List<Subject> colOfSubjects = lecturer.Subjects.ToList();
                //db.Subjects.RemoveRange(colOfSubjects);
                lecturer.Subjects.Clear();


                db.Lecturers.Remove(lecturer);
                db.SaveChanges();
            }

            return RedirectToAction("Manage");
        }
        public ActionResult DeleteStudent(int studentId)
        {
            using (UniversityContext db = new UniversityContext())
            {
                Student student = db.Students.Find(studentId);
                    
                db.Entry(student).Collection(c => c.Lecturers).Load();
                student.Lecturers.Clear();

                db.Entry(student).Collection(c => c.Marks).Load();
                List<Mark> colOfMarks = student.Marks.ToList();
                student.Marks.Clear();
                db.Marks.RemoveRange(colOfMarks);

                db.Entry(student).Collection(c => c.Subjects).Load();
                student.Subjects.Clear();


                db.Students.Remove(student);
                db.SaveChanges();
            }

            return RedirectToAction("Manage");
        }
        public ActionResult DeleteSubject(string subjectName)
        {
            using (UniversityContext db = new UniversityContext())
            {
                List<Subject> colOfSubjects = db.Subjects.Where(s => s.Name == subjectName).ToList();

                foreach (Subject subject in colOfSubjects)
                {
                    db.Entry(subject).Collection(c => c.Students).Load();
                    subject.Students.Clear();
                    subject.Lecturer = null;

                    db.Entry(subject).Collection(c => c.Marks).Load();
                    List<Mark> colOfMarks = subject.Marks.ToList();
                    subject.Marks.Clear();
                    db.Marks.RemoveRange(colOfMarks);
                }
                db.Subjects.RemoveRange(colOfSubjects);

                db.SaveChanges();
            }

            return RedirectToAction("Manage");
        }

    }
}