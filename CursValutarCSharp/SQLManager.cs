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

                    using (SqlCeCommand com = new SqlCeCommand("INSERT INTO Rates (name, value) VALUES (@name, @value)", con))
                    {
                        com.Parameters.Add("@name", SqlDbType.VarChar);
                        com.Parameters["@name"].Value = name;

                        com.Parameters.Add("@value", SqlDbType.VarChar);
                        com.Parameters["@value"].Value = value;

                        Console.WriteLine("command = {0}", com.CommandText);

                        com.ExecuteNonQuery();
                    }
                }

                con.Close();
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
            }

            return toReturn;
        }
    }
}
