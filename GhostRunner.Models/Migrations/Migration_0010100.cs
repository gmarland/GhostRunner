using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models.Migrations
{
    public class Migration_0010100 : IMigration
    {
        private SQLiteConnection _connection;

        public Migration_0010100(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public string Version
        {
            get
            {
                return "001.01.00";
            }
        }

        public void Up()
        {
            CreatePackageCacheTable();
        }

        public void UpdateVersion()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "INSERT INTO _Schema VALUES('" + Version + "');";

            command.ExecuteNonQuery();
        }

        private void CreatePackageCacheTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE PackageCache (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "Name TEXT NOT NULL, " +
                                                "Version TEXT NOT NULL, " +
                                                "Store INTEGER NOT NULL, " +
                                                "Project_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Project_ID) REFERENCES Project(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }
    }
}
