using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StructureResultsViewer
{
    public partial class ElementSelectionForm : Form
    {
        public ElementSelectionForm()
        {
            InitializeComponent();
        }

        public ElementSelectionForm(Controller controller)
        {
            InitializeComponent();
            elementSpreadSheetControl1.ModelController = controller;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
