namespace StructureResultsViewer
{
    partial class ElementSpreadSheetControl
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
            checkColumn = new DataGridViewCheckBoxColumn();
            elemColumn = new DataGridViewTextBoxColumn();
            x0Column = new DataGridViewTextBoxColumn();
            y0Column = new DataGridViewTextBoxColumn();
            x1Column = new DataGridViewTextBoxColumn();
            y1Column = new DataGridViewTextBoxColumn();
            button3 = new Button();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { checkColumn, elemColumn, x0Column, y0Column, x1Column, y1Column });
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dataGridView1.Location = new Point(19, 16);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 102;
            dataGridView1.Size = new Size(992, 611);
            dataGridView1.TabIndex = 0;
            // 
            // checkColumn
            // 
            checkColumn.HeaderText = "Selected";
            checkColumn.MinimumWidth = 12;
            checkColumn.Name = "checkColumn";
            checkColumn.Width = 137;
            // 
            // elemColumn
            // 
            elemColumn.HeaderText = "Element";
            elemColumn.MinimumWidth = 12;
            elemColumn.Name = "elemColumn";
            elemColumn.ReadOnly = true;
            elemColumn.Width = 179;
            // 
            // x0Column
            // 
            x0Column.HeaderText = "x0";
            x0Column.MinimumWidth = 12;
            x0Column.Name = "x0Column";
            x0Column.ReadOnly = true;
            x0Column.Width = 102;
            // 
            // y0Column
            // 
            y0Column.HeaderText = "y0";
            y0Column.MinimumWidth = 12;
            y0Column.Name = "y0Column";
            y0Column.ReadOnly = true;
            y0Column.Width = 103;
            // 
            // x1Column
            // 
            x1Column.HeaderText = "x1";
            x1Column.MinimumWidth = 12;
            x1Column.Name = "x1Column";
            x1Column.Width = 102;
            // 
            // y1Column
            // 
            y1Column.HeaderText = "y1";
            y1Column.MinimumWidth = 12;
            y1Column.Name = "y1Column";
            y1Column.ReadOnly = true;
            y1Column.Width = 103;
            // 
            // button3
            // 
            button3.Location = new Point(784, 641);
            button3.Name = "button3";
            button3.Size = new Size(227, 58);
            button3.TabIndex = 3;
            button3.Text = "Save Selection";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(19, 649);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(298, 45);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "Select/Unselect all";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ElementSpreadSheetControl
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkBox1);
            Controls.Add(button3);
            Controls.Add(dataGridView1);
            Name = "ElementSpreadSheetControl";
            Size = new Size(1023, 711);
            Load += ElementSpreadSheetControl_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button3;
        private DataGridViewCheckBoxColumn checkColumn;
        private DataGridViewTextBoxColumn elemColumn;
        private DataGridViewTextBoxColumn x0Column;
        private DataGridViewTextBoxColumn y0Column;
        private DataGridViewTextBoxColumn x1Column;
        private DataGridViewTextBoxColumn y1Column;
        private CheckBox checkBox1;
    }
}
