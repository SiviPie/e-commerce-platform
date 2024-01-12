using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecenziiController : ControllerBase
    {
        private IConfiguration _configuration;
        public RecenziiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetRecenzii")]
        public JsonResult GetRecenzii()
        {
            string query = "SELECT * FROM dbo.Recenzii";
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
        [Route("GetRecenziiProdus/{id}")]
        public JsonResult GetRecenziiProdus(int id)
        {
            string query = "SELECT U.Username, U.ImagineProfil, R.ID_Recenzie, R.ContinutRecenzie, R.Stele, R.DataRecenzie " +
                "FROM Utilizatori U INNER JOIN Recenzii R ON U.ID_Utilizator = R.ID_Utilizator " +
                "INNER JOIN Produse P ON R.ID_Produs = P.ID_Produs " +
                $"WHERE P.ID_Produs = {id} " +
                "GROUP BY U.Username, U.ImagineProfil, R.ID_Recenzie, R.ContinutRecenzie, R.Stele, R.DataRecenzie " +
                "ORDER BY R.DataRecenzie DESC";
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
        [Route("AddRecenzie")]

        public JsonResult AddRecenzie([FromForm] int ID_Utilizator, [FromForm] int ID_Produs, [FromForm] string ContinutRecenzie, [FromForm] int Stele, [FromForm] DateTime DataRecenzie)
        {
            string query = "INSERT INTO dbo.Recenzii (ID_Utilizator, ID_Produs, ContinutRecenzie, Stele, DataRecenzie) VALUES (@ID_Utilizator, @ID_Produs, @ContinutRecenzie, @Stele, @DataRecenzie)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Utilizator", ID_Utilizator);
                    myCommand.Parameters.AddWithValue("@ID_Produs", ID_Produs);
                    myCommand.Parameters.AddWithValue("@ContinutRecenzie", ContinutRecenzie);
                    myCommand.Parameters.AddWithValue("@Stele", Stele);
                    myCommand.Parameters.AddWithValue("@DataRecenzie", DataRecenzie);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
                myCon.Close();
            }

            return new JsonResult("Added successfully");
        }
    }
}
