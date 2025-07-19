using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using School.Models;
using SchoolDbcontext.Models;

namespace School.Controllers
{
    public class TeachersPageController : Controller
    {
        //  Declare context here so all methods can use it
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // Show teacher by ID
        public IActionResult Show(int id)
        {
            Teacher teacher = null;

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                Console.WriteLine("Show action called with ID: " + id);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        teacher = new Teacher()
                        {
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            TeacherFName = reader["teacherfname"].ToString(),
                            TeacherLName = reader["teacherlname"].ToString(),
                            HireDate = Convert.ToDateTime(reader["hiredate"]),
                            Salary = Convert.ToDecimal(reader["salary"])
                        };
                    }
                }
            }

            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            return View("List", teacher); //  Make sure List.cshtml expects a single teacher
        }
    }
}
