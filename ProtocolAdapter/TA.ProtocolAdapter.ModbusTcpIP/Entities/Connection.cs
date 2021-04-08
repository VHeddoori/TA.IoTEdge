using System.Text.Json;

namespace TA.ProtocolAdapter.ModbusTcpIP.Entities
{
    class Connection
    {
        #region Data members
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public byte SlaveId { get; set; }
        #endregion


        #region Member functions
        public static Connection Deserialize(string connectionJSON)
        {
            return JsonSerializer.Deserialize<Connection>(connectionJSON);
        }
        #endregion

    }
}
