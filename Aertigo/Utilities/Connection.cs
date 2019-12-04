using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Aertigo.Utilities
{
    public class Connection
    {
        public string ConnectionString()
        {
            string connection = ConfigurationManager.ConnectionStrings["aertigo"].ConnectionString;
            return connection;
        }
    }
}