namespace StructureResultsViewer
{
    partial class NodeSpreadSheetControl
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
            dataGridView1 = new DataGridView();
            Selection = new DataGridViewCheckBoxColumn();
            GsaKeyColumn = new DataGridViewTextBoxColumn();
            xColumn = new DataGridViewTextBoxColumn();
            yColumn = new DataGridViewTextBoxColumn();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Selection, GsaKeyColumn, xColumn, yColumn });
            dataGridView1.Location = new Point(19, 18);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 102;
            dataGridView1.Size = new Size(913, 338);
            dataGridView1.TabIndex = 0;
            // 
            // Selection
            // 
            Selection.HeaderText = "Selected";
            Selection.MinimumWidth = 12;
            Selection.Name = "Selection";
            Selection.ReadOnly = true;
            Selection.Width = 200;
            // 
            // GsaKeyColumn
            // 
            GsaKeyColumn.HeaderText = "GsaKey";
            GsaKeyColumn.MinimumWidth = 12;
            GsaKeyColumn.Name = "GsaKeyColumn";
            GsaKeyColumn.ReadOnly = true;
            GsaKeyColumn.Width = 200;
            // 
            // xColumn
            // 
            xColumn.HeaderText = "x";
            xColumn.MinimumWidth = 12;
            xColumn.Name = "xColumn";
            xColumn.ReadOnly = true;
            xColumn.Width = 200;
            // 
            // yColumn
            // 
            yColumn.HeaderText = "y";
            yColumn.MinimumWidth = 12;
            yColumn.Name = "yColumn";
            yColumn.ReadOnly = true;
            yColumn.Width = 200;
            // 
            // button1
            // 
            button1.Location = new Point(585, 375);
            button1.Name = "button1";
            button1.Size = new Size(347, 58);
            button1.TabIndex = 1;
            button1.Text = "Load Structure Nodes";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SpreadSheetControl
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "SpreadSheetControl";
            Size = new Size(954, 451);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private DataGridViewCheckBoxColumn Selection;
        private DataGridViewTextBoxColumn GsaKeyColumn;
        private DataGridViewTextBoxColumn xColumn;
        private DataGridViewTextBoxColumn yColumn;
    }
}
