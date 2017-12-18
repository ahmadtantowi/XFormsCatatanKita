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

        public CatatansDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Catatan>();
            this.Catatans = new ObservableCollection<Catatan>(database.Table<Catatan>());

            // if the table is empty, initialize the collection
            if (!database.Table<Catatan>().Any())
            {
                AddNewCatatan();
            }
        }

        public void AddNewCatatan()
        {
            this.Catatans.Add(new Catatan
            {
                Name = "",
                Title = "",
                Content = ""
            });
        }

        // Use LINQ to query and filter data
        public IEnumerable<Catatan> GetFilteredCatatansLinq(string name)
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from note in database.Table<Catatan>()
                            where note.Name == name
                            select note;
                return query.AsEnumerable();
            }
        }

        // Use SQL queries against data
        public IEnumerable<Catatan> GetFilteredCatatansSql(string name)
        {
            lock (collisionLock)
            {
                return database.
                  Query<Catatan>
                  ("SELECT * FROM Item WHERE Country = ' " + name + "'").AsEnumerable();
            }
        }

        public Catatan GetCatatan(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Catatan>().
                  FirstOrDefault(catatan => catatan.Id == id);
            }
        }

        public int SaveCatatan(Catatan catatanInstance)
        {
            lock (collisionLock)
            {
                if (catatanInstance.Id != 0)
                {
                    database.Update(catatanInstance);
                    return catatanInstance.Id;
                }
                else
                {
                    database.Insert(catatanInstance);
                    return catatanInstance.Id;
                }
            }
        }

        public void SaveAllCatatans()
        {
            lock (collisionLock)
            {
                foreach (var catatanInstance in this.Catatans)
                {
                    if (catatanInstance.Id != 0)
                    {
                        database.Update(catatanInstance);
                    }
                    else
                    {
                        database.Insert(catatanInstance);
                    }
                }
            }
        }

        public int DeleteCatatan(Catatan catatanInstance)
        {
            var id = catatanInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Catatan>(id);
                }
            }
            this.Catatans.Remove(catatanInstance);
            return id;
        }

        public void DeleteAllCatatans()
        {
            lock (collisionLock)
            {
                database.DropTable<Catatan>();
                database.CreateTable<Catatan>();
            }
            this.Catatans = null;
            this.Catatans = new ObservableCollection<Catatan>
              (database.Table<Catatan>());
        }
    }
}
