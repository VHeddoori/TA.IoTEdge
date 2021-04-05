using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.Registers
{
    interface IRegister
    {
        void Read(IEnumerable<Tag> tags);
        void Write(IEnumerable<Tag> tags);
    }
}
