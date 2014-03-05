using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CursValutarCSharp
{
    class NetworkingClass
    {
        public static string RequestData()
        {
            WebRequest webRequest = WebRequest.Create("http://www.bnr.ro/nbrfxrates.xml");
            WebResponse response = webRequest.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            //Console.WriteLine(responseFromServer);
            reader.Close();
            response.Close();

            return responseFromServer;
        }

        public static void ParseData(string data)
        {
            XDocument xdoc = XDocument.Load(data);

            List <CurrencyRate> rates = (from rate in xdoc.Element("DataSet").Element("Body").Element("Cube").Elements("Rate")
                                         select new CurrencyRate
                                         {
                                             currencyValue = rate.Value,
                                             currencyName = rate.FirstAttribute.ToString()
                                         }
                                             
                                         ).ToList();

            foreach (CurrencyRate rate in rates)
            {
                Console.WriteLine("{0}", rate.currencyName);
            }
        }
    }
}
