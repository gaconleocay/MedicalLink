namespace MedicalLink.ChucNang.PhieuTamUng
{
    partial class frmSuaPhieuTamUng
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuaPhieuTamUng));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblBillID = new DevExpress.XtraEditors.LabelControl();
            this.cboNguoiThu = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboPhong = new DevExpress.XtraEditors.LookUpEdit();
            this.cboKhoa = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSuaThongTinBN = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboNguoiThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhoa.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 16);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "ID phiếu thu:";
            // 
            // lblBillID
            // 
            this.lblBillID.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillID.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblBillID.Location = new System.Drawing.Point(106, 29);
            this.lblBillID.Name = "lblBillID";
            this.lblBillID.Size = new System.Drawing.Size(0, 19);
            this.lblBillID.TabIndex = 2;
            // 
            // cboNguoiThu
            // 
            this.cboNguoiThu.Location = new System.Drawing.Point(106, 150);
            this.cboNguoiThu.Name = "cboNguoiThu";
            this.cboNguoiThu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboNguoiThu.Properties.Appearance.Options.UseFont = true;
            this.cboNguoiThu.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.cboNguoiThu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNguoiThu.Properties.ImmediatePopup = true;
            this.cboNguoiThu.Properties.NullText = "";
            this.cboNguoiThu.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.cboNguoiThu.Properties.PopupFormMinSize = new System.Drawing.Size(400, 400);
            this.cboNguoiThu.Properties.PopupFormSize = new System.Drawing.Size(400, 400);
            this.cboNguoiThu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboNguoiThu.Properties.View = this.gridLookUpEdit1View;
            this.cboNguoiThu.Size = new System.Drawing.Size(399, 22);
            this.cboNguoiThu.TabIndex = 46;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.ColumnPanelRowHeight = 25;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.FooterPanelHeight = 25;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsFind.AlwaysVisible = true;
            this.gridLookUpEdit1View.OptionsFind.FindNullPrompt = "Từ khóa tìm kiếm...";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            this.gridLookUpEdit1View.RowHeight = 25;
            this.gridLookUpEdit1View.ViewCaptionHeight = 25;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "userhisid";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 123;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Mã người dùng";
            this.gridColumn2.FieldName = "usercode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 376;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Tên người dùng";
            this.gridColumn3.FieldName = "username";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 669;
            // 
            // cboPhong
            // 
            this.cboPhong.Location = new System.Drawing.Point(106, 110);
            this.cboPhong.Name = "cboPhong";
            this.cboPhong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhong.Properties.Appearance.Options.UseFont = true;
            this.cboPhong.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboPhong.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cboPhong.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboPhong.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboPhong.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboPhong.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cboPhong.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboPhong.Properties.AppearanceFocused.Options.UseFont = true;
            this.cboPhong.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboPhong.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cboPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentid", 35, "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentname", 100, "Tên")});
            this.cboPhong.Properties.DropDownRows = 15;
            this.cboPhong.Properties.NullText = "";
            this.cboPhong.Size = new System.Drawing.Size(399, 22);
            this.cboPhong.TabIndex = 45;
            // 
            // cboKhoa
            // 
            this.cboKhoa.Location = new System.Drawing.Point(106, 70);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.Properties.Appearance.Options.UseFont = true;
            this.cboKhoa.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboKhoa.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cboKhoa.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboKhoa.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboKhoa.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboKhoa.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cboKhoa.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboKhoa.Properties.AppearanceFocused.Options.UseFont = true;
            this.cboKhoa.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboKhoa.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cboKhoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboKhoa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentgroupid", 35, "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentgroupname", 100, "Tên"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("cumthutien", "Cụm")});
            this.cboKhoa.Properties.DropDownRows = 15;
            this.cboKhoa.Properties.NullText = "";
            this.cboKhoa.Size = new System.Drawing.Size(399, 22);
            this.cboKhoa.TabIndex = 44;
            this.cboKhoa.EditValueChanged += new System.EventHandler(this.cboKhoa_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl3.Location = new System.Drawing.Point(58, 73);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 16);
            this.labelControl3.TabIndex = 47;
            this.labelControl3.Text = "Khoa";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl4.Location = new System.Drawing.Point(51, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(35, 16);
            this.labelControl4.TabIndex = 48;
            this.labelControl4.Text = "Phòng";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl5.Location = new System.Drawing.Point(31, 153);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 16);
            this.labelControl5.TabIndex = 49;
            this.labelControl5.Text = "Người thu";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSuaThongTinBN);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 54);
            this.panel1.TabIndex = 50;
            // 
            // btnSuaThongTinBN
            // 
            this.btnSuaThongTinBN.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaThongTinBN.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSuaThongTinBN.Appearance.Options.UseFont = true;
            this.btnSuaThongTinBN.Appearance.Options.UseForeColor = true;
            this.btnSuaThongTinBN.Image = global::MedicalLink.Properties.Resources.check_mark_24;
            this.btnSuaThongTinBN.Location = new System.Drawing.Point(225, 9);
            this.btnSuaThongTinBN.Name = "btnSuaThongTinBN";
            this.btnSuaThongTinBN.Size = new System.Drawing.Size(100, 40);
            this.btnSuaThongTinBN.TabIndex = 47;
            this.btnSuaThongTinBN.Text = "Cập nhật";
            this.btnSuaThongTinBN.ToolTip = "Sửa thông tin hành chính";
            this.btnSuaThongTinBN.Click += new System.EventHandler(this.btnSuaThongTinBN_Click);
            // 
            // frmSuaPhieuTamUng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cboKhoa);
            this.Controls.Add(this.cboPhong);
            this.Controls.Add(this.cboNguoiThu);
            this.Controls.Add(this.lblBillID);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(572, 318);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(572, 318);
            this.Name = "frmSuaPhieuTamUng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa phiếu tạm ứng";
            this.Load += new System.EventHandler(this.frmSuaPhieuTamUng_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboNguoiThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhoa.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblBillID;
        private DevExpress.XtraEditors.GridLookUpEdit cboNguoiThu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.LookUpEdit cboPhong;
        private DevExpress.XtraEditors.LookUpEdit cboKhoa;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnSuaThongTinBN;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}