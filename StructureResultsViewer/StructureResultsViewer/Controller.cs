using GoferAnalysisDTOs.Models;
using GoferAnalysisDTOs.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;


namespace StructureResultsViewer
{
    public class Controller
    {
        private AnalysisResult? _analysisResult;
        private HashSet<int> _elemSelection;
        private HashSet<int> _nodeSelection;
        private Dictionary<Guid, int> _guidGsaKeys;
        public AnalysisResult? Results { get { return _analysisResult; } }

        public Controller()
        {
            _elemSelection = new HashSet<int>();
            _nodeSelection = new HashSet<int>();
            _guidGsaKeys = new Dictionary<Guid, int>();
        }

        public void SetNodeSelection(HashSet<int> elemSelection)
        {
            if ((_analysisResult == null) || (_guidGsaKeys == null))
                return;
            _nodeSelection.Clear();
            _elemSelection = new HashSet<int>(elemSelection);
            foreach (Element1d element1D in _analysisResult.StructureElements)
            {
                if (elemSelection.Contains(element1D.GsaModelKey))
                {
                    ElementEndNode node0 = element1D.Nodes.First();
                    ElementEndNode node1 = element1D.Nodes.Last();
                    _nodeSelection.Add(_guidGsaKeys[node0.NodeId]);
                    _nodeSelection.Add(_guidGsaKeys[node1.NodeId]);
                }
            }
        }

        public bool IsSelected(int nodeIndex)
        {
            return _nodeSelection.Contains(nodeIndex);
        }

        // Clearing selection means all nodes get displayed.
        public void ClearNodeSelection()
        {
            _nodeSelection.Clear();
            if ((_analysisResult != null) && (_analysisResult.Nodes != null))
            {
                foreach (var kvp in _analysisResult.Nodes)
                {
                    _nodeSelection.Add(kvp.Value.GsaModelKey);
                }
            }
        }
        public void ClearElementSelection()
        {
            _elemSelection.Clear();
            if ((_analysisResult != null) && (_analysisResult.Nodes != null))
            {
                foreach (var elem in _analysisResult.StructureElements)
                {
                    _elemSelection.Add(elem.GsaModelKey);
                }
            }
        }

        public void UpdateNodeTable()
        {
            if (_analysisResult != null)
            {
                _guidGsaKeys = new Dictionary<Guid, int>(_analysisResult.Nodes.Count);

                foreach (Node node in _analysisResult.Nodes.Values)
                {
                    _guidGsaKeys.Add(node.Id, node.GsaModelKey);
                }
            }
        }

