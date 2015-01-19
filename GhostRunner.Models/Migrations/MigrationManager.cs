using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models.Migrations
{
    public class MigrationManager
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void AddMigrations(SQLiteConnection connection, String currentVersion)
        {
            _log.Debug("Setting up the migrations");

            List<IMigration> migrations = new List<IMigration>();

            migrations.Add(new Migration_0010000(connection));
            migrations.Add(new Migration_0010100(connection));

            _log.Debug("Checking if we have the current version");

            if (migrations.Last().Version != currentVersion)
            {
                if (!String.IsNullOrEmpty(currentVersion))
                {
                    _log.Debug("Checking the version to migrate to");

                    Boolean foundCurrentVersion = false;

                    do
                    {
                        if (migrations.First().Version == currentVersion) foundCurrentVersion = true;

                        migrations.RemoveAt(0);
                    }
                    while ((!foundCurrentVersion) && (migrations.Count() > 0));
                }
                else _log.Debug("No versions have been implemented yet");

                foreach (IMigration migration in migrations)
                {
                    migration.Up();
                    migration.UpdateVersion();
                }
            }
            else _log.Debug("We have latest version");
        }
    }
}
