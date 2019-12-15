using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace converter
{
    class Program
    {
        static void Main(string[] args)
        {
            const String connectstr = "Data Source=MYCOMPUTER\\SQLEXPRESS;Initial Catalog=EiwaDB;Integrated Security=True";
            SqlConnection sqlCnt = new SqlConnection(connectstr); ;
            string[] lines = File.ReadAllLines(@"ejdic-hand-utf8.txt");

            int i = 0;
            foreach (string line in lines)
            {
                string[] arr = line.Split('\t');
                SqlCommand cmd = new SqlCommand("InsertProc", sqlCnt);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@word", SqlDbType.NVarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@paraphase", SqlDbType.NText));
                cmd.Parameters["@id"].Value = i;
                cmd.Parameters["@word"].Value = arr[0];
                cmd.Parameters["@paraphase"].Value = arr[1];
                Console.WriteLine(arr[0] + "\t" + arr[1]);
                sqlCnt.Open();
                cmd.ExecuteNonQuery();
                sqlCnt.Close();
                i++;
            }

        }
    }
}
