using GalaSoft.MvvmLight;

namespace Poker.Charts.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public ChartsManagerViewModel LeftChartsManager { get; set; } = new ChartsManagerViewModel();

        public ChartsManagerViewModel RightChartsManager { get; set; } = new ChartsManagerViewModel();
    }
}
