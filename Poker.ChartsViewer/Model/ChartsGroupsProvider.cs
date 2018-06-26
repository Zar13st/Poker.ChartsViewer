using Poker.ChartsViewer.Constants;
using Poker.ChartsViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Poker.ChartsViewer.Model
{
    public class ChartsGroupsProvider
    {
        private readonly string _pathToCharts = $"{Environment.CurrentDirectory}\\Data";

        private ButtonsColorManager _buttonsColorManager;
        private SecondChartManager _secondChartManager;

        public ChartsGroupsProvider(ButtonsColorManager buttonsColorManager, SecondChartManager secondChartManager)
        {
            Contract.Assert(buttonsColorManager != null, "buttonsColorManager != null");
            Contract.Assert(secondChartManager != null, "secondChartManager != null");

            _buttonsColorManager = buttonsColorManager;

            _secondChartManager = secondChartManager;
        }

        public IEnumerable<ChartsGroupViewModel> GetChartsGroups()
        {
            if (!Directory.Exists(_pathToCharts))
            {
                Directory.CreateDirectory(_pathToCharts);
            }

            var groups = Directory.EnumerateDirectories(_pathToCharts);

            var chartsGroupViewModels = new List<ChartsGroupViewModel>();

            foreach (var group in groups)
            {
                var groupName = group.Split('\\').Last();

                var chartsGroup = new ChartsGroupViewModel()
                {
                    Path = group,
                    Name = groupName,
                    BackGroundColor = _buttonsColorManager.GetButtonBackGroundColor(groupName),
                };

                var charts = Directory.EnumerateFiles(group);

                foreach(var chart in charts)
                {
                    var chartName = chart.Split('\\').Last().Split('.').First();

                    chartsGroup.ChartsInGroup.Add(new ChartViewModel()
                    {
                        Path = chart,
                        Name = chartName,
                        BackGroundColor = _buttonsColorManager.GetButtonBackGroundColor(chartName),
                        SecondChartPath = _secondChartManager.GetSecondChartPath(SecondChartKeyProvider.GetKey(groupName, chartName)),
                    });
                }

                chartsGroupViewModels.Add(chartsGroup);
            }

            return chartsGroupViewModels;
        }
    }
}
