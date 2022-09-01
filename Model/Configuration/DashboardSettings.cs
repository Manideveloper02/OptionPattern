namespace OptionPattern.Model.Configuration
{
    public class DashboardSettings
    {
        public string Title { get; set; }
        public Header Header { get; set; }
    }

    public class Header
    {
        public bool IsSearchBoxEnabled { get; set; }
        public string BannerTitle { get; set; }
        public bool IsBannerSliderEnabled { get; set; }
    }
}
