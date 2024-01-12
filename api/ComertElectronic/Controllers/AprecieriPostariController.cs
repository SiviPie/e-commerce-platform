using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ComertElectronic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprecieriPostariController : ControllerBase
    {
        private IConfiguration _configuration;
        public AprecieriPostariController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
