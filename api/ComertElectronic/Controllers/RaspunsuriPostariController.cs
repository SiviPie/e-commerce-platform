using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaspunsuriPostariController : ControllerBase
    {
        private IConfiguration _configuration;
        public RaspunsuriPostariController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetRaspunsuriPostari")]
        public JsonResult GetRaspunsuriPostari()
        {
            string query = "SELECT * FROM dbo.RaspunsuriPostari";
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
        [Route("GetRaspunsuriPostare/{id}")]
        public JsonResult GetRaspunsuriPostare(int id)
        {
            string query = $"SELECT RP.*, U.Username, U.ImagineProfil FROM RaspunsuriPostari RP JOIN Utilizatori U ON RP.ID_Utilizator = U.ID_Utilizator WHERE ID_Postare = {id} ORDER BY RP.DataRaspuns DESC";
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
        [Route("AddRaspunsPostare")]
        public JsonResult AddRaspunsPostare([FromForm] int ID_Utilizator, [FromForm] int ID_Postare, [FromForm] string ContinutRaspuns, [FromForm] DateTime DataRaspuns)
        {
            string query = "INSERT INTO dbo.RaspunsuriPostari (ID_Utilizator, ID_Postare, ContinutRaspuns, DataRaspuns) VALUES (@ID_Utilizator, @ID_Postare, @ContinutRaspuns, @DataRaspuns)";
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Utilizator", ID_Utilizator);
                    myCommand.Parameters.AddWithValue("@ID_Postare", ID_Postare);
                    myCommand.Parameters.AddWithValue("@ContinutRaspuns", ContinutRaspuns);
                    myCommand.Parameters.AddWithValue("@DataRaspuns", DataRaspuns);
                    myCommand.ExecuteNonQuery();
                }
                myCon.Close();
            }

            return new JsonResult("Added successfully");
        }

    }
}
