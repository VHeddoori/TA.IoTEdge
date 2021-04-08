using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.Registers
{
    abstract class ModbusRegister
    {
        public abstract List<RegisterBlock> GetRegisterBlocks(IEnumerable<Tags> tags);
        public abstract void ReadRegisters(IEnumerable<Tags> tags);
        public abstract void WriteRegisters(IEnumerable<Tags> tags);
    }
}
