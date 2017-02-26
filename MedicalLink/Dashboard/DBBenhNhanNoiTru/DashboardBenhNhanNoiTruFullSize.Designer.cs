namespace MedicalLink.Dashboard.DBBenhNhanNoiTru
{
    partial class DashboardBenhNhanNoiTruFullSize
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StackedBarSeriesLabel stackedBarSeriesLabel1 = new DevExpress.XtraCharts.StackedBarSeriesLabel();
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView1 = new DevExpress.XtraCharts.StackedBarSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StackedBarSeriesLabel stackedBarSeriesLabel2 = new DevExpress.XtraCharts.StackedBarSeriesLabel();
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView2 = new DevExpress.XtraCharts.StackedBarSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardBenhNhanNoiTruFullSize));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControlData = new DevExpress.XtraEditors.PanelControl();
            this.chartControlBNNoiTru = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlData)).BeginInit();
            this.panelControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlBNNoiTru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControlData);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1350, 730);
            this.panelControl2.TabIndex = 2;
            // 
            // panelControlData
            // 
            this.panelControlData.Controls.Add(this.chartControlBNNoiTru);
            this.panelControlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlData.Location = new System.Drawing.Point(2, 2);
            this.panelControlData.Name = "panelControlData";
            this.panelControlData.Size = new System.Drawing.Size(1346, 726);
            this.panelControlData.TabIndex = 5;
            // 
            // chartControlBNNoiTru
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Label.TextPattern = "{V:#,##0}";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControlBNNoiTru.Diagram = xyDiagram1;
            this.chartControlBNNoiTru.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlBNNoiTru.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControlBNNoiTru.Location = new System.Drawing.Point(2, 2);
            this.chartControlBNNoiTru.Name = "chartControlBNNoiTru";
            this.chartControlBNNoiTru.PaletteName = "Metro";
            stackedBarSeriesLabel1.TextPattern = "{V:#,##0}";
            series1.Label = stackedBarSeriesLabel1;
            series1.Name = "BHYT";
            stackedBarSeriesView1.Border.Color = System.Drawing.Color.Orange;
            stackedBarSeriesView1.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            stackedBarSeriesView1.Color = System.Drawing.Color.PaleGreen;
            series1.View = stackedBarSeriesView1;
            stackedBarSeriesLabel2.TextPattern = "{V:#,##0}";
            series2.Label = stackedBarSeriesLabel2;
            series2.Name = "Viện phí";
            stackedBarSeriesView2.Border.Color = System.Drawing.Color.BlueViolet;
            stackedBarSeriesView2.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            stackedBarSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            series2.View = stackedBarSeriesView2;
            series3.Name = "Tạm ứng";
            lineSeriesView1.Color = System.Drawing.Color.Fuchsia;
            lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.View = lineSeriesView1;
            this.chartControlBNNoiTru.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3};
            this.chartControlBNNoiTru.Size = new System.Drawing.Size(1342, 722);
            this.chartControlBNNoiTru.TabIndex = 0;
            chartTitle1.Text = "Doanh thu giữa các khoa";
            this.chartControlBNNoiTru.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            this.chartControlBNNoiTru.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            // 
            // DashboardBenhNhanNoiTruFullSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.panelControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DashboardBenhNhanNoiTruFullSize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo bệnh nhân nội trú";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlData)).EndInit();
            this.panelControlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlBNNoiTru)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControlData;
        private DevExpress.XtraCharts.ChartControl chartControlBNNoiTru;

    }
}