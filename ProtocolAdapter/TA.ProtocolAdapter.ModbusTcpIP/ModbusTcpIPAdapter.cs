using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TA.IoTEdge.Common;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP
{
    class ModbusTcpIPAdapter
    {
        #region Data members
        private Connection connParams;
        private ModbusTcpIPMaster modbusTcpIPMaster;
        private Timer poller;
        public event EventHandler OnGetRegisterValues;
        #endregion

        #region Member functions
        public void Connect(string connectionDetails)
        {
            connParams = Connection.Read(connectionDetails);
            modbusTcpIPMaster.Connect(connParams.IPAddress, connParams.Port);
        }

        public void Disconnect()
        {
            modbusTcpIPMaster.Disconnect();
        }

        public void ReadTags(string tagDetails)
        {
            var tags = Tag.Read(tagDetails);

            modbusTcpIPMaster.ReadRegisters(tags);
        }

        public void WriteTags(string tagDetails)
        {
            var tags = Tag.Read(tagDetails);
        }

        public void Poll(string tagDetails, int PollFrequencyMS = 0)
        {
            if (PollFrequencyMS > 0)
            {
                var tags = Tag.Read(tagDetails);
                poller = new Timer(PollFrequencyMS);
                poller.Elapsed += (sender, e) => Poller_Elapsed(tags);
            }
        }

        private void Poller_Elapsed(IEnumerable<Tag> tags)
        {
            modbusTcpIPMaster.ReadRegisters(tags);

            // Prepare telemetry data to send
            TelemetryData telemetryData = new TelemetryData();
            OnGetRegisterValues?.Invoke(telemetryData, new EventArgs());
        }

        #endregion
    }
}
