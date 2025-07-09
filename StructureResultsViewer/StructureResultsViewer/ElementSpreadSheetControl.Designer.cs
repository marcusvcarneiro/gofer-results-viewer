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
            gsaIdxColumn = new DataGridViewTextBoxColumn();
            nodeColumn = new DataGridViewTextBoxColumn();
            xColumn = new DataGridViewTextBoxColumn();
            yColumn = new DataGridViewTextBoxColumn();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { checkColumn, gsaIdxColumn, nodeColumn, xColumn, yColumn });
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.Location = new Point(19, 16);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 102;
            dataGridView1.Size = new Size(835, 378);
            dataGridView1.TabIndex = 0;
            // 
            // checkColumn
            // 
            checkColumn.HeaderText = "Selected";
            checkColumn.MinimumWidth = 12;
            checkColumn.Name = "checkColumn";
            checkColumn.ReadOnly = true;
            checkColumn.Width = 250;
            // 
            // gsaIdxColumn
            // 
            gsaIdxColumn.HeaderText = "Elem";
            gsaIdxColumn.MinimumWidth = 12;
            gsaIdxColumn.Name = "gsaIdxColumn";
            gsaIdxColumn.ReadOnly = true;
            gsaIdxColumn.Width = 75;
            // 
            // nodeColumn
            // 
            nodeColumn.HeaderText = "Node";
            nodeColumn.MinimumWidth = 12;
            nodeColumn.Name = "nodeColumn";
            nodeColumn.ReadOnly = true;
            nodeColumn.Width = 250;
            // 
            // xColumn
            // 
            xColumn.HeaderText = "x";
            xColumn.MinimumWidth = 12;
            xColumn.Name = "xColumn";
            xColumn.ReadOnly = true;
            xColumn.Width = 250;
            // 
            // yColumn
            // 
            yColumn.HeaderText = "y";
            yColumn.MinimumWidth = 12;
            yColumn.Name = "yColumn";
            yColumn.ReadOnly = true;
            yColumn.Width = 250;
            // 
            // button1
            // 
            button1.Location = new Point(30, 409);
            button1.Name = "button1";
            button1.Size = new Size(354, 58);
            button1.TabIndex = 1;
            button1.Text = "Load Structure Elements";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(623, 409);
            button2.Name = "button2";
            button2.Size = new Size(231, 58);
            button2.TabIndex = 2;
            button2.Text = "Reset Selection";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(390, 409);
            button3.Name = "button3";
            button3.Size = new Size(227, 58);
            button3.TabIndex = 3;
            button3.Text = "Save Selection";
            button3.UseVisualStyleBackColor = true;
            // 
            // ElementSpreadSheetControl
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "ElementSpreadSheetControl";
            Size = new Size(870, 486);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridViewCheckBoxColumn checkColumn;
        private DataGridViewTextBoxColumn gsaIdxColumn;
        private DataGridViewTextBoxColumn nodeColumn;
        private DataGridViewTextBoxColumn xColumn;
        private DataGridViewTextBoxColumn yColumn;
    }
}
