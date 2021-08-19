using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohDemo.DataAccess.Data.Repository.IRepository
{
    public interface ISp_Call : IDisposable
    {
        IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null);
        void ExecuteWithoutReturn<T>(string procedureName, DynamicParameters param = null);
        T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null);
    }
}
