Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraTreeList.Menu
Imports DevExpress.XtraEditors
Imports System.Drawing

Namespace DXSample
	Public Class TreeListColumnMenuExtender
		Private treeList As TreeList
		Private renameEditor As TextEdit
		Private renameColumn As TreeListColumn

		Public Sub New(ByVal treeList As TreeList)
			Me.treeList = treeList
			SetUpRenameEditor(treeList)
		End Sub

		Private Sub SetUpRenameEditor(ByVal treeList As TreeList)
			renameEditor = New TextEdit()
			renameEditor.BackColor = DevExpress.LookAndFeel.LookAndFeelHelper.GetSystemColor(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel, SystemColors.Control)
			renameEditor.Parent = treeList
			renameEditor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
			renameEditor.Properties.ValidateOnEnterKey = True
			renameEditor.Visible = False
		End Sub

		Public Sub EnableRenameColumnOption()
			AddHandler treeList.ShowTreeListMenu, AddressOf OnShowTreeListMenu
			AddHandler renameEditor.Validated, AddressOf OnRenameEditorValidated
		End Sub

		Private Sub OnRenameEditorValidated(ByVal sender As Object, ByVal e As EventArgs)
			renameColumn.Caption = renameEditor.Text
			renameEditor.Visible = False
			renameEditor.EditValue = Nothing
		End Sub

		Private Sub OnShowTreeListMenu(ByVal sender As Object, ByVal e As TreeListMenuEventArgs)
			Dim menu As TreeListColumnMenu = TryCast(e.Menu, TreeListColumnMenu)
			If menu Is Nothing Then
				Return
			End If
			Dim item As New DXMenuItem("Rename Column", AddressOf OnRenameColumn)
			renameColumn = menu.Column
			menu.Items.Add(item)
		End Sub

		Private Sub OnRenameColumn(ByVal sender As Object, ByVal e As EventArgs)
			UpdateRenameEditor(renameColumn)
		End Sub

		Private Sub UpdateRenameEditor(ByVal column As TreeListColumn)
			Dim rect As Rectangle = treeList.ViewInfo.ColumnsInfo(column).CaptionRect
			rect.Y -= 2
			If column.Caption = String.Empty Then
				renameEditor.EditValue = column.FieldName
			Else
				renameEditor.EditValue = column.Caption
			End If
			renameEditor.Bounds = rect
			renameEditor.Visible = True
			renameEditor.Select()
			renameEditor.SelectedText = renameEditor.Text
		End Sub

		Public Sub DisableRenameColumnOption()
			RemoveHandler treeList.ShowTreeListMenu, AddressOf OnShowTreeListMenu
			AddHandler renameEditor.Validated, AddressOf OnRenameEditorValidated
		End Sub
	End Class
End Namespace