using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizatoriController : ControllerBase
    {
        private IConfiguration _configuration;
        public UtilizatoriController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetUtilizatori")]

        public JsonResult GetUtilizatori()
        {
            string query = "SELECT * FROM dbo.Utilizatori";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon)) { 
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetUtilizatorById/{id}")]
        public JsonResult GetUtilizatorById(int id)
        {
            string query = $"SELECT * FROM Utilizatori WHERE ID_Utilizator = {id}";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("IsAdmin/{id}")]

        public JsonResult IsAdmin(int id)
        {
            string query = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM Administratori WHERE ID_Utilizator = {id}) THEN 1 ELSE 0 END AS IsAdmin;";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetTopReviewers")]

        public JsonResult GetTopReviewers()
        {
            string query = $"SELECT U.Nume, U.Prenume, R.NumarRecenzii, R.MediaStele FROM Utilizatori U INNER JOIN ( SELECT ID_Utilizator, COUNT(*) AS NumarRecenzii, AVG(Stele) AS MediaStele FROM Recenzii GROUP BY ID_Utilizator HAVING COUNT(*) > 0) R ON U.ID_Utilizator = R.ID_Utilizator ORDER BY R.NumarRecenzii DESC";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetTopPosters")]

        public JsonResult GetTopPosters()
        {
            string query = $"SELECT U.Nume, U.Prenume, COUNT(P.ID_Postare) AS NumarTotalPostari FROM Utilizatori U INNER JOIN Postari P ON U.ID_Utilizator = P.ID_Utilizator WHERE EXISTS ( SELECT 1 FROM Postari WHERE ID_Utilizator = U.ID_Utilizator) GROUP BY U.Nume, U.Prenume;";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AddUtilizator")]
        public JsonResult AddUtilizator(
     [FromForm] string Nume,
     [FromForm] string Prenume,
     [FromForm] string Username,
     [FromForm] string Email,
     [FromForm] string Telefon,
     [FromForm] string Parola,
     [FromForm] string ImagineProfil,
     [FromForm] string Adresa,
     [FromForm] int Puncte,
     [FromForm] DateTime DataCreate,
     [FromForm] DateTime UltimaAutentificare)
        {
            string query = "INSERT INTO dbo.Utilizatori (Nume, Prenume, Username, Email, Telefon, Parola, ImagineProfil, Adresa, Puncte, DataCreate, UltimaAutentificare) " +
                           "VALUES (@Nume, @Prenume, @Username, @Email, @Telefon, @Parola, @ImagineProfil, @Adresa, @Puncte, @DataCreate, @UltimaAutentificare)";
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("ComertElectronicDBCon")))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Nume", Nume);
                    myCommand.Parameters.AddWithValue("@Prenume", Prenume);
                    myCommand.Parameters.AddWithValue("@Username", Username);
                    myCommand.Parameters.AddWithValue("@Email", Email);
                    myCommand.Parameters.AddWithValue("@Telefon", Telefon);
                    myCommand.Parameters.AddWithValue("@Parola", Parola);
                    myCommand.Parameters.AddWithValue("@ImagineProfil", ImagineProfil);
                    myCommand.Parameters.AddWithValue("@Adresa", Adresa);
                    myCommand.Parameters.AddWithValue("@Puncte", Puncte);
                    myCommand.Parameters.AddWithValue("@DataCreate", DataCreate);
                    myCommand.Parameters.AddWithValue("@UltimaAutentificare", UltimaAutentificare);

                    // Use ExecuteNonQuery for an INSERT operation
                    myCommand.ExecuteNonQuery();
                }
            }

            return new JsonResult("Added successfully");
        }



        [HttpDelete]
        [Route("DeleteUtilizatori")]

        public JsonResult DeleteUtilizatori(int id)
        {
            string query = "DELETE FROM dbo.Utilizatori WHERE ID_Utilizator = @id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted successfully");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromForm] string Username, [FromForm] string Parola)
        {
            string query = "SELECT ID_Utilizator, Nume, Prenume FROM dbo.Utilizatori WHERE Username = @Username AND Parola = @Parola";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Username", Username);
                    myCommand.Parameters.AddWithValue("@Parola", Parola);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            // Check if login was successful
            if (table.Rows.Count == 1)
            {
                // Generate a unique token (you can use a library for this)
                string token = Guid.NewGuid().ToString();

                // Update the user's Token in the database
                int userId = Convert.ToInt32(table.Rows[0]["ID_Utilizator"]);
                UpdateUserToken(userId, token);

                // Return the token and user information with a success status code (200 OK)
                return Ok(new { Token = token, UserID = userId, Nume = table.Rows[0]["Nume"], Prenume = table.Rows[0]["Prenume"] });
            }
            else
            {
                // Return an unauthorized status code (401 Unauthorized) with an error message
                return Unauthorized("Login failed. Invalid username or password.");
            }
        }

        // Helper method to update the user's Token in the database
        private void UpdateUserToken(int userId, string token)
        {
            string updateQuery = "UPDATE dbo.Utilizatori SET Token = @Token WHERE ID_Utilizator = @UserID";
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("ComertElectronicDBCon")))
            {
                myCon.Open();
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, myCon))
                {
                    updateCommand.Parameters.AddWithValue("@UserID", userId);
                    updateCommand.Parameters.AddWithValue("@Token", token);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

    }
}