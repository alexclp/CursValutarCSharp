using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursValutarCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.DataSource = GetCurrencyRatesTable();
        }

        private DataTable GetCurrencyRatesTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Currency".ToString());
            table.Columns.Add("Rate".ToString());

            List<CurrencyRate> list = NetworkingClass.ParseData();

            foreach(CurrencyRate current in list)
            {
                DataRow row = table.NewRow();

                row["Currency"] = current.currencyName;
                row["Rate"] = current.currencyValue;

                table.Rows.Add(row);
            }

            return table;
        }


    }
}
