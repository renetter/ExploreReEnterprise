using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReEnterprise.Core.Interface
{
    /// <summary>
    /// Coordinates the business process transaction.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        void Rollback();
    }
}
