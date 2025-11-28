namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ITIContext())
            {
                // Ensure database is created
                context.Database.EnsureCreated();

                var operations = new ITI_Operations(context);

                Console.WriteLine("=== ITI Database Management System ===\n");

                // Create Topics
                operations.CreateTopic("Programming");
                operations.CreateTopic("Database Management");
                operations.CreateTopic("Web Development");

                // Create Departments
                operations.CreateDepartment("Computer Science", DateTime.Now);
                operations.CreateDepartment("Information Systems", DateTime.Now);

                // Create Instructors
                operations.CreateInstructor("Dr. Ahmed Ali", 15000, "Cairo", 200, 2000, 1);
                operations.CreateInstructor("Dr. Sara Mohamed", 18000, "Alexandria", 250, 2500, 1);

                // Update Department Manager
                operations.UpdateDepartment(1, "Computer Science", DateTime.Now, 1);

                // Create Courses
                operations.CreateCourse("C# Programming", 40, "Learn C# from scratch", 1);
                operations.CreateCourse("SQL Server", 30, "Database fundamentals", 2);
                operations.CreateCourse("ASP.NET Core", 50, "Web development with .NET", 3);

                // Create Students
                operations.CreateStudent("Ahmed", "Hassan", "Cairo, Egypt", 22, 1);
                operations.CreateStudent("Fatma", "Ibrahim", "Giza, Egypt", 21, 1);
                operations.CreateStudent("Mohamed", "Sayed", "Alexandria, Egypt", 23, 2);

                // Enroll Students in Courses
                operations.EnrollStudentInCourse(1, 1, 85.5m);
                operations.EnrollStudentInCourse(1, 2, 90.0m);
                operations.EnrollStudentInCourse(2, 1, 78.5m);
                operations.EnrollStudentInCourse(3, 3, 88.0m);

                // Assign Instructors to Courses
                operations.AssignInstructorToCourse(1, 1, "Excellent teaching");
                operations.AssignInstructorToCourse(1, 2, "Very organized");
                operations.AssignInstructorToCourse(2, 3, "Great interaction");

                Console.WriteLine("\n=== Reading Data with Relationships ===\n");

                // Read data with relationships
                operations.ReadStudent(1);
                Console.WriteLine();
                operations.ReadDepartment(1);
                Console.WriteLine();
                operations.ReadCourse(1);
                Console.WriteLine();
                operations.ReadInstructor(1);
                Console.WriteLine();


             
            }
        }
    }
}
