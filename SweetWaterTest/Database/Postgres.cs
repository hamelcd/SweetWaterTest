using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SweetWaterTest.Database
{
    public static class Postgres
    {
        private static string _ConnectionString;

        // Lazy Loading
        internal static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_ConnectionString))
                {
                    NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
                    sb.Host = "baasu.db.elephantsql.com";
                    sb.Port = 5432;
                    sb.Username = sb.Database = "zmemjgvb";
                    sb.Password = "Pnto6waG08GGQINu3PcC9wNmu2jtbHaZ";
                    sb.Pooling = true;

                    _ConnectionString = sb.ToString();
                }

                return _ConnectionString;
            }
        }

        public static DataTable SqlToDataTable(NpgsqlCommand Command, out string ErrorMessage)
        {
            DataTable dt = new DataTable();
            ErrorMessage = string.Empty;

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                        da.Dispose();
                    }

                    conn.Close();
                }
            }
            catch (PostgresException pex)
            {
                ErrorMessage = pex.ToString();
            }
            catch (NpgsqlException nex)
            {
                ErrorMessage = nex.ToString();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }

            return dt;
        }
    }
}
