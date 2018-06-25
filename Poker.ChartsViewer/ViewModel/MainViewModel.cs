using GalaSoft.MvvmLight;

namespace Poker.Charts.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            LeftChartsManager = new ChartsManagerViewModel();

            RightChartsManager = new ChartsManagerViewModel();
        }

        public ChartsManagerViewModel LeftChartsManager { get; private set; } 

        public ChartsManagerViewModel RightChartsManager { get; private set; }

        public void Begin()
        {
            LeftChartsManager.GetChartsGroups();

            RightChartsManager.GetChartsGroups();
        }
    }
}
