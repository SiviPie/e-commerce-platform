using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ComertElectronic.Models;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduseController : ControllerBase
    {
        private IConfiguration _configuration;
        public ProduseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetProduse")]

        public JsonResult GetProduse()
        {
            string query = "SELECT * FROM dbo.Produse";
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
        [Route("GetProdusById/{id}")]
        public JsonResult GetProdusById(int id)
        {
            string query = $"SELECT * FROM Produse WHERE ID_Produs = {id}";
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
        [Route("GetProduseByText/{text}")]
        public JsonResult GetProduseByText(string text)
        {
            string query = $"SELECT * FROM Produse WHERE NumeProdus LIKE '%{text}%';";
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
        [Route("GetProduseByCategorie/{id}")]
        public JsonResult GetProduseByCategorie(int id)
        {
            string query = $"SELECT P.* FROM Produse P LEFT JOIN CategoriiProduse CP ON P.ID_Categorie = CP.ID_Categorie WHERE P.ID_Categorie = {id}";
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
        [Route("GetProduseByBon/{id}")]
        public JsonResult GetProduseByBon(int id)
        {
            string query = $"SELECT * FROM Produse " +
                $"WHERE ID_Produs IN (" +
                $" SELECT DB.ID_Produs" +
                $" FROM DetaliiBon DB" +
                $"  JOIN Bonuri B ON DB.ID_Bon = B.ID_Bon" +
                $"  WHERE B.ID_Bon = {id});";
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
        [Route("GetTopDistinctSpecProduct")]

        public JsonResult GetTopDistinctSpecProduct()
        {
            string query = "SELECT Top 1 P.NumeProdus, (SELECT COUNT(DISTINCT PS.ID_Specificatie) FROM ProdusSpecificatii PS WHERE PS.ID_Produs = P.ID_Produs) AS NumarSpecDistincte FROM Produse P ORDER BY NumarSpecDistincte DESC";
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
        [Route("AddProdus")]

        public JsonResult AddProdus([FromForm] string NumeProdus, [FromForm] int ID_Categorie, [FromForm] string CodProdus, [FromForm] string ImagineProdus, [FromForm] string DescriereProdus, [FromForm] float PretProdus, [FromForm] int ID_Reducere)
        {
            Console.WriteLine("aaaa");
            Console.WriteLine($"NumeProdus: {NumeProdus}, ID_Categorie: {ID_Categorie}, CodProdus: {CodProdus}, ImagineProdus: {ImagineProdus}, DescriereProdus: {DescriereProdus}, PretProdus: {PretProdus}, ID_Reducere {ID_Reducere} ");
            string query = "INSERT INTO dbo.Produse (NumeProdus, ID_Categorie, CodProdus, ImagineProdus, DescriereProdus, PretProdus) VALUES (@NumeProdus, @ID_Categorie, @CodProdus, @ImagineProdus, @DescriereProdus, @PretProdus)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NumeProdus", NumeProdus);
                    myCommand.Parameters.AddWithValue("@ID_Categorie", ID_Categorie);
                    myCommand.Parameters.AddWithValue("@CodProdus", CodProdus);
                    myCommand.Parameters.AddWithValue("@ImagineProdus", ImagineProdus);
                    myCommand.Parameters.AddWithValue("@DescriereProdus", DescriereProdus);
                    myCommand.Parameters.AddWithValue("@PretProdus", PretProdus);
                    myCommand.Parameters.AddWithValue("@ID_Reducere", ID_Reducere);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
                myCon.Close();
            }

            return new JsonResult("Added successfully");
        }


        [HttpPut]
        [Route("UpdateProdus/{id}")]
        public JsonResult UpdateProdus(
            int id,
            [FromForm] string NumeProdus,
            [FromForm] int ID_Categorie,
            [FromForm] string CodProdus,
            [FromForm] string ImagineProdus,
            [FromForm] string DescriereProdus,
            [FromForm] float PretProdus,
            [FromForm] int ID_Reducere)
        {
            try
            {
                string query = $"UPDATE dbo.Produse SET NumeProdus = @NumeProdus, ID_Categorie = @ID_Categorie, CodProdus = @CodProdus, ImagineProdus = @ImagineProdus, DescriereProdus = @DescriereProdus, PretProdus = @PretProdus, ID_Reducere = @ID_Reducere WHERE ID_Produs = {id}";

                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");

                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id", id);
                        myCommand.Parameters.AddWithValue("@NumeProdus", NumeProdus);
                        myCommand.Parameters.AddWithValue("@ID_Categorie", ID_Categorie);
                        myCommand.Parameters.AddWithValue("@CodProdus", CodProdus);
                        myCommand.Parameters.AddWithValue("@ImagineProdus", ImagineProdus);
                        myCommand.Parameters.AddWithValue("@DescriereProdus", DescriereProdus);
                        myCommand.Parameters.AddWithValue("@PretProdus", PretProdus);
                        myCommand.Parameters.AddWithValue("@ID_Reducere", ID_Reducere);

                        myCommand.ExecuteNonQuery();
                    }
                    myCon.Close();
                }

                return new JsonResult("Updated successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error updating product: {ex.Message}");
            }
        }



        [HttpDelete]
        [Route("DeleteProdus/{id}")]

        public JsonResult DeleteProdus(int id)
        {
            string query = "DELETE FROM dbo.Produse WHERE ID_Produs = @id";
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
