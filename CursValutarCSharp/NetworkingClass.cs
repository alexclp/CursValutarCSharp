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
            XNamespace ns = "http://www.bnr.ro/xsd";
            var rates = XDocument.Load("http://www.bnr.ro/nbrfxrates.xml")
                        .Descendants(ns + "Rate")
                        .Select(r => new
                        {
                            Currency = r.Attribute("currency").Value,
                            Value = (decimal)r,
                            Multiplier = (int?)r.Attribute("multiplier")
                        })
                        .ToList();

        }
    }
}
