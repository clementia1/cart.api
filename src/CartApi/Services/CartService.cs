using System.Threading.Tasks;
using AutoMapper;
using CartApi.Entities;
using CartApi.Models;
using CartApi.Providers.Abstractions;
using CartApi.Services.Abstractions;

namespace CartApi.Services
{
    public class CartService : ICartService
    {
        private readonly ICarStoreProvider _carStoreProvider;
        private readonly IMapper _mapper;

        public CartService(
            ICarStoreProvider carStoreProvider,
            IMapper mapper)
        {
            _carStoreProvider = carStoreProvider;
            _mapper = mapper;
        }

        public async Task<bool> Add(int userId, ProductDto product)
        {
            var entity = _mapper.Map<ProductEntity>(product);
            return await _carStoreProvider.Add(userId.ToString(), entity);
        }
    }
}