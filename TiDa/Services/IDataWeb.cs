using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TiDa.Services
{
    public interface IDataWeb<T>
    {


        Task<IList<T>> UploadAsync(IList<T> item);

    }
}
