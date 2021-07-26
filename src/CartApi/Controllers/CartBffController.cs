using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartApi.Models.AddProduct;
using CartApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartBffController : ControllerBase
    {
        private readonly ILogger<CartBffController> _logger;
        private readonly ICartService _cartService;

        public CartBffController(
            ILogger<CartBffController> logger,
            ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [HttpPost("{userId:int}")]
        public async Task Add(int userId, [FromBody] AddProductRequest request)
        {
            var result = await _cartService.Add(userId, request.Product);
        }
    }
}