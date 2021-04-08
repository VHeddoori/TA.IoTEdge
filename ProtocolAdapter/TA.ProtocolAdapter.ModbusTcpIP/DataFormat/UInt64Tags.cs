using System;
using System.Collections.Generic;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.DataFormat
{
    class UInt64Tags
    {
        public void GetMemoryBlocks(IEnumerable<Tags> tags)
        {

        }

        public void ReadHoldingRegisters()
        {
            Console.WriteLine("Reading UInt64 holding registers");
        }

        public void WriteHoldingRegisters()
        {
            Console.WriteLine("Writing UInt64 holding registers");

        }
        public void ReadInputRegisters()
        {
            Console.WriteLine("Reading UInt64 input registers");
        }
    }
}
