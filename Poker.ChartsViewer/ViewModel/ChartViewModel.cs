﻿using System.Windows.Media;

namespace Poker.ChartsViewer.ViewModel
{
    public class ChartViewModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsMainChart { get; set; }

        public SolidColorBrush BackGroundColor { get; set; }
    }
}
