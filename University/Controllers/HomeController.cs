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

    }
}