' Developer Express Code Central Example:
' How to customize the GridControl's print output.
' 
' This example demonstrates how to override the default exporting process to take
' into account a custom drawn content provided via the
' GridView.CustomDrawFooterCell Event
' (ms-help://DevExpress.NETv10.1/DevExpress.WindowsForms/DevExpressXtraGridViewsGridGridView_CustomDrawFooterCelltopic.htm)
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2667

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Repository

Namespace MyXtraGrid
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
			InitGrid()
		End Sub

		Private Sub InitGrid()
			myGridControl1.DataSource = DataHelper.CreateTable(20)
			myGridView1.GroupCount = 1
			myGridView1.Columns("Rating").GroupIndex = 0
			Dim repository As New RepositoryItemRatingControl()
			myGridView1.Columns("Rating").ColumnEdit = repository
			myGridControl1.RepositoryItems.Add(repository)
			AddHandler myGridView1.CustomDrawGroupRow, AddressOf gridView1_CustomDrawGroupRow
		End Sub
		Private Sub gridView1_CustomDrawGroupRow(ByVal sender As Object, ByVal e As RowObjectCustomDrawEventArgs)
			Dim rowInfo As GridGroupRowInfo = TryCast(e.Info, GridGroupRowInfo)
			If rowInfo IsNot Nothing AndAlso rowInfo.Column.FieldName = "Rating" Then
				Dim view As MyGridView = TryCast(sender, MyGridView)
				Dim viewInfo As MyGridViewInfo = TryCast(view.GetViewInfo(), MyGridViewInfo)
				Dim textWidth As Integer = viewInfo.CalcGroupRowTextWidth(e)
				Dim info As RatingControlViewInfo = TryCast(rowInfo.Column.ColumnEdit.CreateViewInfo(), RatingControlViewInfo)
				Dim painter As RatingControlPainter = TryCast(rowInfo.Column.ColumnEdit.CreatePainter(), RatingControlPainter)
				info.EditValue = myGridView1.GetGroupRowValue(e.RowHandle)
				info.CalcViewInfo(e.Graphics)
				info.Bounds = New Rectangle(textWidth, rowInfo.DataBounds.Y, info.RatingSize.Width, rowInfo.DataBounds.Height)

				info.CalcViewInfo(e.Graphics)
				Dim args As New ControlGraphicsInfoArgs(info, New DevExpress.Utils.Drawing.GraphicsCache(e.Graphics), rowInfo.DataBounds)
				e.DefaultDraw()
				painter.Draw(args)
				args.Cache.Dispose()
				e.Handled = True
			End If
		End Sub


		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			myGridControl1.ShowPrintPreview()
		End Sub




	End Class
End Namespace