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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            List<(double, double)> nodalResults = ModelController.ReadNodalResults(_selected_result);

            Series series = new Series();
            series.LegendText = _selected_result;
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;

            if (_invertAxis)
            {
                foreach (var point in nodalResults)
                {
                    series.Points.AddXY(point.Item2, point.Item1);
                }
            }
            else
            {
                foreach (var point in nodalResults)
                {
                    series.Points.AddXY(point.Item1, point.Item2);
                }
            }
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
            if ((ModelController == null) || (ModelController.Results == null))
            {
                return;
            }

            List<(int, double, double)> contactResults = ModelController.ReadContactResults(_selected_result);

            int numSides = 2;
            Series[] series = new Series[2];
            series[0] = new Series();
            series[1] = new Series();

            if (_invertAxis)
            {
                foreach (var point in contactResults)
                {
                    series[point.Item1].Points.AddXY(point.Item3, point.Item2);
                }
            }
            else
            {
                foreach (var point in contactResults)
                {
                    series[point.Item1].Points.AddXY(point.Item2, point.Item3);
                }
            }

            for (int side = 0; side < numSides; side++)
            {
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

        private void button1_Click(object sender, EventArgs e)
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

            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "c:\\";
                    saveFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (_selected_result == "Reactions (contacts)" || _selected_result == "Relative displacement (contacts)" || _selected_result == "Steady pore-pressure (contacts)" | _selected_result == "Excess pore-pressure (contacts)")
                        {
                            List<(int, double, double)> contactResults = ModelController.ReadContactResults(_selected_result);
                            List<(int, double, double)> contactResultsSide0 = contactResults.Where(x => x.Item1 == 0).OrderBy(x => x.Item2).ToList();
                            List<(int, double, double)> contactResultsSide1 = contactResults.Where(x => x.Item1 == 1).OrderBy(x => x.Item2).ToList();
                            using (StreamWriter writetext = new StreamWriter(saveFileDialog.FileName))
                            {
                                for (int i = 0; i < contactResultsSide0.Count; i++)
                                {
                                    if (contactResultsSide0[i].Item2 == contactResultsSide0[i].Item2)
                                    {
                                        writetext.WriteLine("{0},{1},{2}", contactResultsSide0[i].Item2, contactResultsSide0[i].Item3, contactResultsSide1[i].Item3);
                                    }
                                    else
                                    {
                                        throw new InvalidDataException("Data could not be organised for export");
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<(double, double)> nodalResults = ModelController.ReadNodalResults(_selected_result);
                            using (StreamWriter writetext = new StreamWriter(saveFileDialog.FileName))
                            {
                                foreach (var point in nodalResults)
                                {
                                    writetext.WriteLine("{0},{1}", point.Item1, point.Item2);
                                }
                            }
                        }
                        MessageBox.Show("CSV file successfully saved!", "File output", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
