using GalaSoft.MvvmLight;
using System.Windows.Media;

namespace Poker.ChartsViewer.ViewModel
{
    public class ChartViewModel : ViewModelBase
    {
        private SolidColorBrush _backGroundColor;

        public string Name { get; set; }

        public string Path { get; set; }

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
