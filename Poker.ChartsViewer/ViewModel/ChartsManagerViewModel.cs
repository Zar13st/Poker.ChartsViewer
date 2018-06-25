using GalaSoft.MvvmLight;
using Poker.ChartsViewer.Model;
using Poker.ChartsViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Charts.ViewModel
{
    public class ChartsManagerViewModel : ViewModelBase
    {
        private string _currentMainChart;
        private string _currentSecondaryChart;

        private ChartsGroupsProvider _chartsGroupsProvider;

        public ChartsManagerViewModel()
        {
            _chartsGroupsProvider = new ChartsGroupsProvider();
        }

        public ObservableCollection<ChartsGroupViewModel> ChartsGroups { get; set; } = new ObservableCollection<ChartsGroupViewModel>();

        public ObservableCollection<ChartViewModel> CurrentGroupCharts { get; set; } = new ObservableCollection<ChartViewModel>();

        public string CurrentMainChart
        {
            get
            {
                return _currentMainChart;
            }
            set
            {
                if (_currentMainChart != value)
                {
                    _currentMainChart = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        public string CurrentSecondaryChart
        {
            get
            {
                return _currentSecondaryChart;
            }
            set
            {
                if (_currentSecondaryChart != value)
                {
                    _currentSecondaryChart = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        
        public void GetChartsGroups()
        {
            foreach (var chartsGroup in _chartsGroupsProvider.GetChartsGroups())
            {
                ChartsGroups.Add(chartsGroup);
            }
        }


    }
}
