using System.Collections.Generic;

namespace Poker.ChartsViewer.ViewModel
{
    public class ChartsGroupViewModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public List<ChartViewModel> ChartsInGroup { get; } = new List<ChartViewModel>();
    }
}
