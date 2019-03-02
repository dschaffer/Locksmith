using Sitecore.Data;
using System;
using System.Linq;

namespace Locksmith
{
    public static class Dbs
    {
        private static Database _db { get; set; }
        public static Database Db
        {
            get
            {
                if (_db != null)
                    return _db;
                else if (Sitecore.Context.ContentDatabase != null)
                    _db = Sitecore.Context.ContentDatabase;
                else if (Sitecore.Context.Database != null)
                    _db = Sitecore.Context.Database;
                else if (Sitecore.Configuration.Factory.GetDatabaseNames().Contains("web"))
                    _db = Sitecore.Configuration.Factory.GetDatabase("web");
                else if (Sitecore.Configuration.Factory.GetDatabaseNames().Contains("master"))
                    _db = Sitecore.Configuration.Factory.GetDatabase("master");

                return _db;
            }
        }
    }
}