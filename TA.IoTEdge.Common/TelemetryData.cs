using System.Collections.Generic;
using TA.IoTEdge.Common.Entities;

namespace TA.IoTEdge.Common
{
    public class TelemetryData
    {
        public int ConnectionId { get; set; }
        public IEnumerable<Tag> Tags { get; }

        public TelemetryData()
        {
            Tags = new List<Tag>();
        }
    }
}
