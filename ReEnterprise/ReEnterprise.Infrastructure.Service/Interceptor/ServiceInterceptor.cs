using Castle.DynamicProxy;
using System.Linq;
using NHibernate;
using ReEnterprise.Core.Interface;
using System;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using ReEnterprise.Infrastructure.Service.Attribute;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace ReEnterprise.Infrastructure.Service.Interceptor
{
    public class ServiceInterceptor : IInterceptor
    {
        private ISessionFactoryManager _sessionFactoryManager;
        private ILogger _logger;

        public ServiceInterceptor(ISessionFactoryManager sessionFactoryManager, ILogger logger)
        {
            _sessionFactoryManager = sessionFactoryManager;
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var transactionEnabled = invocation.Method.GetCustomAttributes(typeof(EnableTransactionAttribute), false).Any();
            var loggingEnabled = invocation.Method.GetCustomAttributes(typeof(EnableLoggingAttribute), false).Any();

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
            catch(Exception e)
            {
                if (loggingEnabled)
                {
                    _logger.WriteLog(e.Message);
                }
            }

        }
    }


}