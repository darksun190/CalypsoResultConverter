using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace CalypsoResultConverter
{
    public class TableFileData : DataTable
    {

        public TableFileData(FileInfo fi)
        {

            StreamReader sr = new StreamReader(
                new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite),
                Encoding.GetEncoding("GB2312")
                );
            var header = sr.ReadLine().Split("\t".ToArray()).ToList();
            foreach (string columnTitle in header)
            {
                DataColumn dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                if (Columns.Contains(columnTitle))
                {
                    dc.ColumnName = columnTitle + "_2";
                }
                else
                {
                    dc.ColumnName = columnTitle;
                }
                dc.ReadOnly = false;
                dc.Unique = false;
                this.Columns.Add(dc);
            }

            string buf = "";
            while ((buf = sr.ReadLine()) != "END")
            {
                var row = this.NewRow();
                var row_data = buf.Split("\t".ToArray()).ToArray();
                row.ItemArray = row_data;
                this.Rows.Add(row);
            }
        }
        public string this[string s, int i]
        {
            get
            {
                if (!Columns.Contains(s) || Count < i)
                    return null;
                string str = Rows[i][s].ToString();
                if (str.Contains(':'))
                {
                    var dms = str.Split(':');
                    double d = Convert.ToDouble(dms[0]) +
                        Convert.ToDouble(dms[1]) / 60.0 +
                        Convert.ToDouble(dms[1]) / 3600.0;
                    return d.ToString("G4");
                }
                else
                {
                    return str;
                }
            }
        }
        public string this[string s]
        {
            get
            {
                return this[s, 0];
            }
        }
        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }
        public List<string> getColumn(string columnName)
        {

            var result = new List<string>();
            for (int i = 0; i < this.Count; ++i)
            {
                result.Add(this[columnName, i]);
            }
            return result;
        }
    }
}
