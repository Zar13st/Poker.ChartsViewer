using GalaSoft.MvvmLight;
using System.Windows.Media;

namespace Poker.ChartsViewer.ViewModel
{
    public class ChartViewModel : ViewModelBase
    {
        private SolidColorBrush _backGroundColor;

        private string _secondChartPath;

        public string Name { get; set; }

        public string Path { get; set; }

        public string SecondChartPath
        {
            get
            {
                return _secondChartPath;
            }
            set
            {
                if (_secondChartPath != value)
                {
                    _secondChartPath = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        public bool IsMainChart { get; set; }

        public SolidColorBrush BackGroundColor
        {
            get
            {
                return _backGroundColor;
            }
            set
            {
                if (_backGroundColor != value)
                {
                    _backGroundColor = value;
                    base.RaisePropertyChanged();
                }
            }
        }
    }
}
