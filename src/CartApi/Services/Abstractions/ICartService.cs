using System.Threading.Tasks;
using CartApi.Models;

namespace CartApi.Services.Abstractions
{
    public interface ICartService
    {
        Task<bool> Add(int userId, ProductDto product);
    }
}