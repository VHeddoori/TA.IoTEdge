using System.Collections.Generic;
using TA.IoTEdge.Common.Entities;

namespace TA.IoTEdge.Common
{
    public class TelemetryData
    {
        public int ConnectionId { get; set; }
        public IEnumerable<TagData> Tags { get; }

        public TelemetryData()
        {
            Tags = new List<TagData>();
        }
    }
}
