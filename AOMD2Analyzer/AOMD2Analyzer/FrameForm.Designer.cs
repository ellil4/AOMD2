namespace AOMD2Analyzer
{
    partial class FrameForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.amPicBoxScreen = new System.Windows.Forms.PictureBox();
            this.amBtnR = new System.Windows.Forms.Button();
            this.amBtnF = new System.Windows.Forms.Button();
            this.amTextBoxFrame = new System.Windows.Forms.TextBox();
            this.amTextBoxADDR = new System.Windows.Forms.TextBox();
            this.amBtnOpen = new System.Windows.Forms.Button();
            this.amLabelPercentage = new System.Windows.Forms.Label();
            this.amTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.amHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.amLabelAnalyzeInfo2 = new System.Windows.Forms.Label();
            this.amBtnClearSample = new System.Windows.Forms.Button();
            this.amBtnNoiseSample = new System.Windows.Forms.Button();
            this.amLabelAnalzeInfo = new System.Windows.Forms.Label();
            this.amBtnClip = new System.Windows.Forms.Button();
            this.amBtnLoad = new System.Windows.Forms.Button();
            this.amBtnPrint = new System.Windows.Forms.Button();
            this.amBtnGlobal = new System.Windows.Forms.Button();
            this.amChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.amTBGrpYMax = new System.Windows.Forms.TextBox();
            this.amTBHisXMax = new System.Windows.Forms.TextBox();
            this.amTBHisYMax = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.amBtnRefresh = new System.Windows.Forms.Button();
            this.amBtnScaleNormal = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            ((System.ComponentModel.ISupportInitialize)(this.amPicBoxScreen)).BeginInit();
            this.amTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amChart)).BeginInit();
            this.SuspendLayout();
            // 
            // amPicBoxScreen
            // 
            this.amPicBoxScreen.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.amPicBoxScreen.Location = new System.Drawing.Point(0, 0);
            this.amPicBoxScreen.Name = "amPicBoxScreen";
            this.amPicBoxScreen.Size = new System.Drawing.Size(640, 480);
            this.amPicBoxScreen.TabIndex = 0;
            this.amPicBoxScreen.TabStop = false;
            // 
            // amBtnR
            // 
            this.amBtnR.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amBtnR.Location = new System.Drawing.Point(407, 486);
            this.amBtnR.Name = "amBtnR";
            this.amBtnR.Size = new System.Drawing.Size(46, 48);
            this.amBtnR.TabIndex = 1;
            this.amBtnR.Text = "<";
            this.amBtnR.UseVisualStyleBackColor = true;
            this.amBtnR.Click += new System.EventHandler(this.amBtnR_Click);
            // 
            // amBtnF
            // 
            this.amBtnF.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amBtnF.Location = new System.Drawing.Point(565, 486);
            this.amBtnF.Name = "amBtnF";
            this.amBtnF.Size = new System.Drawing.Size(46, 48);
            this.amBtnF.TabIndex = 2;
            this.amBtnF.Text = ">";
            this.amBtnF.UseVisualStyleBackColor = true;
            this.amBtnF.Click += new System.EventHandler(this.amBtnF_Click);
            // 
            // amTextBoxFrame
            // 
            this.amTextBoxFrame.BackColor = System.Drawing.Color.YellowGreen;
            this.amTextBoxFrame.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.amTextBoxFrame.Location = new System.Drawing.Point(459, 504);
            this.amTextBoxFrame.Name = "amTextBoxFrame";
            this.amTextBoxFrame.Size = new System.Drawing.Size(100, 21);
            this.amTextBoxFrame.TabIndex = 6;
            this.amTextBoxFrame.Text = "--------";
            this.amTextBoxFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.amTextBoxFrame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.amTextBoxFrame_KeyDown);
            this.amTextBoxFrame.Leave += new System.EventHandler(this.amTextBoxFrame_Leave);
            // 
            // amTextBoxADDR
            // 
            this.amTextBoxADDR.BackColor = System.Drawing.Color.YellowGreen;
            this.amTextBoxADDR.Enabled = false;
            this.amTextBoxADDR.Location = new System.Drawing.Point(329, 540);
            this.amTextBoxADDR.Name = "amTextBoxADDR";
            this.amTextBoxADDR.ReadOnly = true;
            this.amTextBoxADDR.Size = new System.Drawing.Size(308, 21);
            this.amTextBoxADDR.TabIndex = 7;
            // 
            // amBtnOpen
            // 
            this.amBtnOpen.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amBtnOpen.Location = new System.Drawing.Point(4, 486);
            this.amBtnOpen.Name = "amBtnOpen";
            this.amBtnOpen.Size = new System.Drawing.Size(46, 48);
            this.amBtnOpen.TabIndex = 8;
            this.amBtnOpen.Text = "Op";
            this.amBtnOpen.UseVisualStyleBackColor = true;
            this.amBtnOpen.Click += new System.EventHandler(this.amBtnOpen_Click);
            // 
            // amLabelPercentage
            // 
            this.amLabelPercentage.AutoSize = true;
            this.amLabelPercentage.Font = new System.Drawing.Font("Anonymous", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amLabelPercentage.Location = new System.Drawing.Point(93, 510);
            this.amLabelPercentage.Name = "amLabelPercentage";
            this.amLabelPercentage.Size = new System.Drawing.Size(36, 24);
            this.amLabelPercentage.TabIndex = 10;
            this.amLabelPercentage.Text = "--";
            // 
            // amTabControl
            // 
            this.amTabControl.Controls.Add(this.tabPage1);
            this.amTabControl.Controls.Add(this.tabPage2);
            this.amTabControl.Location = new System.Drawing.Point(0, 0);
            this.amTabControl.Name = "amTabControl";
            this.amTabControl.SelectedIndex = 0;
            this.amTabControl.Size = new System.Drawing.Size(650, 664);
            this.amTabControl.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.amPicBoxScreen);
            this.tabPage1.Controls.Add(this.amBtnOpen);
            this.tabPage1.Controls.Add(this.amBtnR);
            this.tabPage1.Controls.Add(this.amLabelPercentage);
            this.tabPage1.Controls.Add(this.amBtnF);
            this.tabPage1.Controls.Add(this.amTextBoxADDR);
            this.tabPage1.Controls.Add(this.amTextBoxFrame);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(642, 638);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.amBtnScaleNormal);
            this.tabPage2.Controls.Add(this.amBtnRefresh);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.amTBHisYMax);
            this.tabPage2.Controls.Add(this.amTBHisXMax);
            this.tabPage2.Controls.Add(this.amTBGrpYMax);
            this.tabPage2.Controls.Add(this.amHistogram);
            this.tabPage2.Controls.Add(this.amLabelAnalyzeInfo2);
            this.tabPage2.Controls.Add(this.amBtnClearSample);
            this.tabPage2.Controls.Add(this.amBtnNoiseSample);
            this.tabPage2.Controls.Add(this.amLabelAnalzeInfo);
            this.tabPage2.Controls.Add(this.amBtnClip);
            this.tabPage2.Controls.Add(this.amBtnLoad);
            this.tabPage2.Controls.Add(this.amBtnPrint);
            this.tabPage2.Controls.Add(this.amBtnGlobal);
            this.tabPage2.Controls.Add(this.amChart);
            this.tabPage2.Controls.Add(this.shapeContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(642, 638);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // amHistogram
            // 
            this.amHistogram.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea5.Name = "ChartArea1";
            this.amHistogram.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.amHistogram.Legends.Add(legend5);
            this.amHistogram.Location = new System.Drawing.Point(1, 270);
            this.amHistogram.Name = "amHistogram";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.amHistogram.Series.Add(series5);
            this.amHistogram.Size = new System.Drawing.Size(639, 250);
            this.amHistogram.TabIndex = 13;
            this.amHistogram.Text = "chart1";
            // 
            // amLabelAnalyzeInfo2
            // 
            this.amLabelAnalyzeInfo2.AutoSize = true;
            this.amLabelAnalyzeInfo2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.amLabelAnalyzeInfo2.Location = new System.Drawing.Point(3, 618);
            this.amLabelAnalyzeInfo2.Name = "amLabelAnalyzeInfo2";
            this.amLabelAnalyzeInfo2.Size = new System.Drawing.Size(75, 17);
            this.amLabelAnalyzeInfo2.TabIndex = 12;
            this.amLabelAnalyzeInfo2.Text = "AnalyzeInfo";
            // 
            // amBtnClearSample
            // 
            this.amBtnClearSample.Location = new System.Drawing.Point(534, 527);
            this.amBtnClearSample.Name = "amBtnClearSample";
            this.amBtnClearSample.Size = new System.Drawing.Size(75, 23);
            this.amBtnClearSample.TabIndex = 11;
            this.amBtnClearSample.Text = "Clear";
            this.amBtnClearSample.UseVisualStyleBackColor = true;
            this.amBtnClearSample.Click += new System.EventHandler(this.amBtnClearSample_Click);
            // 
            // amBtnNoiseSample
            // 
            this.amBtnNoiseSample.Location = new System.Drawing.Point(453, 527);
            this.amBtnNoiseSample.Name = "amBtnNoiseSample";
            this.amBtnNoiseSample.Size = new System.Drawing.Size(75, 23);
            this.amBtnNoiseSample.TabIndex = 10;
            this.amBtnNoiseSample.Text = "As Sample";
            this.amBtnNoiseSample.UseVisualStyleBackColor = true;
            this.amBtnNoiseSample.Click += new System.EventHandler(this.amBtnNoiseSample_Click);
            // 
            // amLabelAnalzeInfo
            // 
            this.amLabelAnalzeInfo.AutoSize = true;
            this.amLabelAnalzeInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.amLabelAnalzeInfo.Location = new System.Drawing.Point(3, 596);
            this.amLabelAnalzeInfo.Name = "amLabelAnalzeInfo";
            this.amLabelAnalzeInfo.Size = new System.Drawing.Size(75, 17);
            this.amLabelAnalzeInfo.TabIndex = 9;
            this.amLabelAnalzeInfo.Text = "AnalyzeInfo";
            // 
            // amBtnClip
            // 
            this.amBtnClip.Location = new System.Drawing.Point(172, 527);
            this.amBtnClip.Name = "amBtnClip";
            this.amBtnClip.Size = new System.Drawing.Size(75, 23);
            this.amBtnClip.TabIndex = 8;
            this.amBtnClip.Text = "Clip...";
            this.amBtnClip.UseVisualStyleBackColor = true;
            this.amBtnClip.Click += new System.EventHandler(this.amBtnClip_Click);
            // 
            // amBtnLoad
            // 
            this.amBtnLoad.Location = new System.Drawing.Point(91, 527);
            this.amBtnLoad.Name = "amBtnLoad";
            this.amBtnLoad.Size = new System.Drawing.Size(75, 23);
            this.amBtnLoad.TabIndex = 7;
            this.amBtnLoad.Text = "Load...";
            this.amBtnLoad.UseVisualStyleBackColor = true;
            this.amBtnLoad.Click += new System.EventHandler(this.amBtnLoad_Click);
            // 
            // amBtnPrint
            // 
            this.amBtnPrint.Location = new System.Drawing.Point(10, 556);
            this.amBtnPrint.Name = "amBtnPrint";
            this.amBtnPrint.Size = new System.Drawing.Size(75, 23);
            this.amBtnPrint.TabIndex = 6;
            this.amBtnPrint.Text = "Print";
            this.amBtnPrint.UseVisualStyleBackColor = true;
            this.amBtnPrint.Click += new System.EventHandler(this.amBtnPrint_Click);
            // 
            // amBtnGlobal
            // 
            this.amBtnGlobal.Location = new System.Drawing.Point(10, 527);
            this.amBtnGlobal.Name = "amBtnGlobal";
            this.amBtnGlobal.Size = new System.Drawing.Size(75, 23);
            this.amBtnGlobal.TabIndex = 1;
            this.amBtnGlobal.Text = "Global...";
            this.amBtnGlobal.UseVisualStyleBackColor = true;
            this.amBtnGlobal.Click += new System.EventHandler(this.amBtnGlobal_Click);
            // 
            // amChart
            // 
            this.amChart.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea6.Name = "ChartArea1";
            this.amChart.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.amChart.Legends.Add(legend6);
            this.amChart.Location = new System.Drawing.Point(1, 6);
            this.amChart.Name = "amChart";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.amChart.Series.Add(series6);
            this.amChart.Size = new System.Drawing.Size(639, 250);
            this.amChart.TabIndex = 0;
            this.amChart.Text = "chart1";
            // 
            // amTBGrpYMax
            // 
            this.amTBGrpYMax.Location = new System.Drawing.Point(558, 201);
            this.amTBGrpYMax.Name = "amTBGrpYMax";
            this.amTBGrpYMax.Size = new System.Drawing.Size(71, 21);
            this.amTBGrpYMax.TabIndex = 14;
            // 
            // amTBHisXMax
            // 
            this.amTBHisXMax.Location = new System.Drawing.Point(558, 438);
            this.amTBHisXMax.Name = "amTBHisXMax";
            this.amTBHisXMax.Size = new System.Drawing.Size(71, 21);
            this.amTBHisXMax.TabIndex = 15;
            // 
            // amTBHisYMax
            // 
            this.amTBHisYMax.Location = new System.Drawing.Point(558, 465);
            this.amTBHisYMax.Name = "amTBHisYMax";
            this.amTBHisYMax.Size = new System.Drawing.Size(71, 21);
            this.amTBHisYMax.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(517, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "Y-Max";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 441);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "X-Max";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 468);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "Y-Max";
            // 
            // amBtnRefresh
            // 
            this.amBtnRefresh.Location = new System.Drawing.Point(270, 527);
            this.amBtnRefresh.Name = "amBtnRefresh";
            this.amBtnRefresh.Size = new System.Drawing.Size(75, 23);
            this.amBtnRefresh.TabIndex = 20;
            this.amBtnRefresh.Text = "Refresh";
            this.amBtnRefresh.UseVisualStyleBackColor = true;
            this.amBtnRefresh.Click += new System.EventHandler(this.amBtnRefresh_Click);
            // 
            // amBtnScaleNormal
            // 
            this.amBtnScaleNormal.Location = new System.Drawing.Point(351, 527);
            this.amBtnScaleNormal.Name = "amBtnScaleNormal";
            this.amBtnScaleNormal.Size = new System.Drawing.Size(75, 23);
            this.amBtnScaleNormal.TabIndex = 21;
            this.amBtnScaleNormal.Text = "Norm View";
            this.amBtnScaleNormal.UseVisualStyleBackColor = true;
            this.amBtnScaleNormal.Click += new System.EventHandler(this.amBtnScaleNormal_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(636, 632);
            this.shapeContainer1.TabIndex = 22;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.rectangleShape1.Location = new System.Drawing.Point(258, 519);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(174, 32);
            // 
            // FrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 662);
            this.Controls.Add(this.amTabControl);
            this.Name = "FrameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AOMD2 Analyzer Research Version";
            ((System.ComponentModel.ISupportInitialize)(this.amPicBoxScreen)).EndInit();
            this.amTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox amPicBoxScreen;
        private System.Windows.Forms.Button amBtnR;
        private System.Windows.Forms.Button amBtnF;
        private System.Windows.Forms.TextBox amTextBoxFrame;
        private System.Windows.Forms.TextBox amTextBoxADDR;
        private System.Windows.Forms.Button amBtnOpen;
        private System.Windows.Forms.Label amLabelPercentage;
        private System.Windows.Forms.TabControl amTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart amChart;
        private System.Windows.Forms.Button amBtnGlobal;
        private System.Windows.Forms.Button amBtnPrint;
        private System.Windows.Forms.Button amBtnLoad;
        private System.Windows.Forms.Button amBtnClip;
        private System.Windows.Forms.Label amLabelAnalzeInfo;
        private System.Windows.Forms.Button amBtnNoiseSample;
        private System.Windows.Forms.Button amBtnClearSample;
        private System.Windows.Forms.Label amLabelAnalyzeInfo2;
        private System.Windows.Forms.DataVisualization.Charting.Chart amHistogram;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox amTBHisYMax;
        private System.Windows.Forms.TextBox amTBHisXMax;
        private System.Windows.Forms.TextBox amTBGrpYMax;
        private System.Windows.Forms.Button amBtnRefresh;
        private System.Windows.Forms.Button amBtnScaleNormal;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
    }
}

