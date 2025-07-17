namespace StructureResultsViewer
{
    partial class PlotControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            checkBox1 = new CheckBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // chart1
            // 
            chart1.BorderlineWidth = 20;
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 7.1146245F;
            legend1.Position.Width = 17.472435F;
            legend1.Position.X = 79.527565F;
            legend1.Position.Y = 3F;
            legend2.Name = "Legend2";
            legend2.Position.Auto = false;
            legend2.Position.Height = 7.1146245F;
            legend2.Position.Width = 27.2264633F;
            legend2.Position.X = 69.77354F;
            legend2.Position.Y = 3F;
            chart1.Legends.Add(legend1);
            chart1.Legends.Add(legend2);
            chart1.Location = new Point(24, 81);
            chart1.Margin = new Padding(3, 4, 3, 4);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(1876, 1192);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(473, 25);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(219, 45);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Flip X/Y Axis";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Total displacement", "Stage displacement", "Bending moment", "Axial force", "Shear force", "Reactions (contacts)", "Relative displacement (contacts)", "Steady pore-pressure (contacts)", "Excess pore-pressure (contacts)" });
            comboBox2.Location = new Point(24, 25);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(302, 49);
            comboBox2.TabIndex = 3;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "mod", "X", "Y" });
            comboBox1.Location = new Point(332, 25);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(125, 49);
            comboBox1.TabIndex = 5;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(1712, 1300);
            button1.Name = "button1";
            button1.Size = new Size(188, 58);
            button1.TabIndex = 6;
            button1.Text = "Export CSV";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PlotControl
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(comboBox2);
            Controls.Add(checkBox1);
            Controls.Add(chart1);
            Margin = new Padding(3, 5, 3, 5);
            Name = "PlotControl";
            Size = new Size(1940, 1401);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private CheckBox checkBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Button button1;
    }
}
