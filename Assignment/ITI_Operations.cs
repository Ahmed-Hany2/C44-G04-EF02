using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class ITI_Operations
    {
        private readonly ITIContext _context;

        public ITI_Operations(ITIContext context)
        {
            _context = context;
        }

        // ========== STUDENT CRUD ==========
        public void CreateStudent(string fname, string lname, string address, int age, int? depId = null)
        {
            var student = new Student
            {
                FName = fname,
                LName = lname,
                Address = address,
                Age = age,
                Dep_Id = depId
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            Console.WriteLine($"Student {fname} {lname} created with ID: {student.ID}");
        }

        public Student ReadStudent(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Stud_Courses)
                    .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.ID == id);

            if (student != null)
            {
                Console.WriteLine($"Student: {student.FName} {student.LName}, Age: {student.Age}");
                Console.WriteLine($"Department: {student.Department?.Name ?? "N/A"}");
            }
            return student;
        }

        public void UpdateStudent(int id, string fname, string lname, string address, int age, int? depId = null)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                student.FName = fname;
                student.LName = lname;
                student.Address = address;
                student.Age = age;
                student.Dep_Id = depId;
                _context.SaveChanges();
                Console.WriteLine($"Student ID {id} updated successfully");
            }
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                Console.WriteLine($"Student ID {id} deleted successfully");
            }
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students
                .Include(s => s.Department)
                .ToList();
        }

        // ========== DEPARTMENT CRUD ==========
        public void CreateDepartment(string name, DateTime hiringDate, int? managerId = null)
        {
            var department = new Department
            {
                Name = name,
                HiringDate = hiringDate,
                Ins_ID = managerId
            };
            _context.Departments.Add(department);
            _context.SaveChanges();
            Console.WriteLine($"Department {name} created with ID: {department.ID}");
        }

        public Department ReadDepartment(int id)
        {
            var department = _context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .FirstOrDefault(d => d.ID == id);

            if (department != null)
            {
                Console.WriteLine($"Department: {department.Name}");
                Console.WriteLine($"Manager: {department.Manager?.Name ?? "N/A"}");
                Console.WriteLine($"Number of Students: {department.Students.Count}");
            }
            return department;
        }

        public void UpdateDepartment(int id, string name, DateTime hiringDate, int? managerId = null)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                department.Name = name;
                department.HiringDate = hiringDate;
                department.Ins_ID = managerId;
                _context.SaveChanges();
                Console.WriteLine($"Department ID {id} updated successfully");
            }
        }

        public void DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                Console.WriteLine($"Department ID {id} deleted successfully");
            }
        }

        public List<Department> GetAllDepartments()
        {
            return _context.Departments
                .Include(d => d.Manager)
                .ToList();
        }

        // ========== COURSE CRUD ==========
        public void CreateCourse(string name, int duration, string description, int? topicId = null)
        {
            var course = new Course
            {
                Name = name,
                Duration = duration,
                Description = description,
                Top_ID = topicId
            };
            _context.Courses.Add(course);
            _context.SaveChanges();
            Console.WriteLine($"Course {name} created with ID: {course.ID}");
        }

        public Course ReadCourse(int id)
        {
            var course = _context.Courses
                .Include(c => c.Topic)
                .Include(c => c.Stud_Courses)
                    .ThenInclude(sc => sc.Student)
                .Include(c => c.Course_Insts)
                    .ThenInclude(ci => ci.Instructor)
                .FirstOrDefault(c => c.ID == id);

            if (course != null)
            {
                Console.WriteLine($"Course: {course.Name}, Duration: {course.Duration} hours");
                Console.WriteLine($"Topic: {course.Topic?.Name ?? "N/A"}");
                Console.WriteLine($"Enrolled Students: {course.Stud_Courses.Count}");
            }
            return course;
        }

        public void UpdateCourse(int id, string name, int duration, string description, int? topicId = null)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                course.Name = name;
                course.Duration = duration;
                course.Description = description;
                course.Top_ID = topicId;
                _context.SaveChanges();
                Console.WriteLine($"Course ID {id} updated successfully");
            }
        }

        public void DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
                Console.WriteLine($"Course ID {id} deleted successfully");
            }
        }

        public List<Course> GetAllCourses()
        {
            return _context.Courses
                .Include(c => c.Topic)
                .ToList();
        }

        // ========== INSTRUCTOR CRUD ==========
        public void CreateInstructor(string name, decimal salary, string address, decimal hourRate, decimal bonus, int? deptId = null)
        {
            var instructor = new Instructor
            {
                Name = name,
                Salary = salary,
                Address = address,
                HourRate = hourRate,
                Bonus = bonus,
                Dept_ID = deptId
            };
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            Console.WriteLine($"Instructor {name} created with ID: {instructor.ID}");
        }

        public Instructor ReadInstructor(int id)
        {
            var instructor = _context.Instructors
                .Include(i => i.Department)
                .Include(i => i.Course_Insts)
                    .ThenInclude(ci => ci.Course)
                .Include(i => i.ManagedDepartments)
                .FirstOrDefault(i => i.ID == id);

            if (instructor != null)
            {
                Console.WriteLine($"Instructor: {instructor.Name}, Salary: {instructor.Salary:C}");
                Console.WriteLine($"Department: {instructor.Department?.Name ?? "N/A"}");
                Console.WriteLine($"Teaching Courses: {instructor.Course_Insts.Count}");
            }
            return instructor;
        }

        public void UpdateInstructor(int id, string name, decimal salary, string address, decimal hourRate, decimal bonus, int? deptId = null)
        {
            var instructor = _context.Instructors.Find(id);
            if (instructor != null)
            {
                instructor.Name = name;
                instructor.Salary = salary;
                instructor.Address = address;
                instructor.HourRate = hourRate;
                instructor.Bonus = bonus;
                instructor.Dept_ID = deptId;
                _context.SaveChanges();
                Console.WriteLine($"Instructor ID {id} updated successfully");
            }
        }

        public void DeleteInstructor(int id)
        {
            var instructor = _context.Instructors.Find(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                _context.SaveChanges();
                Console.WriteLine($"Instructor ID {id} deleted successfully");
            }
        }

        public List<Instructor> GetAllInstructors()
        {
            return _context.Instructors
                .Include(i => i.Department)
                .ToList();
        }

        // ========== TOPIC CRUD ==========
        public void CreateTopic(string name)
        {
            var topic = new Topic { Name = name };
            _context.Topics.Add(topic);
            _context.SaveChanges();
            Console.WriteLine($"Topic {name} created with ID: {topic.ID}");
        }

        public Topic ReadTopic(int id)
        {
            var topic = _context.Topics
                .Include(t => t.Courses)
                .FirstOrDefault(t => t.ID == id);

            if (topic != null)
            {
                Console.WriteLine($"Topic: {topic.Name}");
                Console.WriteLine($"Number of Courses: {topic.Courses.Count}");
            }
            return topic;
        }

        public void UpdateTopic(int id, string name)
        {
            var topic = _context.Topics.Find(id);
            if (topic != null)
            {
                topic.Name = name;
                _context.SaveChanges();
                Console.WriteLine($"Topic ID {id} updated successfully");
            }
        }

        public void DeleteTopic(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                _context.SaveChanges();
                Console.WriteLine($"Topic ID {id} deleted successfully");
            }
        }

        public List<Topic> GetAllTopics()
        {
            return _context.Topics.ToList();
        }

        // ========== STUD_COURSE CRUD (Enrollment) ==========
        public void EnrollStudentInCourse(int studentId, int courseId, decimal? grade = null)
        {
            var enrollment = new Stud_Course
            {
                stud_ID = studentId,
                Course_ID = courseId,
                Grade = grade
            };
            _context.Stud_Courses.Add(enrollment);
            _context.SaveChanges();
            Console.WriteLine($"Student {studentId} enrolled in Course {courseId}");
        }

        public Stud_Course ReadEnrollment(int studentId, int courseId)
        {
            var enrollment = _context.Stud_Courses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .FirstOrDefault(sc => sc.stud_ID == studentId && sc.Course_ID == courseId);

            if (enrollment != null)
            {
                Console.WriteLine($"Student: {enrollment.Student.FName} {enrollment.Student.LName}");
                Console.WriteLine($"Course: {enrollment.Course.Name}");
                Console.WriteLine($"Grade: {enrollment.Grade?.ToString() ?? "Not graded yet"}");
            }
            return enrollment;
        }

        public void UpdateEnrollmentGrade(int studentId, int courseId, decimal grade)
        {
            var enrollment = _context.Stud_Courses
                .FirstOrDefault(sc => sc.stud_ID == studentId && sc.Course_ID == courseId);

            if (enrollment != null)
            {
                enrollment.Grade = grade;
                _context.SaveChanges();
                Console.WriteLine($"Grade updated for Student {studentId} in Course {courseId}");
            }
        }

        public void DeleteEnrollment(int studentId, int courseId)
        {
            var enrollment = _context.Stud_Courses
                .FirstOrDefault(sc => sc.stud_ID == studentId && sc.Course_ID == courseId);

            if (enrollment != null)
            {
                _context.Stud_Courses.Remove(enrollment);
                _context.SaveChanges();
                Console.WriteLine($"Enrollment deleted for Student {studentId} from Course {courseId}");
            }
        }

        public List<Stud_Course> GetAllEnrollments()
        {
            return _context.Stud_Courses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .ToList();
        }

        // ========== COURSE_INST CRUD (Course Assignment) ==========
        public void AssignInstructorToCourse(int instructorId, int courseId, string evaluation = null)
        {
            var assignment = new Course_Inst
            {
                inst_ID = instructorId,
                Course_ID = courseId,
                evaluate = evaluation
            };
            _context.Course_Insts.Add(assignment);
            _context.SaveChanges();
            Console.WriteLine($"Instructor {instructorId} assigned to Course {courseId}");
        }

        public Course_Inst ReadCourseAssignment(int instructorId, int courseId)
        {
            var assignment = _context.Course_Insts
                .Include(ci => ci.Instructor)
                .Include(ci => ci.Course)
                .FirstOrDefault(ci => ci.inst_ID == instructorId && ci.Course_ID == courseId);

            if (assignment != null)
            {
                Console.WriteLine($"Instructor: {assignment.Instructor.Name}");
                Console.WriteLine($"Course: {assignment.Course.Name}");
                Console.WriteLine($"Evaluation: {assignment.evaluate ?? "Not evaluated yet"}");
            }
            return assignment;
        }

        public void UpdateCourseEvaluation(int instructorId, int courseId, string evaluation)
        {
            var assignment = _context.Course_Insts
                .FirstOrDefault(ci => ci.inst_ID == instructorId && ci.Course_ID == courseId);

            if (assignment != null)
            {
                assignment.evaluate = evaluation;
                _context.SaveChanges();
                Console.WriteLine($"Evaluation updated for Instructor {instructorId} teaching Course {courseId}");
            }
        }

        public void DeleteCourseAssignment(int instructorId, int courseId)
        {
            var assignment = _context.Course_Insts
                .FirstOrDefault(ci => ci.inst_ID == instructorId && ci.Course_ID == courseId);

            if (assignment != null)
            {
                _context.Course_Insts.Remove(assignment);
                _context.SaveChanges();
                Console.WriteLine($"Assignment deleted for Instructor {instructorId} from Course {courseId}");
            }
        }

        public List<Course_Inst> GetAllCourseAssignments()
        {
            return _context.Course_Insts
                .Include(ci => ci.Instructor)
                .Include(ci => ci.Course)
                .ToList();
        }
    }
}
