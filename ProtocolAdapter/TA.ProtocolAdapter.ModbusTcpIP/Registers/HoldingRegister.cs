using NModbus;
using System;
using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;
using TA.ProtocolAdapter.ModbusTcpIP.Enums;
using TA.ProtocolAdapter.ModbusTcpIP.Interfaces;

namespace TA.ProtocolAdapter.ModbusTcpIP.Registers
{
    class HoldingRegister : ModbusRegister
    {
        private IConcurrentModbusMaster concurrentMaster;
        private byte slaveAddress;

        #region Ctor
        public HoldingRegister(IConcurrentModbusMaster concurrentMaster, byte slaveAddress)
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
            throw new NotImplementedException();
        }
    }
}