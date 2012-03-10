using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace ReEnterprise.Infrastructure.NHibernate.Interface
{
    public interface ISessionFactoryManager
    {
        ISessionFactory GetSessionFactory();
        ISession GetSession();
    }
}
