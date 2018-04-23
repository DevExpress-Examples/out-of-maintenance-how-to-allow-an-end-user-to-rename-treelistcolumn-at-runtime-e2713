using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.Utils.Menu;
using DevExpress.XtraTreeList.Menu;
using DevExpress.XtraEditors;
using System.Drawing;

namespace DXSample {
    public class TreeListColumnMenuExtender {
        TreeList treeList;
        TextEdit renameEditor;
        TreeListColumn renameColumn;

        public TreeListColumnMenuExtender(TreeList treeList) {
            this.treeList = treeList;
            SetUpRenameEditor(treeList);
        }

        private void SetUpRenameEditor(TreeList treeList) {
            renameEditor = new TextEdit();
            renameEditor.BackColor = DevExpress.LookAndFeel.LookAndFeelHelper.GetSystemColor(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel,
                 SystemColors.Control);
            renameEditor.Parent = treeList;
            renameEditor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            renameEditor.Properties.ValidateOnEnterKey = true;
            renameEditor.Visible = false;
        }

        public void EnableRenameColumnOption() {
            treeList.ShowTreeListMenu += OnShowTreeListMenu;
            renameEditor.Validated += OnRenameEditorValidated;
        }

        void OnRenameEditorValidated(object sender, EventArgs e) {
            renameColumn.Caption = renameEditor.Text;
            renameEditor.Visible = false;
            renameEditor.EditValue = null;
        }

        void OnShowTreeListMenu(object sender, TreeListMenuEventArgs e) {
            TreeListColumnMenu menu = e.Menu as TreeListColumnMenu;
            if(menu == null) return;
            DXMenuItem item = new DXMenuItem("Rename Column", OnRenameColumn);
            renameColumn = menu.Column;
            menu.Items.Add(item);
        }

        void OnRenameColumn(object sender, EventArgs e) {
            UpdateRenameEditor(renameColumn);
        }

        private void UpdateRenameEditor(TreeListColumn column) {
            Rectangle rect = treeList.ViewInfo.ColumnsInfo[column].CaptionRect;
            rect.Y -= 2;
            renameEditor.EditValue = column.Caption == string.Empty ? column.FieldName : column.Caption;
            renameEditor.Bounds = rect;
            renameEditor.Visible = true;
            renameEditor.Select();
            renameEditor.SelectedText = renameEditor.Text;
        }

        public void DisableRenameColumnOption() {
            treeList.ShowTreeListMenu -= OnShowTreeListMenu;
            renameEditor.Validated += OnRenameEditorValidated;
        }
    }
}