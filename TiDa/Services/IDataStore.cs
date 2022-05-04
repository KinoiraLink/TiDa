using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TiDa.Services
{
    public interface IDataStore<T>
    {
        Task InitializeAsync();
        bool IsInitialized();
        Task<bool> AddItemAsync(T item);
        Task InserItemAsync(T item);

        Task InserWebAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);

        Task InsertorReplace(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task<IList<T>> GetAllItemsAsync();
        Task InserAllItem(IList<T> list);
    }
}
