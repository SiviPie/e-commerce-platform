using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocProduseController : ControllerBase
    {
        private IConfiguration _configuration;
        public StocProduseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
