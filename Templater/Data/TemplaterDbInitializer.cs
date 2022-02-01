using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teplater.SQLite.Context;

namespace Templater.Data
{
    public class TemplaterDbInitializer
    {
        private TeplaterSQLDB _db;
        public TemplaterDbInitializer(TeplaterSQLDB db)
        {
            _db = db;
        }

        public void Initialize()
        {

        }
    }
}
