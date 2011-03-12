using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace QueryObjects
{
    public class SessionFactoryFactory
    {
        public ISessionFactory Build()
        {
            var connectionString = "";
            return Fluently.Configure()
                .Database(OracleClientConfiguration.Oracle10.ConnectionString(x => x.Is(connectionString)))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<SessionFactoryFactory>())
                .BuildSessionFactory();
        }
    }
}
