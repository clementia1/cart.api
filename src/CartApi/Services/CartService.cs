using System.Threading.Tasks;
using AutoMapper;
using CartApi.Entities;
using CartApi.Models;
using CartApi.Models.GetProduct;
using CartApi.Providers.Abstractions;
using CartApi.Services.Abstractions;

namespace CartApi.Services
{
    public class CartService : ICartService
    {
        private readonly ICartStoreProvider _cartStoreProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IMapper _mapper;

        public CartService(
            ICartStoreProvider cartStoreProvider,
            IJsonSerializer jsonSerializer,
            IMapper mapper)
        {
            _cartStoreProvider = cartStoreProvider;
            _jsonSerializer = jsonSerializer;
            _mapper = mapper;
        }

        public async Task<bool> Add(int userId, ProductDto product)
        {
            var entity = _mapper.Map<ProductEntity>(product);
            var jsonString = _jsonSerializer.Serialize(entity);
            return await _cartStoreProvider.Add(userId.ToString(), jsonString);
        }

        public async Task<bool> Remove(int userId)
        {
            return await _cartStoreProvider.Remove(userId.ToString());
        }

        public async Task<GetProductResponse> GetByKey(int userId)
        {
            var value = await _cartStoreProvider.Get(userId.ToString());
            var entity = _jsonSerializer.Deserialize<ProductEntity>(value);
            var dto = _mapper.Map<ProductDto>(entity);
            return new GetProductResponse { Product = dto };
        }
    }
}