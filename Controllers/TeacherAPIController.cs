using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using School.Models;
using SchoolDbcontext.Models;

namespace School.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        /// <summary>
        /// Returns a list of all teacher details
        /// </summary>
        /// <example>
        /// GET api/teacher/GetAllTeachers
        /// </example>
        [HttpGet("GetAllTeachers")]
        public ActionResult<List<Teacher>> GetAllTeachers()
        {
            List<Teacher> teacherList = new List<Teacher>();

            try
            {
                using (MySqlConnection connection = _context.AccessDatabase())
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM teachers";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Teacher teacher = new Teacher()
                            {
                                TeacherId = Convert.ToInt32(reader["teacherid"]),
                                TeacherFName = reader["teacherfname"].ToString(),
                                TeacherLName = reader["teacherlname"].ToString(),
                                HireDate = Convert.ToDateTime(reader["hiredate"]),
                                Salary = Convert.ToDecimal(reader["salary"])
                            };

                            teacherList.Add(teacher);
                        }
                    }
                }

                return Ok(teacherList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        /// <summary>
        /// Returns details of a single teacher based on ID
        /// </summary>
        /// <example>
        /// GET api/teacher/FindTeacher?id=3
        /// </example>
        [HttpGet("FindTeacher")]
        public ActionResult<Teacher> FindTeacher([FromQuery] int id)
        {
            try
            {
                using (MySqlConnection connection = _context.AccessDatabase())
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Teacher teacher = new Teacher()
                            {
                                TeacherId = Convert.ToInt32(reader["teacherid"]),
                                TeacherFName = reader["teacherfname"].ToString(),
                                TeacherLName = reader["teacherlname"].ToString(),
                                HireDate = Convert.ToDateTime(reader["hiredate"]),
                                Salary = Convert.ToDecimal(reader["salary"])
                            };

                            return Ok(teacher);
                        }
                        else
                        {
                            return NotFound($"No teacher found with ID {id}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}






