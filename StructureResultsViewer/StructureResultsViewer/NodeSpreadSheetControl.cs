using GoferAnalysisDTOs.Results;
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
    public partial class NodeSpreadSheetControl : UserControlBase
    {
        public NodeSpreadSheetControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (ModelController == null)
            {
                return;
            }
            if (ModelController.Results == null)
            {
                return;
            }

            Dictionary<Guid, int> guidGsaKeys = new Dictionary<Guid, int>(ModelController.Results.Nodes.Count);

            foreach (Node node in ModelController.Results.Nodes.Values)
            {
                guidGsaKeys.Add(node.Id, node.GsaModelKey);
            }


            List<Guid> structureNodes = new List<Guid>();
            foreach (Element1d element1D in ModelController.Results.StructureElements)
            {
                foreach (ElementEndNode node in element1D.Nodes)
                {
                    structureNodes.Add(node.NodeId);
                }
            }

            foreach (Guid node_guid in structureNodes)
            {
                int gsaModelkey = guidGsaKeys[node_guid];
                if (ModelController.Results.Nodes.TryGetValue(gsaModelkey, out Node? value))
                {
                    dataGridView1.Rows.Insert(dataGridView1.Rows.Count, false, gsaModelkey, value.X, value.Y);
                }
            }
        }


    }
}
