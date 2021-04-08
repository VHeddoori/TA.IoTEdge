using NModbus.Extensions.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using TA.ProtocolAdapter.ModbusTcpIP.Constants;
using TA.ProtocolAdapter.ModbusTcpIP.Enums;

namespace TA.ProtocolAdapter.ModbusTcpIP.Entities
{
    class RegisterBlock
    {
        public DataType DataType { get; }
        public int StartAddress { get; }
        public int EndAddress { get; }
        public int NumberOfPoints { get; }
        public ushort WordSize { get; }
        public Func<byte[], byte[]> Endianness { get; }
        public bool IsWordSwap { get; }

        public RegisterBlock(DataType dataType, int startAddress, int endAddress, int numberOfPoints, ushort wordSize, Func<byte[], byte[]> endianness, bool isWordSwap)
        {
            this.DataType = dataType;
            this.StartAddress = startAddress;
            this.EndAddress = endAddress;
            this.NumberOfPoints = numberOfPoints;
            this.WordSize = wordSize;
            this.Endianness = endianness;
            this.IsWordSwap = isWordSwap;
        }


        private static Func<byte[], byte[]> GetEndianness(string endian)
        {
            return endian switch
            {
                "BigEndian" or "BigEndianByteSwap" => Endian.BigEndian,
                "LittleEndian" or "LittleEndianByteSwap" => Endian.LittleEndian,
                _ => throw new NotSupportedException($"Invalid value for Endianness found."),
            };
        }

        private static bool IsWordSwapped(string endian)
        {
            return endian switch
            {
                "BigEndianByteSwap" or "LittleEndianByteSwap" => true,
                "BigEndian" or "LittleEndian" => false,
                _ => throw new NotSupportedException($"Invalid value for Endianness found."),
            };
        }

        private static ushort GetWordSize(DataType dataType)
        {
            return dataType switch
            {
                DataType.String or DataType.Boolean or DataType.Int16 or DataType.UInt16 => Words.OneWord,
                DataType.Int32 or DataType.UInt32 or DataType.Float => Words.TwoWords,
                DataType.Int64 or DataType.UInt64 or DataType.Double => Words.FourWords,
                _ => throw new Exception($"Invalid data type found in modbus tag file. Supported data types are : Boolean/Bool, String, Int16, UInt16, Int32, UInt32, Int64, UInt64, Float and Double"),
            };
        }

        private static DataType GetDataTypeEnum(string dataTypeString)
        {
            return (DataType)Enum.Parse(typeof(DataType), dataTypeString);
        }


        public static List<RegisterBlock> GetBlocks(IEnumerable<Tags> tags)
        {
            List<RegisterBlock> registerBlocks = new();
            int numberOfPoints = 0;
            int blockStartAddress = 0;
            int blockEndAddress = 0;
            bool isNewRegisterBlock = true;
            var tagGroups = tags.GroupBy(t => new { t.DataType, t.ByteOrder });

            foreach (var tagGroup in tagGroups)
            {
                DataType dataType = GetDataTypeEnum(tagGroup.Key.DataType);
                Func<byte[], byte[]> byteOrder = GetEndianness(tagGroup.Key.ByteOrder);
                var wordSize = GetWordSize(dataType);

                var orderedTags = tagGroup.OrderBy(t=>t.Address).ToArray();

                for (int index = 0; index < orderedTags.Length; index++)
                {
                    if (isNewRegisterBlock)
                    {
                        SetBlockCursors(index);
                        isNewRegisterBlock = false;
                    }
                    numberOfPoints++;
                    if (orderedTags[index].Address + wordSize != GetNextTagAddress(index))
                    {
                        blockEndAddress = (orderedTags[index].Address + wordSize) - 1;
                        registerBlocks.Add(new RegisterBlock(dataType, blockStartAddress, blockEndAddress, numberOfPoints, wordSize, GetEndianness(orderedTags[index].ByteOrder), IsWordSwapped(orderedTags[index].ByteOrder)));
                        isNewRegisterBlock = true;
                    }
                }

                #region MyRegion
                void SetBlockCursors(int index)
                {
                    blockStartAddress = orderedTags[index].Address;
                    blockEndAddress = blockStartAddress;
                    numberOfPoints = 0;
                }

                int GetNextTagAddress(int index)
                {
                    if (index == orderedTags.Length - 1)
                        return orderedTags[index].Address;
                    else
                        return orderedTags[index + 1].Address;
                } 
                #endregion

            }

            return registerBlocks;
        }

    }
}
