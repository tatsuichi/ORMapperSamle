using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplicationDapper.Models;

namespace WebApplicationDapper.Controllers
{
    public class StudentsController : Controller
    {
        private readonly string _connectionString;

        public StudentsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbContext");
        }

        // GET: Students
        public async Task<ActionResult> Index()
        {
            using var connection = new SqlConnection(_connectionString);
            
            var sql = "SELECT * FROM Student";
            
            var students = await connection.QueryAsync<Student>(sql);

            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Student WHERE ID = @Id";

            var student = connection.QueryFirstOrDefault<Student>(sql, new { Id = id });

            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var sql = "INSERT INTO Student (FirstMidName, LastName, EnrollmentDate) VALUES (@FirstMidName, @LastName, @EnrollmentDate)";

                if (!int.TryParse(collection["ID"].ToString(), out var id))
                {
                    return View();
                }
                if (!DateTime.TryParse(collection["EnrollmentDate"].ToString(), out var enrollmentDate))
                {
                    return View();
                }

                var student = new Student()
                {
                    ID = id,
                    LastName = collection["LastName"].ToString(),
                    FirstMidName = collection["FirstMidName"].ToString(),
                    EnrollmentDate = enrollmentDate
                };

                var rowsAffected = await connection.ExecuteAsync(sql, student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Student WHERE ID = @Id";

            var student = connection.QueryFirstOrDefault<Student>(sql, new { Id = id });

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var sql = "UPDATE Student SET FirstMidName = @FirstMidName, LastName = @LastName, EnrollmentDate = @EnrollmentDate WHERE ID = @Id";

                if (!DateTime.TryParse(collection["EnrollmentDate"].ToString(), out var enrollmentDate))
                {
                    return View();
                }

                var student = new Student()
                {
                    ID = id,
                    LastName = collection["LastName"].ToString(),
                    FirstMidName = collection["FirstMidName"].ToString(),
                    EnrollmentDate = enrollmentDate
                };

                var rowsAffected = await connection.ExecuteAsync(sql, student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Student WHERE ID = @Id";

            var student = connection.QueryFirstOrDefault<Student>(sql, new { Id = id });

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var sql = "DELETE FROM Student WHERE ID = @Id";

                var student = new Student()
                {
                    ID = id,
                };

                var rowsAffected = await connection.ExecuteAsync(sql, student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
