using Poker.ChartsViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Poker.ChartsViewer.Model
{
    public class ChartsGroupsProvider
    {
        private readonly string _pathToCharts = $"{Environment.CurrentDirectory}\\Data";

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
                var chartsGroup = new ChartsGroupViewModel()
                {
                    Path = group,
                    Name = group.Split('\\').Last(),
                };

                var charts = Directory.EnumerateFiles(group);

                foreach(var chart in charts)
                {
                    chartsGroup.ChartsInGroup.Add(new ChartViewModel()
                    {
                        Path = chart,
                        Name = chart.Split('\\').Last().Split('.').First(),
                    });
                }

                chartsGroupViewModels.Add(chartsGroup);
            }

            return chartsGroupViewModels;
        }
    }
}
