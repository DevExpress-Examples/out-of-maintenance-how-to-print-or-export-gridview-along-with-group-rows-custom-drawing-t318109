Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Text

Namespace MyXtraGrid
	Public Module DataHelper
		Public Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("Rating", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(Date))
			Dim j As Integer = 0
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i Mod 3), j, 3 - i, Date.Now.AddDays(i) })
				Dim tempVar As Boolean = j > 5
				j += 1
				If tempVar Then
					j = 0
				End If
			Next i
			Return tbl
		End Function
	End Module
End Namespace
