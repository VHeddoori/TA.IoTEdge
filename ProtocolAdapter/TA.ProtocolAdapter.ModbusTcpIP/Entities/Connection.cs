using System.Text.Json;

namespace TA.ProtocolAdapter.ModbusTcpIP.Entities
{
    class Connection
    {
        #region Data members
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public ushort SlaveId { get; set; }
        #endregion


        #region Member functions
        public static Connection Read(string connectionString)
        {
            return JsonSerializer.Deserialize<Connection>(connectionString);
        }
        #endregion

    }
}
