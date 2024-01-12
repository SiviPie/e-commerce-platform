using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetaliiBonController : ControllerBase
    {
        private IConfiguration _configuration;
        public DetaliiBonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetDetaliiBon")]
        public JsonResult GetDetaliiBon()
        {
            string query = "SELECT * FROM dbo.DetaliiBon";
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
        [Route("GetDetaliiBonByBon/{id}")]
        public JsonResult GetDetaliiBonByBon(int id)
        {
            string query = $"SELECT * FROM dbo.DetaliiBon WHERE ID_Bon = {id}";
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
        [Route("AddDetaliuBon")]
        public JsonResult AddDetaliuBon([FromForm] int ID_Bon, [FromForm] int ID_Produs, [FromForm] int Cantitate, [FromForm] float Pret)
        {
            string query = "INSERT INTO dbo.DetaliiBon (ID_Bon, ID_Produs, Cantitate, Pret) VALUES (@ID_Bon, @ID_Produs, @Cantitate, @Pret)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Bon", ID_Bon);
                    myCommand.Parameters.AddWithValue("@ID_Produs", ID_Produs);
                    myCommand.Parameters.AddWithValue("@Cantitate", Cantitate);
                    myCommand.Parameters.AddWithValue("@Pret", Pret);
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
