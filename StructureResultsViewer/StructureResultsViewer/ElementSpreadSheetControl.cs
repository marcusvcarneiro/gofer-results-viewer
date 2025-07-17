using GoferAnalysisDTOs.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StructureResultsViewer
{
    public partial class ElementSpreadSheetControl : UserControlBase
    {
        public ElementSpreadSheetControl()
        {
            InitializeComponent();
        }

        private bool LoadElements()
        {
            dataGridView1.Rows.Clear();
            if (ModelController == null)
            {
                return false;
            }
            if (ModelController.Results == null)
            {
                return false;
            }

            Dictionary<Guid, int> guidGsaKeys = new Dictionary<Guid, int>(ModelController.Results.Nodes.Count);

            foreach (Node node in ModelController.Results.Nodes.Values)
            {
                guidGsaKeys.Add(node.Id, node.GsaModelKey);
            }

            HashSet<Guid> structureNodes = new HashSet<Guid>();

            foreach (Element1d element1D in ModelController.Results.StructureElements)
            {
                ElementEndNode node0 = element1D.Nodes.First(), node1 = element1D.Nodes.Last();
                (double, double) pointNode0 = (0, 0), pointNode1 = (0, 0);
                int gsaModelkey0 = guidGsaKeys[node0.NodeId], gsaModelkey1 = guidGsaKeys[node1.NodeId];
                if (ModelController.Results.Nodes.TryGetValue(gsaModelkey0, out Node? value0))
                {
                    pointNode0.Item1 = value0.X;
                    pointNode0.Item2 = value0.Y;
                }
                if (ModelController.Results.Nodes.TryGetValue(gsaModelkey1, out Node? value1))
                {
                    pointNode1.Item1 = value1.X;
                    pointNode1.Item2 = value1.Y;
                }
                dataGridView1.Rows.Insert(dataGridView1.Rows.Count, true, element1D.GsaModelKey, pointNode0.Item1, pointNode0.Item2, pointNode1.Item1, pointNode1.Item2);
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ModelController == null)
            {
                return;
            }
            HashSet<int> selectedElems = new HashSet<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var selected = row.Cells["checkColumn"];
                if (selected != null && selected.Value != null)
                {
                    if (selected.Value is bool selection && selection)
                    {
                        var elemIndex = row.Cells["elemColumn"];
                        if (elemIndex != null && elemIndex.Value != null)
                        {
                            if (elemIndex.Value is Int32 gsaModelKey)
                            {
                                selectedElems.Add(gsaModelKey);
                            }
                        }
                    }
                }
            }
            ModelController.SetNodeSelection(selectedElems);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["checkColumn"].Value = checkBox1.Checked;
            }
        }

        private void ElementSpreadSheetControl_Load(object sender, EventArgs e)
        {
            LoadElements();
        }
    }
}
