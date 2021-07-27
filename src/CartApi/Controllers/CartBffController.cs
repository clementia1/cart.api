using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartApi.Models.AddProduct;
using CartApi.Models.DeleteProduct;
using CartApi.Models.GetProduct;
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
        public async Task<IActionResult> Add(int userId, [FromBody] AddProductRequest request)
        {
            var result = await _cartService.Add(userId, request.Product);
            return result 
                ? Ok(new AddProductResponse { Message = "Successfully added" })
                : new JsonResult(new AddProductResponse { Message = "Operation failed"});
        }
        
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> Get(int userId)
        {
            var result = await _cartService.Get(userId);
            return result is null
                ? Ok(new GetProductResponse())
                : Ok(new GetProductResponse {Products = result});
        }
        
        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> Delete(int userId, [FromBody] DeleteProductRequest request)
        {
            var result = await _cartService.Remove(userId, request.ProductId);
            return result 
                ? Ok(new DeleteProductResponse { Message = "Successfully removed" })
                : NotFound(new DeleteProductResponse { Message = "Item not found" });
        }
    }
}