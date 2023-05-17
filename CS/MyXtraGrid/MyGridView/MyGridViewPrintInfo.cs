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
using DevExpress.XtraGrid.Views.Printing;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.Data;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;

namespace MyXtraGrid
{
    public class MyGridViewPrintInfo : GridViewPrintInfo
    {

        public MyGridViewPrintInfo(DevExpress.XtraGrid.Views.Printing.PrintInfoArgs args) : base(args) { }
        new MyGridView View
        {
            get { return base.View as MyGridView; }
        }

        public override void PrintRows(BrickGraphics graph)
        {
            base.PrintRows(graph);
            (View.GetViewInfo() as MyGridViewInfo).IsDataDirty = true;
        }
        protected override void PrintRow(GraphicsCache cache, BrickGraphics graph, int rowHandle, int level)
        {
            _graph = graph;
            base.PrintRow(cache, graph, rowHandle, level);
      
        }
        IBrickGraphics _graph;

        protected override void PrintGroupRow(int rowHandle, int level)
        {
            Rectangle r = Rectangle.Empty;
            r.X = Indent + level * ViewViewInfo.LevelIndent;
            r.Width = this.fMaxRowWidth - r.Left;
            r.Y = Y;
            r.Height = CurrentRowHeight;
            int textWidth;

            ImageBrick ib;
            Bitmap bmp = new Bitmap(r.Width, r.Height);

            GraphicsCache cache = new GraphicsCache(Graphics.FromImage(bmp));
            RowObjectCustomDrawEventArgs args = View.GetRowObjectCustomDrawEventArgs(cache, rowHandle, r, out textWidth);
            if(args == null)
            {
                base.PrintGroupRow(rowHandle, level);
                return;
            }
            ib = new ImageBrick();
            ib.Rect = r;
            ib.Image = bmp;
            ib.BackColor = Color.Transparent;
            _graph.DrawBrick(ib, ib.Rect);


            r.Width = textWidth;
          //  if (View.OptionsPrint.UsePrintStyles) SetDefaultBrickStyle(Graph, Bricks["GroupRow"]);
          //  else
            {
                AppearanceObject rowAppearance = View.GetLevelStyle(level, true);
                BrickStyle tempStyle = new BrickStyle(Bricks["GroupRow"].Sides, Bricks["GroupRow"].BorderWidth, Color.Transparent/*rowAppearance.BorderColor*/, rowAppearance.BackColor, rowAppearance.ForeColor, rowAppearance.Font, Bricks["GroupRow"].StringFormat);
                tempStyle.TextAlignment = DevExpress.XtraPrinting.Native.TextAlignmentConverter.ToTextAlignment(rowAppearance.TextOptions.HAlignment, rowAppearance.TextOptions.VAlignment);
                SetDefaultBrickStyle(Graph, tempStyle);
            }
            string groupText = View.GetGroupRowDisplayText(rowHandle); 

            ITextBrick textBrick = DrawTextBrick(Graph, groupText, r, true);
            textBrick.Text = textBrick.Text.Replace(Environment.NewLine, "");
            textBrick.BackColor = Color.Transparent;
            textBrick.TextValue = View.GetGroupRowPrintValue(rowHandle);


            Y += r.Height; 
        }
    }
}
