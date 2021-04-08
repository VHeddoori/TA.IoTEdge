using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TA.ProtocolAdapter.ModbusTcpIP.Constants;

namespace TA.ProtocolAdapter.ModbusTcpIP.Entities
{
    class Tags
    {
        #region Data members
        public int Id { get; set; }
        public string Name { get; set; }
        public int Address { get; set; }
        public string DataType { get; set; }
        public int FunctionCode { get; set; }
        public string ByteOrder { get; set; }
        public int Value { get; set; }
        public bool IsWrite { get; set; }

        #endregion


        #region Member functions

        internal static IEnumerable<Tags> Deserialize(string tagsString)
        {
            return JsonSerializer.Deserialize<IEnumerable<Tags>>(tagsString);
        }

        internal static void SetRelativeAddress(IEnumerable<Tags> tags)
        {
            foreach(var tag in tags)
            {
                switch (tag.FunctionCode)
                {
                    case 1:
                        tag.Address -= Offset.CoilsOffset;
                        break;
                    case 2:
                        tag.Address -= Offset.InputStatusOffset;
                        break;
                    case 3:
                        tag.Address -= Offset.HoldingRegisterOffset;
                        break;
                    case 4:
                        tag.Address -= Offset.InputRegisterOffset;
                        break;
                    default:
                        throw new Exception($"Invalid function code found for tag with id - {tag.Id}");
                }
            }
        }

        #endregion
    }
}
