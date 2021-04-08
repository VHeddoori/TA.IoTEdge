using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP.Functions
{
    class RegisterHelper
    {

        //public static ushort[][] FindContinuousMemoryBlocks(IEnumerable<Tags> tags)
        //{
        //    Tags[] modbusTags = tags.ToArray();

        //    List<ushort[]> blocks = new List<ushort[]>();
        //    ushort start_addr = 0;
        //    bool start_addr_unset = true;
        //    for (int i = 0; i < modbusTags.Length; i++)
        //    {
        //        if (start_addr_unset)
        //        {
        //            start_addr = ushort.Parse(modbusTags[i].Address.ToString());
        //            start_addr_unset = false;
        //        }
        //        if (i < modbusTags.Length - 1)
        //        {
        //            if (modbusTags[i].Address + GetWordCount(modbusTags[i].DataType) == modbusTags[i + 1].Address)
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                blocks.Add(new ushort[] { start_addr, Convert.ToUInt16(modbusTags[i].Address + GetWordCount(modbusTags[i].DataType)) });
        //                start_addr = 0;
        //                start_addr_unset = true;
        //            }
        //        }
        //        else if (i == modbusTags.Length - 1)
        //        {
        //            blocks.Add(new ushort[] { start_addr, Convert.ToUInt16(modbusTags[i].Address + GetWordCount(modbusTags[i].DataType)) });
        //        }
        //    }
        //    return blocks.ToArray();
        //}


    }
}
