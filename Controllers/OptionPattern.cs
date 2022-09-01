using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OptionPattern.Model.Configuration;

namespace OptionPattern.Controllers
{
    //Use IOptions<T> when you are not expecting your config values to change.
    //Use IOptionsSnaphot<T> when you are expecting your values to change but want it to be consistent for the entirety of a request.
    //Use IOptionsMonitor<T> when you need real time values.
    [ApiController]
    [Route("[controller]")]
    public class OptionPattern : ControllerBase
    {
        private readonly DashboardSettings _dashboardSettings;
        private readonly DashboardSettings _dashboardSettingsSnapshot;
        private readonly DashboardSettings _monitor;
        public OptionPattern(IOptions<DashboardSettings> dashboardSettings, IOptionsSnapshot<DashboardSettings> dashboardSettingsSnapshot, IOptionsMonitor<DashboardSettings> monitor)
        {
            _dashboardSettings = dashboardSettings.Value;
            _dashboardSettingsSnapshot = dashboardSettingsSnapshot.Value;
            _monitor = monitor.CurrentValue;
        }
        
        [HttpGet("IOptions")]
        public string Get()
        {
            return _dashboardSettings.Title;
        }
        //IOptionsSnapshot has a scoped life time to guarantee you have the same configuration
        //values during a single request. It could be odd behavior if the content changes in the
        //mid request. Then one part of your request would apply the old values and the second part
        //after the changes would apply new values, leading to conflicts and hard to track bugs.
        //For everything that's related (or executed during) a request, you should use the snappshot
        //Options snapshots are designed for use with transient and scoped dependencies.
        [HttpGet("IOptionsSnapshot")]
        public string GetSnapshot()
        {
            return _dashboardSettingsSnapshot.Title;
        }
        //Essentially the monitors is for notifications whereas
        //the snapshot is a cached version/snapshot of the IOptionsMonitor<T>
        //and doesnt change during the request
        //IOptionsMonitor is a singleton service

        [HttpGet("IOptionsMonitor")]
        public string GetMonitor()
        {
            return _monitor.Title;
        }
    }
}
