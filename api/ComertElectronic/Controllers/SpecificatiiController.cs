using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificatiiController : ControllerBase
    {
        private IConfiguration _configuration;
        public SpecificatiiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetSpecificatii")]
        public JsonResult GetSpecificatii()
        {
            string query = "SELECT * FROM dbo.Specificatii";
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
        [Route("GetSpecificatiiProdus/{id}")]
        public JsonResult GetSpecificatiiProdus(int id)
        {
            string query = "SELECT P.NumeProdus, CS.NumeCategorie, S.ValoareSpecificatie " +
                "FROM Produse P INNER JOIN ProdusSpecificatii PS ON P.ID_Produs = PS.ID_Produs " +
                "INNER JOIN Specificatii S ON S.ID_Specificatie = PS.ID_Specificatie " +
                "INNER JOIN CategoriiSpecificatii CS ON S.ID_CategorieSpecificatii = CS.ID_CategorieSpecificatii " +
                $"WHERE P.ID_Produs = {id} " +
                "GROUP BY P.NumeProdus, CS.NumeCategorie, S.ValoareSpecificatie";
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
        [Route("AddSpecificatie")]
        public JsonResult AddSpecificatie([FromForm] string ValoareSpecificatie, [FromForm] int ID_CategorieSpecificatii)
        {
            string query = "INSERT INTO dbo.Specificatii (ValoareSpecificatie, ID_CategorieSpecificatii) VALUES (@ValoareSpecificatie, @ID_CategorieSpecificatii)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ValoareSpecificatie", ValoareSpecificatie);
                    myCommand.Parameters.AddWithValue("@ID_CategorieSpecificatii", ID_CategorieSpecificatii);
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
