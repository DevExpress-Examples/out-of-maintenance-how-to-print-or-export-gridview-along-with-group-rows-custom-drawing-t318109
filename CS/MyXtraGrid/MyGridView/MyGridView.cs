// Developer Express Code Central Example:
// How to customize the GridControl's print output.
// 
// This example demonstrates how to override the default exporting process to take
// into account a custom drawn content provided via the
// GridView.CustomDrawFooterCell Event
// (ms-help://DevExpress.NETv10.1/DevExpress.WindowsForms/DevExpressXtraGridViewsGridGridView_CustomDrawFooterCelltopic.htm)
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2667

using System;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace MyXtraGrid
{
    [System.ComponentModel.DesignerCategory("")]
    public class MyGridView : GridView
    {
        public MyGridView() : this(null) { }
        public MyGridView(DevExpress.XtraGrid.GridControl grid) : base(grid)    {}
        protected override string ViewName { get { return "MyGridView"; } }

        protected override DevExpress.XtraGrid.Views.Printing.BaseViewPrintInfo CreatePrintInfoInstance(DevExpress.XtraGrid.Views.Printing.PrintInfoArgs args)
        {
            return new MyGridViewPrintInfo(args);
        }

        internal new AppearanceObject GetLevelStyle(int level, bool groupRow)
        {
            return base.GetLevelStyle(level, groupRow);
        }

        public RowObjectCustomDrawEventArgs GetRowObjectCustomDrawEventArgs(GraphicsCache cache, int rowHandle, Rectangle rect, out int textWidth)
        {
            GridRowInfo ri = (GridRowInfo)ViewInfo.RowsInfo.FindRow(rowHandle);
            if (ri == null)
            {
                textWidth = 0;
                return null;
            }
            ri.DataBounds = new Rectangle(0, 0, rect.Width, rect.Height);
            RowObjectCustomDrawEventArgs args = new RowObjectCustomDrawEventArgs(cache, rowHandle, ElPainter.GroupRow, ri, ri.Appearance);
            textWidth = (ViewInfo as MyGridViewInfo).CalcGroupRowTextWidth(args);
            args.Handled = true;

            RaiseCustomDrawGroupRow(args);
            return args;
        }

        GridElementsPainter elPainter = null;
        private GridElementsPainter ElPainter
        {
            get
            {
                if (elPainter == null) elPainter = (PaintStyle as GridPaintStyle).CreateElementsPainter(this);
                return elPainter;
            }
        }
    }
}