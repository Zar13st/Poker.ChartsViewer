namespace Poker.ChartsViewer.Constants
{
    public static class SecondChartKeyProvider
    {
        public static string GetKey(string groupName, string chartName)
        {
            return $"{groupName}^{chartName}";
        }
    }
}
