using System;
using System.Linq;
using Castle.DynamicProxy;
using NHibernate;
using ReEnterprise.Core.Interface;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using ReEnterprise.Infrastructure.Service.Attribute;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace ReEnterprise.Infrastructure.Service.Interceptor
{
    public class ServiceInterceptor : IInterceptor
    {
        private readonly ILogger _logger;
        private readonly ISessionFactoryManager _sessionFactoryManager;

        public ServiceInterceptor(ISessionFactoryManager sessionFactoryManager, ILogger logger)
        {
            _sessionFactoryManager = sessionFactoryManager;
            _logger = logger;
        }

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            bool transactionEnabled =
                invocation.Method.GetCustomAttributes(typeof (EnableTransactionAttribute), false).Any();
            bool loggingEnabled = invocation.Method.GetCustomAttributes(typeof (EnableLoggingAttribute), false).Any();

            try
            {
                if (transactionEnabled)
                {
                    ITransaction transaction = _sessionFactoryManager.GetSession().BeginTransaction();

                    invocation.Proceed();

                    transaction.Commit();
                }
                else
                {
                    invocation.Proceed();
                }
            }
            catch (Exception e)
            {
                if (loggingEnabled)
                {
                    _logger.WriteLog(e.Message);
                }
            }
        }

        #endregion
    }
}