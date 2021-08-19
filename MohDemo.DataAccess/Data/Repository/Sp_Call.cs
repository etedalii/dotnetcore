using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MohDemo.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MohDemo.DataAccess.Data.Repository
{
    public class Sp_Call : ISp_Call
    {
        readonly ApplicationDbContext _db;
        readonly string connectionString;
        public Sp_Call(ApplicationDbContext db)
        {
            _db = db;
            connectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        public void ExecuteWithoutReturn<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
