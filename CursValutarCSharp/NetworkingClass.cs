using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;

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

        public static List<CurrencyRate> ParseData(string data)
        {
            XElement xelement = XElement.Load(@"C:\Users\alexclp\Desktop\xml.txt");
            IEnumerable<XElement> rates = xelement.Elements();

            List<CurrencyRate> list = new List<CurrencyRate> { };

            foreach (var rate in rates)
            {
                string attribute = rate.FirstAttribute.ToString();

                string value = rate.Value;
                string name = attribute.Substring(10);

                CurrencyRate currentRate = new CurrencyRate();
                currentRate.currencyValue = value;
                currentRate.currencyName = name;

                list.Add(currentRate);
            }
            return list;
        }
    }
}
