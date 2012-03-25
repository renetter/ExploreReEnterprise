using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using NHibernate;
using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using System.Reflection;

namespace ReEnterprise.Infrastructure.NHibernate
{
    public class SessionFactoryManager : ISessionFactoryManager
    {
        private static object _syncObject = new object();
        private static ISessionFactory _sessionFactory;
        private ISession _session;

        public ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                lock (_syncObject)
                {
                    if (_sessionFactory == null)
                    {
                        var configuration = new Configuration();
                        configuration.Configure();

                        _sessionFactory = Fluently.Configure(configuration)
                                            .Mappings(x => x.FluentMappings.AddFromAssemblyOf<SessionFactoryManager>())
                                            .BuildSessionFactory();
                    }
                }
            }

            return _sessionFactory;
        }

        public ISession GetSession()
        {
            if (_session == null)
            {
                _session = GetSessionFactory().OpenSession();
            }

            return _session;
        }
    }
}
