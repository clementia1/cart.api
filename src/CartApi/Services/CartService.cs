using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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
            var key = GetKey(userId);
            var cartString = await _cartStoreProvider.Get(userId.ToString());
            var entity = _mapper.Map<ProductEntity>(product);

            if (cartString is null)
            {
                var newCart = new HashSet<ProductEntity> { entity };
                return await _cartStoreProvider.Add(key, _jsonSerializer.Serialize(newCart));
            }

            var existingCart = _jsonSerializer.Deserialize<HashSet<ProductEntity>>(cartString);
            var cartItem = existingCart!.FirstOrDefault(i => i.Id == entity.Id);
            
            if (cartItem != null)
            {
                cartItem.Amount++;
                return await _cartStoreProvider.Add(key, _jsonSerializer.Serialize(existingCart));
            }
            
            existingCart!.Add(entity);
            return await _cartStoreProvider.Add(key, _jsonSerializer.Serialize(existingCart));
        }

        public async Task<bool> Remove(int userId)
        {
            return await _cartStoreProvider.Remove(userId.ToString());
        }

        public async Task<IReadOnlyCollection<ProductDto>?> Get(int userId)
        {
            var value = await _cartStoreProvider.Get(userId.ToString());

            if (value is null) return null;
            
            var entity = _jsonSerializer.Deserialize<IReadOnlyCollection<ProductEntity>>(value);
            var dto = _mapper.Map<IReadOnlyCollection<ProductDto>>(entity);
            return dto;
        }

        private string GetKey(int userId)
        {
            return userId.ToString();
        }
    }
}