        public void LoadResultsJson(FileInfo resultsFile)
        {
            if (resultsFile != null)
            {
                String json = File.ReadAllText(resultsFile.ToString());
                if (resultsFile.DirectoryName != null)
                {
                    var dir = new DirHelper(resultsFile.DirectoryName.ToString());
                    _analysisResult = dir.ReadData<AnalysisResult>(resultsFile.Name);
                }
                UpdateNodeTable();
                ClearNodeSelection();
                ClearElementSelection();
            }
        }
        private double GetResult(String selectedResult, ContactInterface CIResult, int index)
        {
            if ((selectedResult == "Reactions (contacts)") || (selectedResult == "Relative displacement (contacts)"))
            {
                List<Displacement> interfaceResult;
                if (selectedResult == "Reactions (contacts)")
                {
                    interfaceResult = CIResult.Reaction;
                }
                else
                {
                    interfaceResult = CIResult.RelativeDisplacement;
                }
                return GetValue(interfaceResult[index], selectedResult);
            }
            else
            {
                if (selectedResult == "Steady pore-pressure (contacts)")
                {
                    return CIResult.SteadyStatePorePressure[index];
                }
                else
                {
                    return CIResult.ExcessPorePressure[index];
                }
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

        public List<(int, double, double)> ReadContactResults(String selectedResult)
        {
            List<(int, double, double)> contactResults = new List<(int, double, double)>();
            const int numSides = 2;
            if (_analysisResult == null)
            {
                return contactResults;
            }

            Dictionary<Guid, int> guidGsaKeys = new Dictionary<Guid, int>(_analysisResult.Nodes.Count);

            foreach (Node node in _analysisResult.Nodes.Values)
            {
                guidGsaKeys.Add(node.Id, node.GsaModelKey);
            }

            int gsaKeyRefNode = guidGsaKeys[_analysisResult.StructureElements[0].Nodes[0].NodeId];
            double x0 = _analysisResult.Nodes[gsaKeyRefNode].X;
            double y0 = _analysisResult.Nodes[gsaKeyRefNode].Y;

            HashSet<Guid> structureNodes = new HashSet<Guid>();
            for (int side = 0; side < numSides; side++)
            {
                foreach (Element1d element1D in _analysisResult.StructureElements)
                {
                    if (_elemSelection.Contains(element1D.GsaModelKey))
                    {
                        ElementEndNode node0 = element1D.Nodes[0];
                        ElementEndNode node1 = element1D.Nodes[1];

                        (double, double) pointNode0 = (0, 0), pointNode1 = (0, 0), midPoint = (0, 0);
                        if (_analysisResult.Nodes.TryGetValue(guidGsaKeys[node0.NodeId], out Node? value0))
                        {
                            pointNode0 = (value0.X, value0.Y);
                        }

                        if (_analysisResult.Nodes.TryGetValue(guidGsaKeys[node1.NodeId], out Node? value1))
                        {
                            pointNode1 = (value1.X, value1.Y);
                        }
                        midPoint = ((pointNode0.Item1 + pointNode1.Item1) / 2.0, (pointNode0.Item2 + pointNode1.Item2) / 2.0);

                        for (int segment = 0; segment < 2; segment++)
                        {
                            ContactInterface CIResult = element1D.GetContactResult(side, segment);
                            if (segment == 0)
                            {
                                double resultNode0 = GetResult(selectedResult, CIResult, 0);
                                double resultMidNode = GetResult(selectedResult, CIResult, 1);

                                double lengthNode0 = Math.Sqrt((pointNode0.Item1 - x0) * (pointNode0.Item1 - x0) + (pointNode0.Item2 - y0) * (pointNode0.Item2 - y0));
                                double lengthMidNode = Math.Sqrt((midPoint.Item1 - x0) * (midPoint.Item1 - x0) + (midPoint.Item2 - y0) * (midPoint.Item2 - y0));

                                contactResults.Add((side, lengthNode0, resultNode0));
                                contactResults.Add((side, lengthMidNode, resultMidNode));

                            }
                            else
                            {
                                double resultMidNode = GetResult(selectedResult, CIResult, 0);
                                double resultNode1 = GetResult(selectedResult, CIResult, 1);

                                double lengthNode1 = Math.Sqrt((pointNode1.Item1 - x0) * (pointNode1.Item1 - x0) + (pointNode1.Item2 - y0) * (pointNode1.Item2 - y0));
                                double lengthMidNode = Math.Sqrt((midPoint.Item1 - x0) * (midPoint.Item1 - x0) + (midPoint.Item2 - y0) * (midPoint.Item2 - y0));
                                contactResults.Add((side, lengthMidNode, resultMidNode));
                                contactResults.Add((side, lengthNode1, resultNode1));

                            }
                        }
                    }
                }
            }
            return contactResults;
        }

        public List<(double, double)> ReadNodalResults(String selectedResult)
        {
            List<(double, double)> nodalResults = new List<(double, double)>();
            HashSet<Guid> structureNodes = new HashSet<Guid>();

            if (_analysisResult == null)
                return nodalResults;

            int gsaKeyRefNode = _guidGsaKeys[_analysisResult.StructureElements[0].Nodes[0].NodeId];
            double x0 = _analysisResult.Nodes[gsaKeyRefNode].X;
            double y0 = _analysisResult.Nodes[gsaKeyRefNode].Y;

            foreach (Element1d element1D in _analysisResult.StructureElements)
            {
                foreach (ElementEndNode node in element1D.Nodes)
                {
                    int gsaModelKey = _guidGsaKeys[node.NodeId];
                    if (_nodeSelection.Contains(gsaModelKey))
                    {
                        if (!structureNodes.Contains(node.NodeId))
                        {
                            structureNodes.Add(node.NodeId);
                        }
                    }
                }
            }
            foreach (Guid guid in structureNodes)
            {
                if (_analysisResult.Nodes.TryGetValue(_guidGsaKeys[guid], out Node? value))
                {
                    double length = Math.Sqrt((value.X - x0) * (value.X - x0) + (value.Y - y0) * (value.Y - y0));
                    double result = 0;
                    switch (selectedResult)
                    {
                        case "Total displacement":
                            result = GetValue(value.Displacement, selectedResult);
                            break;
                        case "Stage displacement":
                            result = GetValue(value.StageDisplacement, selectedResult);
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
                    nodalResults.Add((length, result));
                }
            }
            return nodalResults;
        }
    }
}