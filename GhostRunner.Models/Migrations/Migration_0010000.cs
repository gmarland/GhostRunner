using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models.Migrations
{
    public class Migration_0010000 : IMigration
    {
        private SQLiteConnection _connection;

        public Migration_0010000(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public string Version
        {
            get
            {
                return "001.00.00";
            }
        }

        public void Up()
        {
            CreateUserTable();
            CreateProjectTable();
            CreateScriptTable();
            CreateSequenceTable();
            CreateSequenceScriptTable();
            CreateScheduleTable();
            CreateScheduleDetailTable();
            CreateScheduleParameterTable();
            CreateTaskTable();
            CreateTaskScriptTable();
            CreateTaskScriptParameterTable();
        }

        public void UpdateVersion()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "INSERT INTO _Schema VALUES('" + Version + "');";

            command.ExecuteNonQuery();
        }

        private void CreateUserTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE User (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "SessionId TEXT NULL, " +
                                                "Name TEXT NOT NULL, " +
                                                "Email TEXT NOT NULL, " +
                                                "Password TEXT NOT NULL, " +
                                                "Created TEXT NOT NULL" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateProjectTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE Project (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "Name TEXT NOT NULL, " +
                                                "Created TEXT NOT NULL, " +
                                                "User_ID INTEGER NULL, " +
                                                "FOREIGN KEY(User_ID) REFERENCES User(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateScriptTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE Script (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "Name TEXT NOT NULL, " +
                                                "Description TEXT NULL, " +
                                                "Content TEXT NULL, " +
                                                "Type INTEGER NOT NULL, " +
                                                "Project_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Project_ID) REFERENCES Project(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateSequenceTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE Sequence (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "Name TEXT NOT NULL, " +
                                                "Description TEXT NULL, " +
                                                "Project_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Project_ID) REFERENCES Project(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateSequenceScriptTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE SequenceScript (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "Name TEXT NOT NULL, " +
                                                "Content TEXT NOT NULL, " +
                                                "Type INTEGER NOT NULL, " +
                                                "Position INTEGER NOT NULL, " +
                                                "Sequence_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Sequence_ID) REFERENCES Sequence(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateScheduleTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE Schedule (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "ScheduleType INTEGER NOT NULL, " +
                                                "ScheduleItemId INTEGER NOT NULL, " +
                                                "ScheduleItemType INTEGER NOT NULL, " +
                                                "LastScheduled TEXT NULL, " +
                                                "Project_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Project_ID) REFERENCES Project(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateScheduleDetailTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE ScheduleDetail (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "Name TEXT NOT NULL, " +
                                                "Value TEXT NULL, " +
                                                "Schedule_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Schedule_ID) REFERENCES Schedule(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateScheduleParameterTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE ScheduleParameter (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "Name TEXT NOT NULL, " +
                                                "Value TEXT NULL, " +
                                                "Schedule_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Schedule_ID) REFERENCES Schedule(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateTaskTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE Task (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "ExternalId TEXT NOT NULL, " +
                                                "Name TEXT NOT NULL, " +
                                                "Status INTEGER NOT NULL, " +
                                                "ParentId INTEGER NOT NULL, " +
                                                "ParentType INTEGER NOT NULL, " +
                                                "Created TEXT NOT NULL, " +
                                                "Started TEXT NULL, " +
                                                "Completed TEXT NULL, " +
                                                "Project_ID INTEGER NULL, " +
                                                "User_ID INTEGER NULL, " +
                                                "Script_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Project_ID) REFERENCES Project(ID), " +
                                                "FOREIGN KEY(User_ID) REFERENCES User(ID), " +
                                                "FOREIGN KEY(Script_ID) REFERENCES Script(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateTaskScriptTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE TaskScript (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "Content TEXT NULL, " +
                                                "Type INTEGER NOT NULL, " +
                                                "Log TEXT NULL, " +
                                                "Position INTEGER NOT NULL, " +
                                                "Task_ID INTEGER NULL, " +
                                                "FOREIGN KEY(Task_ID) REFERENCES Task(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }

        private void CreateTaskScriptParameterTable()
        {
            SQLiteCommand command = new SQLiteCommand(_connection);

            command.CommandText = "CREATE TABLE TaskScriptParameter (" +
                                                "ID INTEGER PRIMARY KEY, " +
                                                "Name TEXT NOT NULL, " +
                                                "Value TEXT NULL, " +
                                                "TaskScript_ID INTEGER NULL, " +
                                                "FOREIGN KEY(TaskScript_ID) REFERENCES TaskScript(ID)" +
                                                ")";

            command.ExecuteNonQuery();
        }
    }
}
