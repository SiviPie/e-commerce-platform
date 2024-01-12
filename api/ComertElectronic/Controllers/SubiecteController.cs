using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubiecteController : ControllerBase
    {
        private IConfiguration _configuration;
        public SubiecteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetSubiecte")]

        public JsonResult GetSubiecte()
        {
            string query = "SELECT * FROM dbo.Subiecte";
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
        [Route("GetSubiectById/{id}")]
        public JsonResult GetSubiectById(int id)
        {
            string query = $"SELECT * FROM Subiecte WHERE ID_Subiect = {id}";
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
    }
}
