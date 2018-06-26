using Poker.ChartsViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;

using System.Windows.Media;

namespace Poker.ChartsViewer.Model
{
    public class ButtonsColorManager
    {
        private string _pathToColorFile = $"{Environment.CurrentDirectory}\\Colors\\BackColor.txt";

        private char _nameAndColorSpliter = '|';

        private char _colorSpliter = ',';

        private Dictionary<string, SolidColorBrush> _buttonColorByName = new Dictionary<string, SolidColorBrush>();

        public void ReadButtonsColorFromFile()
        {
            if (!File.Exists(_pathToColorFile))
            {
                var fileStrem =  File.Create(_pathToColorFile);

                fileStrem.Close();
            }

            foreach(var lines in File.ReadAllLines(_pathToColorFile))
            {
                var linesParts = lines.Split(_nameAndColorSpliter);

                var name = linesParts[0];

                var colorParts = linesParts[1].Split(_colorSpliter);

                if (colorParts.Length != 4)
                {
                    colorParts = new string[] {"255","0", "0", "0" };
                }

                var isParsedA = byte.TryParse(colorParts[0], out byte a);
                if (!isParsedA)
                {
                    a = 255;
                }

                var isParsedR = byte.TryParse(colorParts[1], out byte r);
                if (!isParsedA)
                {
                    r = 0;
                }

                var isParsedG = byte.TryParse(colorParts[2], out byte g);
                if (!isParsedA)
                {
                    g = 0;
                }

                var isParsedB = byte.TryParse(colorParts[3], out byte b);
                if (!isParsedA)
                {
                    b = 0;
                }

                var color = Color.FromArgb(a, r, g, b);

                _buttonColorByName[name] = new SolidColorBrush(color);
            }
        }

        public SolidColorBrush GetButtonBackGroundColor(string name)
        {
            if (_buttonColorByName.ContainsKey(name))
            {
                return _buttonColorByName[name];

            }
            else
            {
                return new SolidColorBrush(Colors.White);
            }
        }

        public void UpdateGroupBackGroundColor(ChartsGroupViewModel group)
        {
            _buttonColorByName[group.Name] = group.BackGroundColor;

            UpdateFile();
        }

        private void UpdateFile()
        {
            using(var file = new StreamWriter(_pathToColorFile))
            {
                foreach(var line in _buttonColorByName)
                {
                    file.WriteLine($"{line.Key}{_nameAndColorSpliter}{line.Value.Color.A}{_colorSpliter}{line.Value.Color.R}{_colorSpliter}{line.Value.Color.G}{_colorSpliter}{line.Value.Color.B}");
                }
            }
        }
    }
}
