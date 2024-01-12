using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonuriController : ControllerBase
    {
        private IConfiguration _configuration;
        public BonuriController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetBonuri")]
        public JsonResult GetBonuri()
        {
            string query = "SELECT * FROM dbo.Bonuri";
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
        [Route("GetBonuriByUtilizator/{id}")]
        public JsonResult GetBonuriByUtilizator(int id)
        {
            string query = $"SELECT B.* FROM Bonuri B INNER JOIN Utilizatori U ON B.ID_Utilizator = U.ID_Utilizator WHERE B.ID_Utilizator = {id} ORDER BY B.DataFacturare DESC";
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
        [Route("AddBon")]
        public JsonResult AddBon([FromForm] int ID_Utilizator, [FromForm] string DataFacturare, [FromForm] int ID_Voucher)
        {
            try
            {
                string query = "INSERT INTO dbo.Bonuri (ID_Utilizator, DataFacturare, ID_Voucher) OUTPUT INSERTED.ID_Bon VALUES (@ID_Utilizator, @DataFacturare, @ID_Voucher)";
                int insertedID = 0;

                using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("ComertElectronicDBCon")))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ID_Utilizator", ID_Utilizator);
                        myCommand.Parameters.AddWithValue("@DataFacturare", DateTime.Parse(DataFacturare));
                        myCommand.Parameters.AddWithValue("@ID_Voucher", ID_Voucher);

                        // ExecuteScalar to get the inserted ID
                        insertedID = (int)myCommand.ExecuteScalar();
                    }
                }

                // Return the inserted ID as JSON
                return new JsonResult(new { ID_Bon = insertedID, Message = "Added successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return new JsonResult(new { Message = $"Error: {ex.Message}" });
            }
        }



    }
}
