namespace AmengSoft.IBTrader
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.historicalChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_CT = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowHistoryData = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.historicalChart)).BeginInit();
            this.mnuMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // historicalChart
            // 
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.historicalChart.ChartAreas.Add(chartArea1);
            this.historicalChart.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.historicalChart.Legends.Add(legend1);
            this.historicalChart.Location = new System.Drawing.Point(0, 24);
            this.historicalChart.Name = "historicalChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Price";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValuesPerPoint = 4;
            this.historicalChart.Series.Add(series1);
            this.historicalChart.Size = new System.Drawing.Size(1175, 387);
            this.historicalChart.TabIndex = 0;
            this.historicalChart.Text = "Historical Data";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuAction});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1175, 24);
            this.mnuMain.TabIndex = 2;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileConnect});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuFileConnect
            // 
            this.mnuFileConnect.Name = "mnuFileConnect";
            this.mnuFileConnect.Size = new System.Drawing.Size(152, 22);
            this.mnuFileConnect.Text = "&Connect";
            this.mnuFileConnect.Click += new System.EventHandler(this.mnuFileConnect_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(0, 417);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(1175, 93);
            this.messageBox.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.status_CT});
            this.statusStrip1.Location = new System.Drawing.Point(0, 513);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1175, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // status_CT
            // 
            this.status_CT.Name = "status_CT";
            this.status_CT.Size = new System.Drawing.Size(0, 17);
            // 
            // mnuAction
            // 
            this.mnuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowHistoryData});
            this.mnuAction.Name = "mnuAction";
            this.mnuAction.Size = new System.Drawing.Size(54, 20);
            this.mnuAction.Text = "&Action";
            // 
            // mnuShowHistoryData
            // 
            this.mnuShowHistoryData.Name = "mnuShowHistoryData";
            this.mnuShowHistoryData.Size = new System.Drawing.Size(171, 22);
            this.mnuShowHistoryData.Text = "&Show History Data";
            this.mnuShowHistoryData.Click += new System.EventHandler(this.mnuShowHistoryData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 535);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.historicalChart);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "MainForm";
            this.Text = "IB Trader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.historicalChart)).EndInit();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart historicalChart;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileConnect;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel status_CT;
        private System.Windows.Forms.ToolStripMenuItem mnuAction;
        private System.Windows.Forms.ToolStripMenuItem mnuShowHistoryData;
    }
}

