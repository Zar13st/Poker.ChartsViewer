using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows.Media;

namespace Poker.ChartsViewer.ViewModel
{
    public class ChartsGroupViewModel : ViewModelBase
    {
        private SolidColorBrush _backGroundColor;

        public string Name { get; set; }

        public string Path { get; set; }

        public List<ChartViewModel> ChartsInGroup { get; } = new List<ChartViewModel>();

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
