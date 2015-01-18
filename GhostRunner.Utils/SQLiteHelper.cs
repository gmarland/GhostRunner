using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Utils
{
    public class SQLiteHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Public Methods

        public static DataTable GetDataTable(String databaseLocation, String sql)
        {
            SQLiteConnection connection = null;
            SQLiteCommand mycommand = null;
            SQLiteDataReader reader = null;

            DataTable dataTable = new DataTable();

            try
            {
                connection = new SQLiteConnection("Data Source=" + databaseLocation);
                connection.Open();

                mycommand = new SQLiteCommand(connection);
                mycommand.CommandText = sql;

                reader = mycommand.ExecuteReader();
                
                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                _log.Error("GetDataTable(" + databaseLocation + "): Error retrieving data table", ex);

                _log.Debug("SQL Query: " + sql);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }

            return dataTable;
        }

        public static int WriteToDatabase(String databaseLocation, String sql)
        {
            SQLiteConnection connection = null;
            SQLiteCommand command = null;

            int rowsUpdated = 0;

            try
            {
                connection = new SQLiteConnection("Data Source=" + databaseLocation);
                connection.Open();

                command = new SQLiteCommand(connection);
                command.CommandText = sql;

                rowsUpdated = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _log.Error("WriteToDatabase(" + databaseLocation + "): Error writing to database", ex);

                _log.Debug("SQL Query: " + sql);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return rowsUpdated;
        }

        #endregion
    }
}
