using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;

namespace TiDa.Services
{
    public interface IWebService<T>
    {
        Task<T> LgoinAsync(T item);

        Task<T> RegisterAsync(T item);

        


    }
}
