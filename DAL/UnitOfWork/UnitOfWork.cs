using DAL.Repositories;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        public UnitOfWork(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetSection(RepositoryBase.CONNECTIONSTRING_KEY);
            if (string.IsNullOrEmpty(connectionString.Value))
                throw new ArgumentNullException("String de conexão não encontrada!!!");

            _connection = new SqlConnection(connectionString.Value);
        }
        public IUnitOfWorkTransaction Begin(params IRepository[] repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("Repositórios inválidos!", nameof(repositories));

            return new UnitOfWorkTransaction(_connection, repositories);
        }
    }
}
