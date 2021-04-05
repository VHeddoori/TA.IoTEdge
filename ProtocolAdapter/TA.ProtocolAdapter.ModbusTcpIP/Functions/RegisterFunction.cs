using NModbus;
using NModbus.Extensions.Functions;
using System;
using System.IO;
using System.Linq;

namespace TA.ProtocolAdapter.ModbusTcpIP.Functions
{
    class RegisterFunction : RegisterFunctions
    {
        public static byte[][] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints,
            IModbusMaster master, uint wordSize, Func<byte[], byte[]> endianConverter, bool wordSwap = false)
        {
            var registerMultiplier = GetRegisterMultiplier(wordSize);
            var registersToRead = (ushort)(numberOfPoints * registerMultiplier);


            var values = master.ReadInputRegisters(slaveAddress, startAddress, registersToRead);
            if (wordSwap) Array.Reverse(values);
            return ConvertRegistersToValues(values, registerMultiplier).Select(endianConverter).ToArray();
        }

        public static byte[][] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints,
            IModbusMaster master, uint wordSize, Func<byte[], byte[]> endianConverter, bool wordSwap = false)
        {
            var registerMultiplier = GetRegisterMultiplier(wordSize);
            var registersToRead = (ushort)(numberOfPoints * registerMultiplier);

            var values = master.ReadHoldingRegisters(slaveAddress, startAddress, registersToRead);
            if (wordSwap) Array.Reverse(values);
            return ConvertRegistersToValues(values, registerMultiplier).Select(endianConverter).ToArray();
        }

        private static byte[][] ConvertRegistersToValues(ushort[] registers, int registerMultiplier)
        {
            if ((registers.Length % registerMultiplier) != 0)
            {
                throw new InvalidDataException("Registers length is not a multiple of RegisterMultiplier");
            }
            var count = registers.Length / registerMultiplier;
            var values = new byte[count][];
            for (var index = 0; index < count; index++)
            {
                var offset = index * registerMultiplier;
                var segment = new ArraySegment<ushort>(registers, offset, registerMultiplier);
                var bytes = segment.SelectMany(BitConverter.GetBytes).ToArray();
                values[index] = bytes;
            }
            return values;
        }

        private static int GetRegisterMultiplier(uint wordSize)
        {
            switch (wordSize)
            {
                case (16):
                    return 1;
                case (32):
                    return 2;
                case (64):
                    return 4;
                default: throw new ArgumentException("Word size must be 16/32/64");
            }
        }
    }
}
