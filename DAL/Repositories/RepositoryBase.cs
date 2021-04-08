using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal abstract class RepositoryBase
    {
        internal const string CONNECTIONSTRING_KEY = "ConnectionStrings:DBConnection";

        internal IDbConnection Connection { get; set; }

        public IDbTransaction transaction;

        public RepositoryBase(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetSection(CONNECTIONSTRING_KEY);
            if (string.IsNullOrEmpty(connectionString.Value))
                throw new ArgumentNullException("String de conexão não encontrada!");

            Connection = new SqlConnection(connectionString.Value);
        }

        public void SetTransaction(IDbTransaction transaction)
        {
            this.transaction = transaction;
            Connection = transaction.Connection;
        }

        public void SetConnection(IDbConnection connection)
        {
            this.Connection = connection;
            this.transaction = null;
        }
    }
}
