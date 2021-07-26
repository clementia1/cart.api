using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CartApi.Models.GetProduct
{
    public class GetProductResponse
    {
        public IReadOnlyCollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}