namespace MedicalLink.ChucNang
{
    partial class ucTuyChonBaoCao
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTuyChonBaoCao));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupBoxXuatBC = new System.Windows.Forms.GroupBox();
            this.tbnXuatExcel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBoxTuyChon = new System.Windows.Forms.GroupBox();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tbnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBoxXuatBC.SuspendLayout();
            this.groupBoxTuyChon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupBoxXuatBC);
            this.panelControl1.Controls.Add(this.groupBoxTuyChon);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(771, 81);
            this.panelControl1.TabIndex = 0;
            // 
            // groupBoxXuatBC
            // 
            this.groupBoxXuatBC.Controls.Add(this.tbnXuatExcel);
            this.groupBoxXuatBC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxXuatBC.Location = new System.Drawing.Point(507, 6);
            this.groupBoxXuatBC.Name = "groupBoxXuatBC";
            this.groupBoxXuatBC.Size = new System.Drawing.Size(259, 70);
            this.groupBoxXuatBC.TabIndex = 3;
            this.groupBoxXuatBC.TabStop = false;
            this.groupBoxXuatBC.Text = "Xuất báo cáo";
            // 
            // tbnXuatExcel
            // 
            this.tbnXuatExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnXuatExcel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbnXuatExcel.Appearance.Options.UseFont = true;
            this.tbnXuatExcel.Appearance.Options.UseForeColor = true;
            this.tbnXuatExcel.Image = ((System.Drawing.Image)(resources.GetObject("tbnXuatExcel.Image")));
            this.tbnXuatExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.tbnXuatExcel.Location = new System.Drawing.Point(9, 16);
            this.tbnXuatExcel.Name = "tbnXuatExcel";
            this.tbnXuatExcel.Size = new System.Drawing.Size(100, 40);
            this.tbnXuatExcel.TabIndex = 7;
            this.tbnXuatExcel.Text = "Export...";
            this.tbnXuatExcel.Click += new System.EventHandler(this.tbnXuatExcel_Click);
            // 
            // groupBoxTuyChon
            // 
            this.groupBoxTuyChon.Controls.Add(this.comboBoxEdit2);
            this.groupBoxTuyChon.Controls.Add(this.labelControl4);
            this.groupBoxTuyChon.Controls.Add(this.tbnOK);
            this.groupBoxTuyChon.Controls.Add(this.labelControl3);
            this.groupBoxTuyChon.Controls.Add(this.comboBoxEdit1);
            this.groupBoxTuyChon.Controls.Add(this.dateTimePicker2);
            this.groupBoxTuyChon.Controls.Add(this.dateTimePicker1);
            this.groupBoxTuyChon.Controls.Add(this.labelControl2);
            this.groupBoxTuyChon.Controls.Add(this.labelControl1);
            this.groupBoxTuyChon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxTuyChon.Location = new System.Drawing.Point(5, 2);
            this.groupBoxTuyChon.Name = "groupBoxTuyChon";
            this.groupBoxTuyChon.Size = new System.Drawing.Size(496, 74);
            this.groupBoxTuyChon.TabIndex = 2;
            this.groupBoxTuyChon.TabStop = false;
            this.groupBoxTuyChon.Text = "Tùy chọn báo cáo";
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Location = new System.Drawing.Point(240, 41);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
            "Ngoại trú",
            "Nội trú",
            "Ngoại trú+Nội trú"});
            this.comboBoxEdit2.Size = new System.Drawing.Size(126, 20);
            this.comboBoxEdit2.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(195, 45);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(35, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Loại BA";
            // 
            // tbnOK
            // 
            this.tbnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnOK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbnOK.Appearance.Options.UseFont = true;
            this.tbnOK.Appearance.Options.UseForeColor = true;
            this.tbnOK.Image = ((System.Drawing.Image)(resources.GetObject("tbnOK.Image")));
            this.tbnOK.Location = new System.Drawing.Point(377, 18);
            this.tbnOK.Name = "tbnOK";
            this.tbnOK.Size = new System.Drawing.Size(100, 40);
            this.tbnOK.TabIndex = 6;
            this.tbnOK.Text = "OK";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(195, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Tiêu chí";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(240, 16);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "Theo ngày vào viện",
            "Theo ngày ra viện",
            "Theo ngày duyệt VP",
            "Theo ngày duyệt BHYT"});
            this.comboBoxEdit1.Size = new System.Drawing.Size(126, 20);
            this.comboBoxEdit1.TabIndex = 4;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "HH:mm dd/MM/yyyy";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(59, 38);
            this.dateTimePicker2.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(128, 21);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.Value = new System.DateTime(2015, 11, 25, 23, 59, 0, 0);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(59, 15);
            this.dateTimePicker1.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(128, 21);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Value = new System.DateTime(2015, 11, 25, 0, 0, 0, 0);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Đến ngày";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày\r\n";
            // 
            // ucTuyChonBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ucTuyChonBaoCao";
            this.Size = new System.Drawing.Size(771, 81);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.groupBoxXuatBC.ResumeLayout(false);
            this.groupBoxTuyChon.ResumeLayout(false);
            this.groupBoxTuyChon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.GroupBox groupBoxTuyChon;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton tbnOK;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox groupBoxXuatBC;
        private DevExpress.XtraEditors.SimpleButton tbnXuatExcel;
    }
}
