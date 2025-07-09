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
using System.Xml.Linq;


namespace StructureResultsViewer
{
    public class Controller
    {
        private AnalysisResult? _analysisResult;


        public AnalysisResult? Results { get { return _analysisResult; } }


        public Controller() { }


        public void LoadResultsJson(FileInfo resultsFile)
        {
            if (resultsFile != null)
            {
                string json = File.ReadAllText(resultsFile.ToString());
                if (resultsFile.DirectoryName != null)
                {
                    var dir = new DirHelper(resultsFile.DirectoryName.ToString());
                    _analysisResult = dir.ReadData<AnalysisResult>(resultsFile.Name);
                }
            }
        }
    }
}
