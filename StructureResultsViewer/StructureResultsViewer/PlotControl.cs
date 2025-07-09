using GoferAnalysisDTOs.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StructureResultsViewer
{
    public partial class PlotControl : UserControlBase
    {
        private bool _invertAxis;
        private string _selected_result;
        private string _selected_direction;

        public PlotControl()
        {
            InitializeComponent();
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            _selected_result = string.Empty;
            _selected_direction = string.Empty;
        }

        private void PlotResults()
        {
            if (ModelController == null)
            {
                return;
            }
            if (ModelController.Results == null)
            {
                return;
            }
            if (ModelController.Results.StructureElements.Count == 0)
            {
                MessageBox.Show("The input json does not contain structure elements. Try another. ", "Nothing to show", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            chart1.Series.Clear();

            if (_selected_result == "Reactions (contacts)" || _selected_result == "Relative displacement (contacts)" || _selected_result == "Steady pore-pressure (contacts)" | _selected_result == "Excess pore-pressure (contacts)")
            {
                PlotContactResults();
            }
            else
            {
                PlotNodalResults();
            }
        }

        private double GetValue(Displacement displacement, string direction)
        {
            if (direction == "X")
                return displacement.X;
            else if (direction == "Y")
                return displacement.Y;
            else
                return Math.Sqrt(displacement.X * displacement.X + displacement.Y * displacement.Y);
        }

        private double GetResult(ContactInterface CIResult, int index)
        {
            if ((_selected_result == "Reactions (contacts)") || (_selected_result == "Relative displacement (contacts)"))
            {
                List<Displacement> interfaceResult;
                if (_selected_result == "Reactions (contacts)")
                {
                    interfaceResult = CIResult.Reaction;
                }
                else
                {
                    interfaceResult = CIResult.RelativeDisplacement;
                }
                return GetValue(interfaceResult[index], _selected_direction);
            }
            else
            {
                if (_selected_result == "Steady pore-pressure (contacts)")
                {
                    return CIResult.SteadyStatePorePressure[index];
                }
                else
                {
                    return CIResult.ExcessPorePressure[index];
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _invertAxis = checkBox1.Checked;
            PlotResults();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selected_result = comboBox2.Text;
            if (_selected_result == "Bending moment" || _selected_result == "Axial force" || _selected_result == "Shear force" || _selected_result == "Steady pore-pressure (contacts)" || _selected_result == "Excess pore-pressure (contacts)")
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }
            PlotResults();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selected_direction = comboBox1.Text;
            PlotResults();
        }

        private void PlotNodalResults()
        {
            if (ModelController == null)
                return;

            if (ModelController.Results == null)
                return;

            Dictionary<Guid, int> guidGsaKeys = new Dictionary<Guid, int>(ModelController.Results.Nodes.Count);

            foreach (Node node in ModelController.Results.Nodes.Values)
            {
                guidGsaKeys.Add(node.Id, node.GsaModelKey);
            }

            int gsaKeyRefNode = guidGsaKeys[ModelController.Results.StructureElements[0].Nodes[0].NodeId];
            double x0 = ModelController.Results.Nodes[gsaKeyRefNode].X;
            double y0 = ModelController.Results.Nodes[gsaKeyRefNode].Y;
            HashSet<Guid> structureNodes = new HashSet<Guid>();
            Series series = new Series();
            foreach (Element1d element1D in ModelController.Results.StructureElements)
            {
                foreach (ElementEndNode node in element1D.Nodes)
                {
                    if (!structureNodes.Contains(node.Id))
                    {
                        structureNodes.Add(node.NodeId);
                    }
                }
            }
            foreach (Guid guid in structureNodes)
            {
                if (ModelController.Results.Nodes.TryGetValue(guidGsaKeys[guid], out Node? value))
                {
                    double length = Math.Sqrt((value.X - x0) * (value.X - x0) + (value.Y - y0) * (value.Y - y0));
                    double result = 0;
                    switch (_selected_result)
                    {
                        case "Total displacement":
                            result = GetValue(value.Displacement, _selected_direction);
                            break;
                        case "Stage displacement":
                            result = GetValue(value.StageDisplacement, _selected_direction);
                            break;
                        case "Bending moment":
                            result = value.BendingMoment ?? 0;
                            break;
                        case "Axial force":
                            result = value.AxialForce ?? 0;
                            break;
                        case "Shear force":
                            result = value.ShearForce ?? 0;
                            break;

                        default:
                            break;
                    }
                    if (_invertAxis)
                    {
                        series.Points.AddXY(result, length);
                    }
                    else
                    {
                        series.Points.AddXY(length, result);
                    }
                }
            }
            series.LegendText = _selected_result;
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;

            ChartArea chartArea = chart1.ChartAreas.First();

            chartArea.AxisX.Minimum = series.Points.Min(x => x.XValue);
            chartArea.AxisX.Maximum = series.Points.Max(x => x.XValue);
            chartArea.AxisX.LabelStyle.Angle = 0;
            chartArea.AxisX.LabelStyle.Format = "{0:0.000}";

            if (_invertAxis)
            {
                chartArea.AxisX.Title = _selected_result + " " + _selected_direction;
                chartArea.AxisY.Title = "Length";
            }
            else
            {
                chartArea.AxisY.Title = _selected_result + " " + _selected_direction;
                chartArea.AxisX.Title = "Length";
            }
            chart1.Series.Add(series);
            chart1.Legends[0].Position.Auto = false;
            chartArea.RecalculateAxesScale();
            chart1.Update();
        }

        private void PlotContactResults()
        {
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

            int gsaKeyRefNode = guidGsaKeys[ModelController.Results.StructureElements[0].Nodes[0].NodeId];
            double x0 = ModelController.Results.Nodes[gsaKeyRefNode].X;
            double y0 = ModelController.Results.Nodes[gsaKeyRefNode].Y;

            int numSides = 2;
            Series[] series = new Series[2];
            series[0] = new Series();
            series[1] = new Series();

            HashSet<Guid> structureNodes = new HashSet<Guid>();
            for (int side = 0; side < numSides; side++)
            {
                foreach (Element1d element1D in ModelController.Results.StructureElements)
                {
                    ElementEndNode node0 = element1D.Nodes[0];
                    ElementEndNode node1 = element1D.Nodes[1];

                    (double, double) pointNode0 = (0, 0), pointNode1 = (0, 0), midPoint = (0, 0);
                    if (ModelController.Results.Nodes.TryGetValue(guidGsaKeys[node0.NodeId], out Node? value0))
                    {
                        pointNode0 = (value0.X, value0.Y);
                    }

                    if (ModelController.Results.Nodes.TryGetValue(guidGsaKeys[node1.NodeId], out Node? value1))
                    {
                        pointNode1 = (value1.X, value1.Y);
                    }
                    midPoint = ((pointNode0.Item1 + pointNode1.Item1) / 2.0, (pointNode0.Item2 + pointNode1.Item2) / 2.0);

                    for (int segment = 0; segment < 2; segment++)
                    {
                        ContactInterface CIResult = element1D.GetContactResult(side, segment);
                        if (segment == 0)
                        {
                            double resultNode0 = GetResult(CIResult, 0);
                            double resultMidNode = GetResult(CIResult, 1);

                            double lengthNode0 = Math.Sqrt((pointNode0.Item1 - x0) * (pointNode0.Item1 - x0) + (pointNode0.Item2 - y0) * (pointNode0.Item2 - y0));
                            double lengthMidNode = Math.Sqrt((midPoint.Item1 - x0) * (midPoint.Item1 - x0) + (midPoint.Item2 - y0) * (midPoint.Item2 - y0));

                            if (_invertAxis)
                            {
                                series[side].Points.AddXY(resultNode0, lengthNode0);
                                series[side].Points.AddXY(resultMidNode, lengthMidNode);
                            }
                            else
                            {
                                series[side].Points.AddXY(lengthNode0, resultNode0);
                                series[side].Points.AddXY(lengthMidNode, resultMidNode);
                            }
                        }
                        else
                        {
                            double resultMidNode = GetResult(CIResult, 0);
                            double resultNode1 = GetResult(CIResult, 1);

                            double lengthNode1 = Math.Sqrt((pointNode1.Item1 - x0) * (pointNode1.Item1 - x0) + (pointNode1.Item2 - y0) * (pointNode1.Item2 - y0));
                            double lengthMidNode = Math.Sqrt((midPoint.Item1 - x0) * (midPoint.Item1 - x0) + (midPoint.Item2 - y0) * (midPoint.Item2 - y0));

                            if (_invertAxis)
                            {
                                series[side].Points.AddXY(resultMidNode, lengthMidNode);
                                series[side].Points.AddXY(resultNode1, lengthNode1);
                            }
                            else
                            {
                                series[side].Points.AddXY(lengthMidNode, resultMidNode);
                                series[side].Points.AddXY(lengthNode1, resultNode1);
                            }
                        }
                    }
                }
                series[side].LegendText = "Side " + side.ToString();
                series[side].ChartType = SeriesChartType.Line;
                series[side].MarkerStyle = MarkerStyle.Circle;

                ChartArea chartArea = chart1.ChartAreas.First();

                chartArea.AxisX.Minimum = series[side].Points.Min(x => x.XValue);
                chartArea.AxisX.Maximum = series[side].Points.Max(x => x.XValue);
                chartArea.AxisX.LabelStyle.Format = "{0:0.000}";

                if (_invertAxis)
                {
                    chartArea.AxisX.Title = _selected_result + " " + _selected_direction;
                    chartArea.AxisY.Title = "Length";
                }
                else
                {
                    chartArea.AxisY.Title = _selected_result + " " + _selected_direction;
                    chartArea.AxisX.Title = "Length";
                }

                chart1.Series.Add(series[side]);
                chart1.Legends[side].Position.Auto = false;
                chartArea.RecalculateAxesScale();
            }


        }
    }
}
