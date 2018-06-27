using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Poker.ChartsViewer.Constants;
using Poker.ChartsViewer.Model;
using Poker.ChartsViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Poker.Charts.ViewModel
{
    public class ChartsManagerViewModel : ViewModelBase
    {
        private string _currentMainChart;
        private string _currentSecondaryChart;

        private ChartsGroupViewModel _currentGroup;
        private ChartViewModel _currentChart;

        private ButtonsColorManager _buttonsColorManager;
        private SecondChartManager _secondChartManager;
        private ChartsGroupsProvider _chartsGroupsProvider;

        private RelayCommand<ChartsGroupViewModel> _selectedGroupChangedCommand;
        private RelayCommand<ChartViewModel> _selectedChartChangedCommand;

        private RelayCommand _changeGroupBackGroundColorCommand;
        private RelayCommand _changeChartBackGroundColorCommand;
        private RelayCommand _setSecondChartCommand;
        private RelayCommand _deleteSecondChartCommand; 
        private RelayCommand _setMainCommand;

        public ChartsManagerViewModel(ButtonsColorManager buttonsColorManager, SecondChartManager secondChartManager)
        {
            Contract.Assert(buttonsColorManager != null, "buttonsColorManager != null");
            Contract.Assert(secondChartManager != null, "secondChartManager != null");

            _buttonsColorManager = buttonsColorManager;

            _secondChartManager = secondChartManager;

            _chartsGroupsProvider = new ChartsGroupsProvider(buttonsColorManager, secondChartManager);
        }

        public ObservableCollection<ChartsGroupViewModel> ChartsGroups { get; set; } = new ObservableCollection<ChartsGroupViewModel>();

        public ObservableCollection<ChartViewModel> CurrentGroupCharts { get; set; } = new ObservableCollection<ChartViewModel>();

        public event Func<string> OnSecondChartAdding;

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

        public string SecondChartPath
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
            ChartsGroups.Clear();

            foreach (var chartsGroup in _chartsGroupsProvider.GetChartsGroups())
            {
                ChartsGroups.Add(chartsGroup);
            }
        }

        public string GetSecondChartPath()
        {
            if (_currentChart != null)
            {
                return _currentChart.Path;
            }
            else
            {
                return string.Empty;
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
                        SecondChartPath = _currentChart.SecondChartPath;
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
                        SecondChartPath = _currentChart.SecondChartPath;
                    }
                }));
            }
        }

        public RelayCommand ChangeGroupBackGroundColorCommand
        {
            get
            {
                return _changeGroupBackGroundColorCommand ?? (_changeGroupBackGroundColorCommand = new RelayCommand(() =>
                {
                    var MyDialog = new System.Windows.Forms.ColorDialog();

                    if (MyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var wpfColor = Color.FromArgb(MyDialog.Color.A, MyDialog.Color.R, MyDialog.Color.G, MyDialog.Color.B);

                        _currentGroup.BackGroundColor = new SolidColorBrush(wpfColor);
                    }

                    _buttonsColorManager.UpdateGroupBackGroundColor(_currentGroup.Name, _currentGroup.BackGroundColor);
                }));
            }
        }

        public RelayCommand ChangeChartBackGroundColorCommand
        {
            get
            {
                return _changeChartBackGroundColorCommand ?? (_changeChartBackGroundColorCommand = new RelayCommand(() =>
                {
                    var MyDialog = new System.Windows.Forms.ColorDialog();

                    if (MyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var wpfColor = Color.FromArgb(MyDialog.Color.A, MyDialog.Color.R, MyDialog.Color.G, MyDialog.Color.B);

                        _currentChart.BackGroundColor = new SolidColorBrush(wpfColor);
                    }

                    _buttonsColorManager.UpdateGroupBackGroundColor(_currentChart.Name, _currentChart.BackGroundColor);
                }));
            }
        }

        public RelayCommand SetSecondChartCommand
        {
            get
            {
                return _setSecondChartCommand ?? (_setSecondChartCommand = new RelayCommand(() =>
                {
                    var secondChartPath = OnSecondChartAdding?.Invoke();

                    SecondChartPath = secondChartPath;

                    _currentChart.SecondChartPath = secondChartPath;

                    _secondChartManager.UpdateSecondCharts(SecondChartKeyProvider.GetKey(_currentGroup.Name, _currentChart.Name), secondChartPath);
                }));
            }
        }
 
        public RelayCommand DeleteSecondChartCommand
        {
            get
            {
                return _deleteSecondChartCommand ?? (_deleteSecondChartCommand = new RelayCommand(() =>
                {
                    SecondChartPath = string.Empty;

                    _currentChart.SecondChartPath = string.Empty;

                    _secondChartManager.DeleteSecondCharts(SecondChartKeyProvider.GetKey(_currentGroup.Name, _currentChart.Name));
                }));
            }
        }

        public RelayCommand SetMainCommand
        {
            get
            {
                return _setMainCommand ?? (_setMainCommand = new RelayCommand(() =>
                {
                    foreach(var chart in _currentGroup.ChartsInGroup)
                    {
                        chart.IsMainChart = false;
                    }

                    _currentChart.IsMainChart = true;


                }));
            }
        }

    }
}
