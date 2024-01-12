    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    namespace ComertElectronic.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PostariController : ControllerBase
        {
            private IConfiguration _configuration;

            public PostariController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet]
            [Route("GetPostari")]
            public JsonResult GetPostari()
            {
                string query = "SELECT * FROM dbo.Postari";
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
            [Route("GetPostareById/{id}")]
            public JsonResult GetPostareById(int id)
            {
                string query = $"SELECT P.*, U.Username, U.ImagineProfil FROM Postari P JOIN Utilizatori U ON P.ID_Utilizator = U.ID_Utilizator WHERE P.ID_Postare = {id}";
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
            [Route("GetPostariBySubiect/{id}")]
            public JsonResult GetPostariBySubiect(int id)
            {
                string query = $"SELECT P.*, U.Username, U.ImagineProfil FROM Postari P JOIN Utilizatori U ON P.ID_Utilizator = U.ID_Utilizator WHERE P.ID_Subiect = {id} ORDER BY P.DataPostarii DESC";
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
            [Route("GetAuthor/{id}")]
            public JsonResult GetAuthor(int id)
            {
                string query = $"SELECT U.Username From Utilizatori U INNER JOIN Postari P On U.ID_Utilizator = P.ID_Utilizator WHERE P.ID_Postare = {id}";
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
        [Route("AddPostare")]

        public JsonResult AddPostare([FromForm] int ID_Subiect, [FromForm] int ID_Utilizator, [FromForm] string TitluPostare, [FromForm] string ContinutPostare, [FromForm] DateTime DataPostarii)
        {
            string query = "INSERT INTO dbo.Postari (ID_Subiect, ID_Utilizator, TitluPostare, ContinutPostare, DataPostarii) VALUES (@ID_Subiect, @ID_Utilizator, @TitluPostare, @ContinutPostare, @DataPostarii)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ComertElectronicDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Subiect", ID_Subiect);
                    myCommand.Parameters.AddWithValue("@ID_Utilizator", ID_Utilizator);
                    myCommand.Parameters.AddWithValue("@TitluPostare", TitluPostare);
                    myCommand.Parameters.AddWithValue("@ContinutPostare", ContinutPostare);
                    myCommand.Parameters.AddWithValue("@DataPostarii", DataPostarii);
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
