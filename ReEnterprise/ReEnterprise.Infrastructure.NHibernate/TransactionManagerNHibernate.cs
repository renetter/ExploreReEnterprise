using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Core.Interface;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using NHibernate;

namespace ReEnterprise.Infrastructure.NHibernate
{
    public class TransactionManagerNHibernate: ITransactionManager
    {
        private ISessionFactoryManager _sessionFactoryManager;
        private ITransaction _transaction;

        public TransactionManagerNHibernate(ISessionFactoryManager sessionFactoryManager)
        {
            _sessionFactoryManager = sessionFactoryManager;
        }

        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _sessionFactoryManager.GetSession().BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction must be started before it can be committed.");
            }

            _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction must be started before it can be rolled back.");
            }

            _transaction.Rollback();
        }
    }
}
