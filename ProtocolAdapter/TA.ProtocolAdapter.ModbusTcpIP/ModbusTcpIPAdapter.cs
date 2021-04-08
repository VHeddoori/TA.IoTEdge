using System;
using System.Collections.Generic;
using System.Timers;
using TA.IoTEdge.Common;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP
{
    /// <summary>
    /// The ModbusTcpIPAdapter is responsible for interfacing with the IoTEdgeApplication 
    /// and provides interfaces to read/write/poll for a modbus slave
    /// </summary>
    class ModbusTcpIPAdapter
    {
        #region Data members
        private Connection connParams;
        private ModbusTcpIPMaster modbusTcpIPMaster;
        private Timer poller;
        public event EventHandler OnGetRegisterValues;
        #endregion

        #region Member functions
        public void Connect(string connectionJSON)
        {
            connParams = Connection.Deserialize(connectionJSON);
            modbusTcpIPMaster.Connect(connParams.IPAddress, connParams.Port);
        }

        public void Disconnect()
        {
            modbusTcpIPMaster.Disconnect();
        }

        public void ReadTags(string tagsJSON)
        {
            var tags = Tags.Deserialize(tagsJSON);
            Tags.SetRelativeAddress(tags);

            modbusTcpIPMaster.Read(tags);
        }

        public void WriteTags(string tagsJSON)
        {
            var tags = Tags.Deserialize(tagsJSON);
            Tags.SetRelativeAddress(tags);
        }

        public void PollSlave(string tagsJSON, int PollFrequencyMS)
        {
            if (PollFrequencyMS > 0)
            {
                var tags = Tags.Deserialize(tagsJSON);
                Tags.SetRelativeAddress(tags);

                poller = new Timer(PollFrequencyMS);
                poller.Elapsed += (sender, e) => Poller_Elapsed(tags);
            }
        }

        private void Poller_Elapsed(IEnumerable<Tags> tags)
        {
            modbusTcpIPMaster.Read(tags);

            // Prepare telemetry data to send
            TelemetryData telemetryData = new();

            OnGetRegisterValues?.Invoke(telemetryData, new EventArgs());
        }
        #endregion
    }
}
