using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using SQLite;
using XFormsCatatanKita.UWP;
using Windows.Storage;
using System.IO;

[assembly: Dependency(typeof(DatabaseConnection_UWP))]
namespace XFormsCatatanKita.UWP
{
    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "CatatanDb.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}
