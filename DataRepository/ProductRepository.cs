using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using DataRepository.Model;

namespace DataRepository
{
    public class ProductRepository:IRepository<Product>
    {
        private readonly IDbConnection _connection;

        public ProductRepository()
        {
            _connection = new SqlConnection(Properties.Settings.Default.DbConnection);
        }

        public IEnumerable<Product> Get()
        {
            return _connection.GetAll<Product>();
        }

        public Product GetById(int id)
        {
            return _connection.Get<Product>(id);
        }
    }
}
