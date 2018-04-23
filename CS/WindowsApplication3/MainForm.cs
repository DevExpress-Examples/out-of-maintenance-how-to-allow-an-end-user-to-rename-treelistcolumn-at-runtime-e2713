using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace DXSample {
    public partial class MainForm: XtraForm {
        public MainForm() {
            InitializeComponent();
        }

        TreeListColumnMenuExtender extender;
      
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            new DevExpress.XtraTreeList.Design.XViews(treeList1);
            extender = new TreeListColumnMenuExtender(treeList1);
            extender.EnableRenameColumnOption();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            extender.DisableRenameColumnOption();
        }
    }
}
