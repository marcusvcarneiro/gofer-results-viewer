using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StructureResultsViewer
{
    public partial class FileLoaderControl : UserControlBase
    {


        public FileLoaderControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ModelController == null)
            {
                MessageBox.Show("Model controller not connected", "Check Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "json files (*.json)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        FileInfo? inputFilePath = new FileInfo(openFileDialog.FileName);
                        textBox1.Text = inputFilePath.FullName;
                        ModelController.LoadResultsJson(inputFilePath);
                    }
                }
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("The input json has an unexpected schema", "Results Json required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
