using System.Threading.Tasks;
using CartApi.Models;
using CartApi.Models.GetProduct;

namespace CartApi.Services.Abstractions
{
    public interface ICartService
    {
        Task<bool> Add(int userId, ProductDto product);
        Task<GetProductResponse> GetByKey(int userId);
    }
}