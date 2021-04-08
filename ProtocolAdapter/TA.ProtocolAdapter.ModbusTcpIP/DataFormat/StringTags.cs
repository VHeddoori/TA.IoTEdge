using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.DataFormat
{
    class StringTags
    {
        public void GetMemoryBlocks(IEnumerable<Tags> tags)
        {

        }

        public void ReadHoldingRegisters()
        {
            Console.WriteLine("Reading String holding registers");
        }

        public void WriteHoldingRegisters()
        {
            Console.WriteLine("Writing String holding registers");

        }
        public void ReadInputRegisters()
        {
            Console.WriteLine("Reading String input registers");
        }
    }
}
