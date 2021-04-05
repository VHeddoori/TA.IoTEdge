using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TA.ProtocolAdapter.ModbusTcpIP.Constants;

namespace TA.ProtocolAdapter.ModbusTcpIP.Entities
{
    class Tag
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
        internal int RegisterCount { get; set; }
        internal bool IsByteSwap { get; set; }
        internal bool IsBigEndian { get; set; }

        #endregion


        #region Member functions

        internal static IEnumerable<Tag> Read(string tagsString)
        {
            return JsonSerializer.Deserialize<IEnumerable<Tag>>(tagsString);
        }

        internal static void ProcessTags (IEnumerable<Tag> tags)
        {
            tags.ToList().ForEach(tag=> 
            {
                SetEndiannessAndByteSwap(tag);
                tag.Address = GetRelativeAddress(tag);
                tag.RegisterCount = GetWordCount(tag.DataType);
            });
        }

        private static ushort GetWordCount(string dataType)
        {
            switch (dataType)
            {
                case string stringType when stringType.Equals(Enums.DataType.String.ToString(), StringComparison.OrdinalIgnoreCase):
                case string boolType when boolType.Equals(Enums.DataType.Boolean.ToString(), StringComparison.OrdinalIgnoreCase) || boolType.Equals("Bool", StringComparison.OrdinalIgnoreCase):
                case string int16Type when int16Type.Equals(Enums.DataType.Int16.ToString(), StringComparison.OrdinalIgnoreCase):
                case string uint16Type when uint16Type.Equals(Enums.DataType.UInt16.ToString(), StringComparison.OrdinalIgnoreCase):
                    return Words.OneWord;

                case string int32Type when int32Type.Equals(Enums.DataType.Int32.ToString(), StringComparison.OrdinalIgnoreCase):
                case string uint32Type when uint32Type.Equals(Enums.DataType.UInt32.ToString(), StringComparison.OrdinalIgnoreCase):
                case string floatType when floatType.Equals(Enums.DataType.Float.ToString(), StringComparison.OrdinalIgnoreCase):
                    return Words.TwoWords;

                case string int64Type when int64Type.Equals(Enums.DataType.Int64.ToString(), StringComparison.OrdinalIgnoreCase):
                case string uint64Type when uint64Type.Equals(Enums.DataType.UInt64.ToString(), StringComparison.OrdinalIgnoreCase):
                case string doubleType when doubleType.Equals(Enums.DataType.Double.ToString(), StringComparison.OrdinalIgnoreCase):
                    return Words.FourWords;
                
                default:
                    throw new Exception($"Invalid data type found " +
                        $"Supported data types are : Boolean, String, Int16, UInt16, Int32, UInt32, Int64, UInt64, Float and Double");
            }
        }

        private static int GetRelativeAddress(Tag tag)
        {
            tag.Address -= Constant.AddressOffset;
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

            return tag.Address;
        }

        private static void SetEndiannessAndByteSwap(Tag tag)
        {
            switch (tag.ByteOrder)
            {
                case string bigEndian when bigEndian.Equals(Endianness.BigEndian, StringComparison.OrdinalIgnoreCase):
                    tag.IsBigEndian = true;
                    tag.IsByteSwap = false;
                    break;
                case string littleEndian when littleEndian.Equals(Endianness.LittleEndian, StringComparison.OrdinalIgnoreCase):
                    tag.IsBigEndian = false;
                    tag.IsByteSwap = false;
                    break;
                case string bigEndianByteSwap when bigEndianByteSwap.Equals(Endianness.BigEndianByteSwap, StringComparison.OrdinalIgnoreCase):
                    tag.IsBigEndian = true;
                    tag.IsByteSwap = true;
                    break;
                case string littleEndianByteSwap when littleEndianByteSwap.Equals(Endianness.LittleEndianByteSwap, StringComparison.OrdinalIgnoreCase):
                    tag.IsBigEndian = false;
                    tag.IsByteSwap = true;
                    break;
                default:
                    tag.IsBigEndian = true;
                    tag.IsByteSwap = false;
                    break;
            }
        }

        #endregion
    }
}
