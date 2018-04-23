Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text

Namespace MyXtraGrid
	Public Class MyGridViewInfo
		Inherits GridViewInfo

		Public Sub New(ByVal gridView As DevExpress.XtraGrid.Views.Grid.GridView)
			MyBase.New(gridView)
		End Sub

		Public Function CalcGroupRowTextWidth(ByVal args As RowObjectCustomDrawEventArgs) As Integer
			Dim rowInfo As GridGroupRowInfo = TryCast(args.Info, GridGroupRowInfo)
			If rowInfo Is Nothing Then
				Return 0
			End If
			Return CInt(rowInfo.Paint.CalcTextSize(args.Graphics, rowInfo.GroupText, rowInfo.Appearance.Font, New StringFormat(), 0).Width)
		End Function
	End Class
End Namespace
