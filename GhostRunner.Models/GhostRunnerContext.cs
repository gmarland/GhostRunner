using GhostRunner.Models;
using GhostRunner.Models.Migrations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class GhostRunnerContext : DbContext, IContext
    {
        public GhostRunnerContext(String connectionString)
            : base(new SQLiteConnection(connectionString), contextOwnsConnection: true)
        {
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Sequence> Sequences { get; set; }

        public IDbSet<SequenceScript> SequenceScripts { get; set; }

        public IDbSet<ScheduleDetail> ScheduleDetails { get; set; }

        public IDbSet<Script> Scripts { get; set; }

        public IDbSet<Task> Tasks { get; set; }

        public IDbSet<TaskScript> TaskScripts { get; set; }

        public IDbSet<TaskScriptParameter> TaskScriptParameters { get; set; }

        public IDbSet<Schedule> Schedules { get; set; }

        public IDbSet<ScheduleParameter> ScheduleParameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static void Initialize(String connectionString)
        {
            String[] datasourceParts = connectionString.Split(new char[] { '=' });

            if (datasourceParts.Length == 2)
            {
                List<String> versions = new List<String>();

                if (!File.Exists(datasourceParts[1])) SQLiteConnection.CreateFile(datasourceParts[1]);

                SQLiteConnection connection = new SQLiteConnection(connectionString);
                connection.Open();

                SQLiteCommand schemaCommand = new SQLiteCommand(connection);
                schemaCommand.CommandText = "SELECT version FROM _Schema ORDER BY version";

                try
                {
                    SQLiteDataReader schemaReader = schemaCommand.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(schemaReader);

                    foreach(DataRow row in dataTable.Rows)
                    {
                        versions.Add((String)row["version"]);
                    }
                }
                catch(Exception)
                {
                    SQLiteCommand writeSchemaCommand = new SQLiteCommand(connection);
                    writeSchemaCommand.CommandText = "CREATE TABLE _Schema (version NVARCHAR(250) NOT NULL)";

                    writeSchemaCommand.ExecuteNonQuery();
                }

                String lastMigration = String.Empty;
                if (versions.Count > 0) lastMigration = versions.Last();

                MigrationManager.AddMigrations(connection, lastMigration);
            }
        }
    }
}
