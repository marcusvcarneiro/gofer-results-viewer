namespace StructureResultsViewer
{
    partial class GraphicUserControl
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
            openglControl1 = new SharpGL.OpenGLControl();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)openglControl1).BeginInit();
            SuspendLayout();
            // 
            // openglControl1
            // 
            openglControl1.Dock = DockStyle.Top;
            openglControl1.DrawFPS = false;
            openglControl1.Location = new Point(0, 0);
            openglControl1.Margin = new Padding(8, 10, 8, 10);
            openglControl1.Name = "openglControl1";
            openglControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            openglControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            openglControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            openglControl1.Size = new Size(2203, 1231);
            openglControl1.TabIndex = 0;
            openglControl1.OpenGLInitialized += openglControl1_OpenGLInitialized;
            openglControl1.OpenGLDraw += openGLControl_OpenGLDraw;
            openglControl1.Resized += openglControl1_Resized;
            openglControl1.MouseWheel += openglControl_MouseWheel;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Displacement", "Stress", "EffectiveStress", "PorePressure" });
            comboBox1.Location = new Point(3, 1244);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(302, 49);
            comboBox1.TabIndex = 1;
            // 
            // GraphicUserControl
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(comboBox1);
            Controls.Add(openglControl1);
            Margin = new Padding(5, 7, 5, 7);
            Name = "GraphicUserControl";
            Size = new Size(2203, 1309);
            ((System.ComponentModel.ISupportInitialize)openglControl1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SharpGL.OpenGLControl openglControl1;
        private ComboBox comboBox1;
    }
}
