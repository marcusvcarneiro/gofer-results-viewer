namespace StructureResultsViewer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fileLoaderControl1 = new FileLoaderControl();
            button1 = new Button();
            button2 = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            plotControl1 = new PlotControl();
            tabPage2 = new TabPage();
            graphicUserControl1 = new GraphicUserControl();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // fileLoaderControl1
            // 
            fileLoaderControl1.Location = new Point(12, 42);
            fileLoaderControl1.Margin = new Padding(3, 5, 3, 5);
            fileLoaderControl1.ModelController = null;
            fileLoaderControl1.Name = "fileLoaderControl1";
            fileLoaderControl1.Size = new Size(1461, 96);
            fileLoaderControl1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(1810, 1592);
            button1.Name = "button1";
            button1.Size = new Size(188, 58);
            button1.TabIndex = 4;
            button1.Text = "Close";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(35, 1592);
            button2.Name = "button2";
            button2.Size = new Size(348, 58);
            button2.TabIndex = 5;
            button2.Text = "Filter Elements/Nodes";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(35, 131);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1976, 1455);
            tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(plotControl1);
            tabPage1.Location = new Point(10, 58);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1956, 1387);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // plotControl1
            // 
            plotControl1.AllowDrop = true;
            plotControl1.AutoSize = true;
            plotControl1.Dock = DockStyle.Top;
            plotControl1.Location = new Point(3, 3);
            plotControl1.Margin = new Padding(3, 5, 3, 5);
            plotControl1.ModelController = null;
            plotControl1.Name = "plotControl1";
            plotControl1.Size = new Size(1950, 1365);
            plotControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(graphicUserControl1);
            tabPage2.Location = new Point(10, 58);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1956, 1304);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // graphicUserControl1
            // 
            graphicUserControl1.Dock = DockStyle.Top;
            graphicUserControl1.Location = new Point(3, 3);
            graphicUserControl1.Margin = new Padding(5, 7, 5, 7);
            graphicUserControl1.ModelController = null;
            graphicUserControl1.Name = "graphicUserControl1";
            graphicUserControl1.Size = new Size(1950, 2298);
            graphicUserControl1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2012, 1656);
            Controls.Add(tabControl1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(fileLoaderControl1);
            Name = "Form1";
            Text = "Gofer Structure Results Viewer";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FileLoaderControl fileLoaderControl1;
        private Button button1;
        private Button button2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private PlotControl plotControl1;
        private TabPage tabPage2;
        private GraphicUserControl graphicUserControl1;
    }
}
