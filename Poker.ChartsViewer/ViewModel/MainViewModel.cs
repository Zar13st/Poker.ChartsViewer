using GalaSoft.MvvmLight;
using Poker.ChartsViewer.Model;

namespace Poker.Charts.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _buttonColorManager  = new ButtonsColorManager();

            _secondChartManager = new SecondChartManager();

            LeftChartsManager = new ChartsManagerViewModel(_buttonColorManager, _secondChartManager);

            RightChartsManager = new ChartsManagerViewModel(_buttonColorManager, _secondChartManager);
        }

        private ButtonsColorManager _buttonColorManager;
        private SecondChartManager _secondChartManager;

        public ChartsManagerViewModel LeftChartsManager { get; private set; } 

        public ChartsManagerViewModel RightChartsManager { get; private set; }

        public void Begin()
        {
            _buttonColorManager.ReadButtonsColorFromFile();

            _secondChartManager.ReadSecondChartsFromFile();

            LeftChartsManager.OnSecondChartAdding += RightChartsManager.GetSecondChartPath;

            RightChartsManager.OnSecondChartAdding += LeftChartsManager.GetSecondChartPath;

            LeftChartsManager.GetChartsGroups();

            RightChartsManager.GetChartsGroups();
        }
    }
}
