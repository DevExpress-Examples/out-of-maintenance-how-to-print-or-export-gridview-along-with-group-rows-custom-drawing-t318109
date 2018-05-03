using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyXtraGrid
{
    public class MyGridViewInfo : GridViewInfo
    {
        public MyGridViewInfo(DevExpress.XtraGrid.Views.Grid.GridView gridView) : base(gridView) { }

        public int CalcGroupRowTextWidth(RowObjectCustomDrawEventArgs args )
        {
            GridGroupRowInfo rowInfo = args.Info as GridGroupRowInfo;
            if (rowInfo == null)
                return 0;
            return (int)rowInfo.Paint.CalcTextSize(args.Graphics, rowInfo.GroupText, rowInfo.Appearance.Font, new StringFormat(), 0).Width;
        }
    }
}
