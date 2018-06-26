using System;
using System.Collections.Generic;
using System.IO;

namespace Poker.ChartsViewer.Model
{
    public class SecondChartManager
    {
        private string _pathToColorFile = $"{Environment.CurrentDirectory}\\SecondCharts\\SecondCharts.txt";

        private char _spliter = '|';

        private Dictionary<string, string> _secondChartPathByName = new Dictionary<string, string>();

        public void ReadSecondChartsFromFile()
        {
            if (!Directory.Exists($"{Environment.CurrentDirectory}\\SecondCharts"))
            {
                Directory.CreateDirectory($"{Environment.CurrentDirectory}\\SecondCharts");
            }

            if (!File.Exists(_pathToColorFile))
            {
                var fileStrem = File.Create(_pathToColorFile);

                fileStrem.Close();
            }

            foreach (var lines in File.ReadAllLines(_pathToColorFile))
            {
                var linesParts = lines.Split(_spliter);

                var name = linesParts[0];

                var secondChartPath = linesParts[1];

                _secondChartPathByName[name] = secondChartPath;
            }
        }

        public string GetSecondChartPath(string name)
        {
            if (_secondChartPathByName.ContainsKey(name))
            {
                return _secondChartPathByName[name];
            }
            else
            {
                return string.Empty;
            }
        }

        public void UpdateSecondCharts(string groupAndName, string secondChartPath)
        {
            _secondChartPathByName[groupAndName] = secondChartPath;

            UpdateFile();
        }

        public void DeleteSecondCharts(string groupAndName)
        {
            if (_secondChartPathByName.ContainsKey(groupAndName))
            {
                _secondChartPathByName.Remove(groupAndName);

                UpdateFile();
            }
        }

        private void UpdateFile()
        {
            using (var file = new StreamWriter(_pathToColorFile))
            {
                foreach (var line in _secondChartPathByName)
                {
                    file.WriteLine($"{line.Key}{_spliter}{line.Value}");
                }
            }
        }

    }
}
