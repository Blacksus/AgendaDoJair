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
    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private Dictionary<RepositoryBase, IDbConnection> _repositories = new Dictionary<RepositoryBase, IDbConnection>();
        private bool _disposed;

        public UnitOfWorkTransaction(IDbConnection connection, params IRepository[] repositories)
        {
            _connection = connection;
            ConfigureConnections(repositories);
        }

        private void ConfigureConnections(IRepository[] repositories)
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            foreach (var repo in repositories)
            {
                if (!(repo is RepositoryBase repoBase))
                    throw new InvalidOperationException("Repositório inválido!");

                _repositories.Add(repoBase, repoBase.Connection);
                repoBase.SetTransaction(_transaction);
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        private void resetRepositories()
        {
            foreach (var repo in _repositories)
            {
                repo.Key.SetConnection(repo.Value);
            }
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
                resetRepositories();
            }
        }

        public void Roolback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        ~UnitOfWorkTransaction()
        {
            dispose(false);
            resetRepositories();
        }
    }
}
