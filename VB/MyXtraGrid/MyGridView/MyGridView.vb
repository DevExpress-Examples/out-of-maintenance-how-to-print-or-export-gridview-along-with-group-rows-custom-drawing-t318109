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
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid.Drawing
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace MyXtraGrid
	<System.ComponentModel.DesignerCategory("")>
	Public Class MyGridView
		Inherits GridView

		Public Sub New()
			Me.New(Nothing)
		End Sub
		Public Sub New(ByVal grid As DevExpress.XtraGrid.GridControl)
			MyBase.New(grid)
		End Sub
		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property

		Protected Overrides Function CreatePrintInfoInstance(ByVal args As DevExpress.XtraGrid.Views.Printing.PrintInfoArgs) As DevExpress.XtraGrid.Views.Printing.BaseViewPrintInfo
			Return New MyGridViewPrintInfo(args)
		End Function

		Friend Shadows Function GetLevelStyle(ByVal level As Integer, ByVal groupRow As Boolean) As AppearanceObject
			Return MyBase.GetLevelStyle(level, groupRow)
		End Function

		Public Function GetRowObjectCustomDrawEventArgs(ByVal cache As GraphicsCache, ByVal rowHandle As Integer, ByVal rect As Rectangle, <System.Runtime.InteropServices.Out()> ByRef textWidth As Integer) As RowObjectCustomDrawEventArgs
			Dim ri As GridRowInfo = CType(ViewInfo.RowsInfo.FindRow(rowHandle), GridRowInfo)
			If ri Is Nothing Then
				textWidth = 0
				Return Nothing
			End If
			ri.DataBounds = New Rectangle(0, 0, rect.Width, rect.Height)
			Dim args As New RowObjectCustomDrawEventArgs(cache, rowHandle, ElPainter.GroupRow, ri, ri.Appearance)
			textWidth = (TryCast(ViewInfo, MyGridViewInfo)).CalcGroupRowTextWidth(args)
			args.Handled = True

			RaiseCustomDrawGroupRow(args)
			Return args
		End Function

'INSTANT VB NOTE: The variable elPainter was renamed since Visual Basic does not allow variables and other class members to have the same name:
		Private elPainter_Renamed As GridElementsPainter = Nothing
		Private ReadOnly Property ElPainter() As GridElementsPainter
			Get
				If elPainter_Renamed Is Nothing Then
					elPainter_Renamed = (TryCast(PaintStyle, GridPaintStyle)).CreateElementsPainter(Me)
				End If
				Return elPainter_Renamed
			End Get
		End Property
	End Class
End Namespace