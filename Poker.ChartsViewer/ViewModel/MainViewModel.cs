using GalaSoft.MvvmLight;
using Poker.ChartsViewer.Model;

namespace Poker.Charts.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _buttonColorManager  = new ButtonsColorManager();

            LeftChartsManager = new ChartsManagerViewModel(_buttonColorManager);

            RightChartsManager = new ChartsManagerViewModel(_buttonColorManager);
        }

        private ButtonsColorManager _buttonColorManager;

        public ChartsManagerViewModel LeftChartsManager { get; private set; } 

        public ChartsManagerViewModel RightChartsManager { get; private set; }

        public void Begin()
        {
            _buttonColorManager.ReadButtonsColorFromFile();

            LeftChartsManager.GetChartsGroups();

            RightChartsManager.GetChartsGroups();
        }
    }
}
