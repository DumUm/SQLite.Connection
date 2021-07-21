using CommonLib;
using Lite.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite
{
    public class MainProcess
    {
        public MainProcess()
        {
            string writeText = string.Empty;
            while (true)
            {
                writeText = string.Empty;
                writeText = Console.ReadLine();

                if (writeText.ToUpper().Contains("EXIT"))
                {
                    Console.WriteLine("Process Down");
                    return;
                }

                else if (writeText.ToUpper().Contains("DB"))
                {
                    //Logger logger = new Logger();
                    Console.WriteLine(Logger.SolutionPath);

                    Lite lite = new Lite();
                    lite.Search();
                }
                else if (writeText.ToUpper().Contains("CSV"))
                {
                    Console.WriteLine("CSV Read");

                    int euckrCodePage = 51949;
                    Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    Encoding euckr = Encoding.GetEncoding(euckrCodePage);

                    StreamReader sr = new StreamReader(Logger.SolutionPath + "\\country_code.csv", euckr);
                    DataTable csv = new DataTable();
                    csv.Columns.AddRange(new DataColumn[] {
                         new DataColumn("name_kor")
                        ,new DataColumn("name_eng")
                        ,new DataColumn("code")
                        ,new DataColumn("lang_code")
                    });
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        string[] temp = s.Replace("?", "").Split(',');
                        csv.Rows.Add(temp);
                        
                    }
                    Console.WriteLine("Data Row Count: {0}", csv.Rows.Count.ToString());

                    string query = "insert into country values ($name_kor,$name_eng,$code,$lang_code)";
                    Console.WriteLine(query);
                    Lite lite = new Lite();

                    int queryCount = 0;
                    foreach (DataRow row in csv.Rows)
                    {
                        DataTable dt = lite.doSQLiteQuery("insert", query, row.ItemArray.Cast<string>().ToArray());
                        queryCount++;
                    }

                    Console.WriteLine(queryCount);
                }
            }
        }
    }

}
