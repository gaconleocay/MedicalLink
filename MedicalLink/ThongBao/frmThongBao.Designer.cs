namespace MedicalLink.ThongBao
{
    partial class frmThongBao
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThongBao));
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.timerThongBao = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblThongBao
            // 
            this.lblThongBao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblThongBao.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("lblThongBao.Appearance.Image")));
            this.lblThongBao.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblThongBao.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblThongBao.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblThongBao.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblThongBao.Location = new System.Drawing.Point(4, 12);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(376, 80);
            this.lblThongBao.TabIndex = 1;
            this.lblThongBao.Text = "Thông báo";
            this.lblThongBao.Click += new System.EventHandler(this.lblThongBao_Click);
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
            // 
            // frmThongBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 104);
            this.ControlBox = false;
            this.Controls.Add(this.lblThongBao);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 120);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 120);
            this.Name = "frmThongBao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl lblThongBao;
        private System.Windows.Forms.Timer timerThongBao;

    }
}