using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using NHibernate;

namespace ReEnterprise.Infrastructure.NHibernate
{
    public class RepositoryBaseNHibernate
    {
        private ISessionFactoryManager _sessionFactoryManager;

        public ISession Session { get { return _sessionFactoryManager.GetSession(); } }

        public RepositoryBaseNHibernate(ISessionFactoryManager sessionFactoryManager)
        {
            _sessionFactoryManager = sessionFactoryManager;
        }
    }
}
