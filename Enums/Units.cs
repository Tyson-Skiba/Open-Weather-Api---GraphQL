using System.ComponentModel;

namespace WeatherApi
{
    public enum Units
    {
        [AmbientValue("standard")]
        Standard = 1,

        [AmbientValue("metric")]
        Metric,

        [AmbientValue("imperial")]
        Imperial
    }
}
