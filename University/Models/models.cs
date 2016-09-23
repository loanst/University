using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace University.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Lecturer> Lecturers { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Mark> Marks { get; set; }


        public Student()
        {
            Lecturers = new List<Lecturer>();
            Subjects = new List<Subject>();
            Marks = new List<Mark>();
        }

    }
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }

        public Lecturer()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();

        }

    }
    public class Subject : IEquatable<Subject>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int? LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }


        public ICollection<Student> Students { get; set; }
        public ICollection<Mark> Marks { get; set; }

        public Subject()
        {
            Students = new List<Student>();
            Marks = new List<Mark>();
        }



        public bool Equals(Subject other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return Name.Equals(other.Name);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashProductName = Name == null ? 0 : Name.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName;
        }
    }

    public class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int? StudentId { get; set; }
        public Student Student { get; set; }

        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }


    }


    public class UniversityInitializer : CreateDatabaseIfNotExists<UniversityContext>
    {
        protected override void Seed(UniversityContext db)
        {
            Lecturer lecturer1 = new Lecturer() {Name = "Alan Turing" };
            Lecturer lecturer2 = new Lecturer() { Name = "Albert Einstein" };

            db.Lecturers.Add(lecturer1);
            db.Lecturers.Add(lecturer2);
            db.SaveChanges();

            Mark mark1 = new Mark() {Value = 5 };
            Mark mark2 = new Mark() { Value = 4 };
            Mark mark3 = new Mark() { Value = 4 };
            Mark mark4 = new Mark() { Value = 5 };
            Mark mark5 = new Mark() { Value = 3 };
            Mark mark6 = new Mark() { Value = 5 };
            Mark mark7 = new Mark() { Value = 5 };
            Mark mark8 = new Mark() { Value = 4 };
            Mark mark9 = new Mark() { Value = 4 };
            Mark mark10 = new Mark() { Value = 5 };

            db.Marks.Add(mark1);
            db.Marks.Add(mark2);
            db.Marks.Add(mark3);
            db.Marks.Add(mark4);
            db.Marks.Add(mark5);
            db.Marks.Add(mark6);
            db.Marks.Add(mark7);
            db.Marks.Add(mark8);
            db.Marks.Add(mark9);
            db.Marks.Add(mark10);


            Subject subject1 = new Subject() {Name = "Mathematics", Lecturer = lecturer1 };
            subject1.Marks.Add(mark1);
            subject1.Marks.Add(mark3);
            Subject subject2 = new Subject() { Name = "Mathematics", Lecturer = lecturer1 };
            subject2.Marks.Add(mark2);
            Subject subject3 = new Subject() { Name = "Physics", Lecturer = lecturer2 };
            subject3.Marks.Add(mark4);
            subject3.Marks.Add(mark5);
            subject3.Marks.Add(mark6);
            subject3.Marks.Add(mark7);
            Subject subject4 = new Subject() { Name = "Mathematics", Lecturer = lecturer1 };
            subject4.Marks.Add(mark8);
            Subject subject5 = new Subject() { Name = "Physics", Lecturer = lecturer2 };
            subject5.Marks.Add(mark9);
            subject5.Marks.Add(mark10);

            db.Subjects.Add(subject1);
            db.Subjects.Add(subject2);
            db.Subjects.Add(subject3);
            db.Subjects.Add(subject4);
            db.Subjects.Add(subject5);
            db.SaveChanges();


            Student student1 = new Student() {Name = "Ivanov Ivan" };
            student1.Subjects.Add(subject1);
            student1.Marks.Add(mark1);
            student1.Marks.Add(mark3);
            student1.Lecturers.Add(lecturer1);
            db.Students.Add(student1);

            Student student2 = new Student() {Name = "Petrov Petr" };
            student2.Subjects.Add(subject2);
            student2.Marks.Add(mark2);
            student2.Lecturers.Add(lecturer1);
            db.Students.Add(student2);

            Student student3 = new Student() { Name = "Sidorov Sidor" };
            student3.Subjects.Add(subject3);
            student3.Marks.Add(mark4);
            student3.Marks.Add(mark5);
            student3.Marks.Add(mark6);
            student3.Marks.Add(mark7);
            student3.Lecturers.Add(lecturer2);
            db.Students.Add(student3);

            Student student4 = new Student() { Name = "Stepanov Stepan" };
            student4.Subjects.Add(subject4);
            student4.Subjects.Add(subject5);
            student4.Marks.Add(mark8);
            student4.Marks.Add(mark9);
            student4.Marks.Add(mark10);
            student4.Lecturers.Add(lecturer1);
            student4.Lecturers.Add(lecturer2);
            db.Students.Add(student4);

            db.SaveChanges();
        }
    }


    public class UniversityContext : DbContext
    {
        public UniversityContext()
        {
            Database.SetInitializer<UniversityContext>(new UniversityInitializer());
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }

    }

}