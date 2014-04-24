using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlServerCe;

namespace CursValutarCSharp
{
    public class SQLManager
    {
        public static void InsertData(List<CurrencyRate> data)
        {
            string connectionString = Properties.Settings.Default.RatesConnectionString;

            using (SqlCeConnection con = new SqlCeConnection(connectionString))
            {
                con.Open();

                foreach (CurrencyRate rate in data)
                {
                    string name = rate.currencyName;
                    string value = rate.currencyValue;

                    name = name.Substring(0, name.Length - 1);

                    string query = String.Format("INSERT INTO Rates (name, value) VALUES ({0}, {1})", "'" + name + "'", "'" + value + "'");
                    SqlCeCommand command = new SqlCeCommand(query, con);

                    //command.Parameters.AddWithValue("@name", name);
                    //command.Parameters.AddWithValue("@value", value);

                    int affectedRows = command.ExecuteNonQuery();
                    Console.WriteLine("affected rows = {0}", affectedRows);
                }
                con.Close();
            }
        }

        public static void InsertValues(string name, string value)
        {
            string connectionString = Properties.Settings.Default.RatesConnectionString;

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                using (SqlCeCommand command = new SqlCeCommand("INSERT INTO Rates (name, value) VALUES (@name, @value)", connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@value", value);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static List<CurrencyRate> GetData()
        {
            string connectionString = Properties.Settings.Default.RatesConnectionString;
            List <CurrencyRate> toReturn = new List<CurrencyRate>();

            using (SqlCeConnection con = new SqlCeConnection(connectionString))
            {
                con.Open();

                using (SqlCeCommand com = new SqlCeCommand("SELECT name, value FROM Rates", con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        toReturn.Add(new CurrencyRate
                        {
                            currencyName = reader.GetString(0),
                            currencyValue = reader.GetString(1)
                        });
                    }
                }
                con.Close();
            }

            return toReturn;
        }
    }
}
