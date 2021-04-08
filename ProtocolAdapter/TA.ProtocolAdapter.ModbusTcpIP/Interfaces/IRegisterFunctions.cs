using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.Interfaces
{
    interface IRegisterFunctions
    {
        void ReadRegisters(IEnumerable<Tags> tags);
        void WriteRegisters(IEnumerable<Tags> tags);
    }
}
