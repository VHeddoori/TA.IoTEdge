using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.DataFormat
{
    class BooleanTags
    {
        public void GetMemoryBlocks(IEnumerable<Tags> tags)
        {

        }

        public void ReadCoils()
        {
            Console.WriteLine("Reading boolean coils");
        }

        public void WriteCoils()
        {
            Console.WriteLine("Writing boolean coils");
        
        }

        public void ReadInputStatus()
        {
            Console.WriteLine("Reading boolean input status");
        }

    }
}
