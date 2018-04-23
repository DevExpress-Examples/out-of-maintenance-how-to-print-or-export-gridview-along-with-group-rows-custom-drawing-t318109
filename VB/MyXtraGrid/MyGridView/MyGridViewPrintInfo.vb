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
Imports DevExpress.XtraGrid.Views.Printing
Imports DevExpress.XtraPrinting
Imports System.Drawing
Imports DevExpress.Data
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base

Namespace MyXtraGrid
	Public Class MyGridViewPrintInfo
		Inherits GridViewPrintInfo

		Public Sub New(ByVal args As DevExpress.XtraGrid.Views.Printing.PrintInfoArgs)
			MyBase.New(args)
		End Sub
		Private Shadows ReadOnly Property View() As MyGridView
			Get
				Return TryCast(MyBase.View, MyGridView)
			End Get
		End Property

		Public Overrides Sub PrintRows(ByVal graph As DevExpress.XtraPrinting.IBrickGraphics)
			MyBase.PrintRows(graph)
			TryCast(View.GetViewInfo(), MyGridViewInfo).IsDataDirty = True
		End Sub

		Protected Overrides Sub PrintRow(ByVal g As Graphics, ByVal graph As IBrickGraphics, ByVal rowHandle As Integer, ByVal level As Integer)
			graphics = g
			_graph = graph
			MyBase.PrintRow(g, graph, rowHandle, level)

		End Sub
		Private graphics As Graphics
		Private _graph As IBrickGraphics

		Protected Overrides Sub PrintGroupRow(ByVal rowHandle As Integer, ByVal level As Integer)
			Dim r As Rectangle = Rectangle.Empty
			r.X = Indent + level * ViewViewInfo.LevelIndent
			r.Width = Me.fMaxRowWidth - r.Left
			r.Y = Y
			r.Height = CurrentRowHeight
			Dim textWidth As Integer = Nothing

			Dim ib As ImageBrick
			Dim bmp As New Bitmap(r.Width, r.Height)

			Dim cache As New GraphicsCache(System.Drawing.Graphics.FromImage(bmp))
			Dim args As RowObjectCustomDrawEventArgs = View.GetRowObjectCustomDrawEventArgs(cache, rowHandle, r, textWidth)
			If args Is Nothing Then
				MyBase.PrintGroupRow(rowHandle, level)
				Return
			End If
			ib = New ImageBrick()
			ib.Rect = r
			ib.Image = bmp
			ib.BackColor = Color.Transparent
			_graph.DrawBrick(ib, ib.Rect)


			r.Width = textWidth
		  '  if (View.OptionsPrint.UsePrintStyles) SetDefaultBrickStyle(Graph, Bricks["GroupRow"]);
		  '  else
			If True Then
				Dim rowAppearance As AppearanceObject = View.GetLevelStyle(level, True)
				Dim tempStyle As New BrickStyle(Bricks("GroupRow").Sides, Bricks("GroupRow").BorderWidth, Color.Transparent, rowAppearance.BackColor, rowAppearance.ForeColor, rowAppearance.Font, Bricks("GroupRow").StringFormat) 'rowAppearance.BorderColor
				tempStyle.TextAlignment = DevExpress.XtraPrinting.Native.TextAlignmentConverter.ToTextAlignment(rowAppearance.TextOptions.HAlignment, rowAppearance.TextOptions.VAlignment)
				SetDefaultBrickStyle(Graph, tempStyle)
			End If
			Dim groupText As String = View.GetGroupRowDisplayText(rowHandle)

			Dim textBrick As ITextBrick = DrawTextBrick(Graph, groupText, r, True)
			textBrick.Text = textBrick.Text.Replace(Environment.NewLine, "")
			textBrick.BackColor = Color.Transparent
			textBrick.TextValue = View.GetGroupRowPrintValue(rowHandle)


			Y += r.Height
		End Sub
	End Class
End Namespace
