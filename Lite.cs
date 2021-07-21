using CommonLib;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite
{


    class Lite
    {
        private object id = 1;
        private string query = string.Empty;

        private List<Part> listParam = new List<Part>();

        private DataTable dt = new DataTable();

        public Lite()
        {

        }

        public void Search()
        {
            string table_name = "db_list";
            string query = " SELECT id,name FROM " + table_name;
            DataTable dt = ExecuteQuery(query);
            if (dt == null)
            {
                //Console.WriteLine("Select Error");
                return;
            }
            Console.WriteLine(string.Format("TABLE_NAME : {0}", dt.TableName));

            foreach (DataRow item in dt.Rows)
            {
                string lines = string.Empty;
                foreach (DataColumn col in dt.Columns)
                    lines += item[col] + "|";

                Console.WriteLine(lines.Substring(0, lines.Length - 1));
            }
        }

        private DataTable ExecuteQuery(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                string connStr = string.Format("Data Source={0}\\mydb.db", Logger.SolutionPath);

                using (var conn = new SqliteConnection(connStr))
                {
                    conn.Open();
                    var commend = new SqliteCommand(query, conn);
                    var rdr = commend.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(rdr);
                    rdr.Close();
                    conn.Close();
                    return dataTable;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }


        }

        public DataTable doSQLiteQuery(string cmd, string query, string[] data)
        {
            string connStr = string.Format("Data Source={0}\\mydb.db", "D:\\workspace\\database\\sqlite");
            DataTable dataTable = new DataTable();
            using (var connection = new SqliteConnection(connStr))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = query;

                if (cmd.ToUpper().Contains("INSERT"))
                {
                    string[] values = query.Split("values")[1].Split(',');

                    for (int i = 0; i < values.Length; i++)
                    {
                        command.Parameters.AddWithValue(values[i].Replace("(", "").Replace(")","").Trim(), data[i]);
                    }

                    SqliteDataReader rdr = command.ExecuteReader();
                    //dataTable.Load(rdr);
                }
                else if(cmd.ToUpper().Contains("SELECT"))
                {
                    // command.CommandText =
                    // @"
                    //     SELECT name
                    //     FROM member
                    //     WHERE id = $id
                    // ";
                    //command.Parameters.AddWithValue("$id", id);

                    //foreach (var item in values)
                    //{
                    //    command.Parameters.AddWithValue("%" + item, item);
                    //}

                    // command.ExecuteNonQuery();
                    SqliteDataReader rdr = command.ExecuteReader();
                    dataTable.Load(rdr);
                }
            }

            return dataTable;
        }
    }

    public class Part : IEquatable<Part>
    {
        public string PartName { get; set; }
        public int PartId { get; set; }

        public bool Equals(Part other)
        {
            if (other == null) return false;
            return (this.PartId.Equals(other.PartId));
        }
    }
}
