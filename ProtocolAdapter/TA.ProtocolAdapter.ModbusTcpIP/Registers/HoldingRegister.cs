using System;
using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.Registers
{
    class HoldingRegister : IRegister
    {
        public HoldingRegister(IEnumerable<Tag> tags)
        {

        }

        public void Read(IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }
    }
}
