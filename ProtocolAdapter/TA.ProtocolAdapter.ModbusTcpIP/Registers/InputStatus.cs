using NModbus;
using System;
using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.Registers
{
    class InputStatus : ModbusRegister
    {
        private IConcurrentModbusMaster concurrentMaster;
        private byte slaveAddress;

        #region Ctor
        public InputStatus(IConcurrentModbusMaster concurrentMaster, byte slaveAddress)
        {
            this.concurrentMaster = concurrentMaster;
            this.slaveAddress = slaveAddress;
        }
        #endregion
        public override List<RegisterBlock> GetRegisterBlocks(IEnumerable<Tags> tags)
        {
            return RegisterBlock.GetBlocks(tags);
        }

        public override void ReadRegisters(IEnumerable<Tags> tags)
        {
            throw new NotImplementedException();
        }

        public override void WriteRegisters(IEnumerable<Tags> tags)
        {
            throw new NotSupportedException();
        }
    }
}
