using System;

namespace ReEnterprise.Infrastructure.Service.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class EnableLoggingAttribute : System.Attribute
    {
    }

}