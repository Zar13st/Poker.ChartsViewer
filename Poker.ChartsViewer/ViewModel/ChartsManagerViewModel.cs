using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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

        private ChartsGroupViewModel _currentGroup;
        private ChartViewModel _currentChart;

        private ChartsGroupsProvider _chartsGroupsProvider;

        private RelayCommand<ChartsGroupViewModel> _selectedGroupChangedCommand;
        private RelayCommand<ChartViewModel> _selectedChartChangedCommand;

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


        public RelayCommand<ChartsGroupViewModel> SelectedGroupChangedCommand
        {
            get
            {
                return _selectedGroupChangedCommand ?? (_selectedGroupChangedCommand = new RelayCommand<ChartsGroupViewModel>((selectedItem) =>
                {
                    _currentGroup = selectedItem;

                    CurrentGroupCharts.Clear();

                    foreach (var chart in selectedItem.ChartsInGroup)
                    {
                        CurrentGroupCharts.Add(chart);
                    }

                    if (CurrentGroupCharts.Count != 0)
                    {
                        _currentChart = CurrentGroupCharts.FirstOrDefault(c => c.IsMainChart);

                        if (_currentChart == null)
                        {
                            _currentChart = CurrentGroupCharts.FirstOrDefault();
                        }

                        CurrentMainChart = _currentChart.Path;
                    }
                }));
            }
        }

        public RelayCommand<ChartViewModel> SelectedChartChangedCommand
        {
            get
            {
                return _selectedChartChangedCommand ?? (_selectedChartChangedCommand = new RelayCommand<ChartViewModel>((selectedItem) =>
                {
                    if (selectedItem != null)
                    {
                        _currentChart = selectedItem;

                        CurrentMainChart = selectedItem.Path;
                    }
                }));
            }
        }

    }
}
