using System;
using System.Threading.Tasks;
using CartApi.Entities;

namespace CartApi.Providers.Abstractions
{
    public interface ICarStoreProvider
    {
        Task<bool> Add(string key, ProductEntity entity);
        Task<string> Get(string key);
        Task<bool> Remove(string key);
        Task<bool> Exists(string key);
    }
}