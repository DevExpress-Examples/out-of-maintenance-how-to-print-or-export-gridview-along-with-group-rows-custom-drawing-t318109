using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyXtraGrid
{
    public static class DataHelper
    {
        public static DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Rating", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            int j = 0;
            for (int i = 0; i < RowCount; i++)
            {
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i % 3), j, 3 - i, DateTime.Now.AddDays(i) });
                if (j++ > 5)
                    j = 0;
            }
            return tbl;
        }
    }
}
