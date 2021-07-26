using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartBffController : ControllerBase
    {
        private readonly ILogger<CartBffController> _logger;

        public CartBffController(ILogger<CartBffController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
        }
    }
}