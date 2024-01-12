using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriiProduseController : ControllerBase
    {
        private IConfiguration _configuration;
        public CategoriiProduseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetCategorii")]

        public JsonResult GetCategorii()
        {
            string query = "SELECT * FROM dbo.CategoriiProduse";
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
        [Route("GetCategorieById/{id}")]
        public JsonResult GetCategorieById(int id)
        {
            string query = $"SELECT * FROM CategoriiProduse WHERE ID_Categorie = {id}";
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
        [Route("AddCategorii")]
        public JsonResult AddCategorii([FromForm] string NumeCategorie, [FromForm] string DescriereCategorie)
        {
            string query = "INSERT INTO dbo.CategoriiProduse (NumeCategorie, DescriereCategorie) VALUES (@NumeCategorie, @DescriereCategorie)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NumeCategorie", NumeCategorie);
                    myCommand.Parameters.AddWithValue("@DescriereCategorie", DescriereCategorie);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
                myCon.Close();
            }

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        [Route("UpdateCategorie/{id}")]
        public JsonResult UpdateCategorie(
            int id,
            [FromForm] string NumeCategorie,
            [FromForm] string DescriereCategorie
        )
        {
            try
            {
                string query = $"UPDATE dbo.CategoriiProduse SET NumeCategorie = @NumeCategorie, DescriereCategorie = @DescriereCategorie WHERE ID_Categorie = {id}";

                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");

                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id", id);
                        myCommand.Parameters.AddWithValue("@NumeCategorie", NumeCategorie);
                        myCommand.Parameters.AddWithValue("@DescriereCategorie", DescriereCategorie);

                        myCommand.ExecuteNonQuery();
                    }
                    myCon.Close();
                }

                return new JsonResult("Updated successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error updating categorii produse: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("DeleteCategorie/{id}")]

        public JsonResult DeleteCategorie(int id)
        {
            string query = "DELETE FROM dbo.CategoriiProduse " +
                "           WHERE ID_Categorie = @id";
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
    }
}
