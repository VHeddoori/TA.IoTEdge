using NModbus;
using System;
using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.DataFormat;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;
using TA.ProtocolAdapter.ModbusTcpIP.Interfaces;

namespace TA.ProtocolAdapter.ModbusTcpIP.Registers
{
    class Coils : ModbusRegister
    {
        private IConcurrentModbusMaster concurrentMaster;
        private readonly byte slaveAddress;
        private BooleanTags boolTags;

        #region Ctor
        public Coils(IConcurrentModbusMaster concurrentMaster, byte slaveAddress)
        {
            this.concurrentMaster = concurrentMaster;
            this.slaveAddress = slaveAddress;

            boolTags = new BooleanTags();
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
