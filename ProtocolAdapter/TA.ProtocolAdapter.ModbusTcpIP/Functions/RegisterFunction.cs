using NModbus;
using NModbus.Extensions.Functions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TA.ProtocolAdapter.ModbusTcpIP.Functions
{
    class RegisterFunction : RegisterFunctions
    {

        public static async Task<byte[][]> ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints,
            IConcurrentModbusMaster master, uint wordSize, Func<byte[], byte[]> endianConverter, bool wordSwap = false)
        {
            var registerMultiplier = GetRegisterMultiplier(wordSize);
            var registersToRead = (ushort)(numberOfPoints * registerMultiplier);


            var values = await master.ReadInputRegistersAsync(slaveAddress, startAddress, registersToRead);
            if (wordSwap) Array.Reverse(values);
            return ConvertRegistersToValues(values, registerMultiplier).Select(endianConverter).ToArray();
        }

        public static async Task<byte[][]> ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints,
            IConcurrentModbusMaster master, uint wordSize, Func<byte[], byte[]> endianConverter, bool wordSwap = false)
        {
            var registerMultiplier = GetRegisterMultiplier(wordSize);
            var registersToRead = (ushort)(numberOfPoints * registerMultiplier);

            var values = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, registersToRead);
            if (wordSwap) Array.Reverse(values);
            return ConvertRegistersToValues(values, registerMultiplier).Select(endianConverter).ToArray();
        }


        public static async void WriteHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints,
            IConcurrentModbusMaster master, uint wordSize, Func<byte[], byte[]> endianConverter, bool wordSwap = false, ushort[] data = null)
        {
            var registerMultiplier = GetRegisterMultiplier(wordSize);
            var registersToRead = (ushort)(numberOfPoints * registerMultiplier);

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress,data, registersToRead);
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
