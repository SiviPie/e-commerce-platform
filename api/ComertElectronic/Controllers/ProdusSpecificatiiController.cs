using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdusSpecificatiiController : ControllerBase
    {
        private IConfiguration _configuration;
        public ProdusSpecificatiiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetProdusSpecificatii")]
        public JsonResult GetProdusSpecificatii()
        {
            string query = "SELECT * FROM dbo.ProdusSpecificatii";
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
        [Route("AddProdusSpecificatie")]

        public JsonResult AddProdusSpecificatie([FromForm] int ID_Produs, [FromForm] int ID_Specificatie)
        {
            string query = "INSERT INTO dbo.ProdusSpecificatii (ID_Produs, ID_Specificatie) VALUES (@ID_Produs, @ID_Specificatie)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Produs", ID_Produs);
                    myCommand.Parameters.AddWithValue("@ID_Specificatie", ID_Specificatie);
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
