using System.Collections.Generic;
using System.Threading.Tasks;
using CartApi.Models;
using CartApi.Models.GetProduct;

namespace CartApi.Services.Abstractions
{
    public interface ICartService
    {
        Task<bool> Add(int userId, ProductDto product);
        Task<IReadOnlyCollection<ProductDto>?> Get(int userId);
        Task<bool> Remove(int userId);
    }
}