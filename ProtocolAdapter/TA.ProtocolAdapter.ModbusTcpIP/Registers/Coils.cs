using NModbus;
using System;
using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.Registers
{
    class Coils : IRegister
    {

        #region Ctor
        public Coils(IEnumerable<Tag> tags)
        {

        }
        #endregion
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
