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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;

namespace MyXtraGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitGrid();
        }

        private void InitGrid()
        {
            myGridControl1.DataSource = DataHelper.CreateTable(20);
            myGridView1.GroupCount = 1;
            myGridView1.Columns["Rating"].GroupIndex = 0;
            RepositoryItemRatingControl repository = new RepositoryItemRatingControl();
            myGridView1.Columns["Rating"].ColumnEdit = repository;
            myGridControl1.RepositoryItems.Add(repository);
            myGridView1.CustomDrawGroupRow += gridView1_CustomDrawGroupRow;
        }
        void gridView1_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo rowInfo = e.Info as GridGroupRowInfo;
            if (rowInfo != null && rowInfo.Column.FieldName == "Rating")
            {
                MyGridView view = sender as MyGridView;
                MyGridViewInfo viewInfo = view.GetViewInfo() as MyGridViewInfo;
                int textWidth = viewInfo.CalcGroupRowTextWidth(e);
                RatingControlViewInfo info = rowInfo.Column.ColumnEdit.CreateViewInfo() as RatingControlViewInfo;
                RatingControlPainter painter = rowInfo.Column.ColumnEdit.CreatePainter() as RatingControlPainter;
                info.EditValue = myGridView1.GetGroupRowValue(e.RowHandle);
                info.CalcViewInfo(e.Graphics);
                info.Bounds = new Rectangle(textWidth, rowInfo.DataBounds.Y, info.RatingSize.Width, rowInfo.DataBounds.Height); ;
                info.CalcViewInfo(e.Graphics);
                ControlGraphicsInfoArgs args = new ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(e.Graphics), rowInfo.DataBounds);
                e.DefaultDraw();
                painter.Draw(args);
                args.Cache.Dispose();
                e.Handled = true;
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            myGridControl1.ShowPrintPreview();
        }




    }
}