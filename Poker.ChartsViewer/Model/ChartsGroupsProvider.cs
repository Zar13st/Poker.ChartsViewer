using Poker.ChartsViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.ChartsViewer.Model
{
    public class ChartsGroupsProvider
    {
        public IEnumerable<ChartsGroupViewModel> GetChartsGroups()
        {
            return new ChartsGroupViewModel[] { new ChartsGroupViewModel() { Name = "11" }, new ChartsGroupViewModel() { Name = "22" } };
        }
    }
}
