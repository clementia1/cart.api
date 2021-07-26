using System;
using System.Threading.Tasks;
using CartApi.Entities;

namespace CartApi.Providers.Abstractions
{
    public interface ICartStoreProvider
    {
        Task<bool> Add(string key, string value);
        Task<string> Get(string key);
        Task<bool> Remove(string key);
        Task<bool> Exists(string key);
    }
}