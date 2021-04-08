using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.DataFormat
{
    class UInt16Tags
    {
        public void GetMemoryBlocks(IEnumerable<Tags> tags)
        {

        }

        public void ReadHoldingRegisters()
        {
            Console.WriteLine("Reading UInt16 holding registers");
        }

        public void WriteHoldingRegisters()
        {
            Console.WriteLine("Writing UInt16 holding registers");

        }
        public void ReadInputRegisters()
        {
            Console.WriteLine("Reading UInt16 input registers");
        }
    }
}
