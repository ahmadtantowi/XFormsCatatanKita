using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace XFormsCatatanKita
{
    class CatatansDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Catatan> Catatans { get; set; }
    }
}
