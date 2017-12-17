using System;
using System.Collections.Generic;
using System.Text;

namespace XFormsCatatanKita
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
