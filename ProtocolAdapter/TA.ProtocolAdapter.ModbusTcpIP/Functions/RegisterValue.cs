using NModbus.Extensions.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TA.ProtocolAdapter.ModbusTcpIP.Functions
{
    class RegisterValue
    {
        internal static T GetRegisterValue<T>(byte[][] byteResult, Enums.DataType dataType)
        {
            switch (dataType)
            {
                case Enums.DataType.Boolean:
                    return (T)(object)GetBoolValues(byteResult);
                case Enums.DataType.String:
                    return (T)(object)GetStringValues(byteResult);
                case Enums.DataType.Int16:
                    return (T)(object)GetInt16Values(byteResult);
                case Enums.DataType.UInt16:
                    return (T)(object)GetUInt16Values(byteResult);
                case Enums.DataType.Int32:
                    return (T)(object)RegisterFunctions.ByteValueArraysToInts(byteResult);
                case Enums.DataType.UInt32:
                    return (T)(object)RegisterFunctions.ByteValueArraysToUInts(byteResult);
                case Enums.DataType.Int64:
                    return (T)(object)GetInt64Values(byteResult);
                case Enums.DataType.UInt64:
                    return (T)(object)GetUInt64Values(byteResult);
                case Enums.DataType.Float:
                    return (T)(object)RegisterFunctions.ByteValueArraysToFloats(byteResult);
                case Enums.DataType.Double:
                    return (T)(object)GetDoubleValues(byteResult);
                default:
                    throw new Exception("Invalid datatype encountered in GetRegisterValue method");
            }
        }

        private static bool[] GetBoolValues(byte[][] byteResult)
        {
            List<bool> inputReg = new List<bool>();
            foreach (var res in byteResult)
            {
                inputReg.Add(BitConverter.ToBoolean(res, 0));
            }
            return inputReg.ToArray();
        }

        private static string[] GetStringValues(byte[][] byteResult)
        {
            List<string> inputReg = new List<string>();
            foreach (var res in byteResult)
            {
                string registerValue = string.Empty;
                char[] regValue = Encoding.UTF8.GetString(res).ToArray();
                Array.Reverse(regValue);
                foreach (var val in regValue)
                {
                    registerValue = string.Concat(registerValue, val.ToString());
                }

                inputReg.Add(registerValue);
            }
            return inputReg.ToArray();
        }

        private static short[] GetInt16Values(byte[][] byteResult)
        {
            List<short> inputReg = new List<short>();
            foreach (var res in byteResult)
            {
                inputReg.Add(BitConverter.ToInt16(res, 0));
            }
            return inputReg.ToArray();
        }

        private static ulong[] GetUInt64Values(byte[][] byteResult)
        {
            List<ulong> inputReg = new List<ulong>();
            foreach (var res in byteResult)
            {
                inputReg.Add(BitConverter.ToUInt64(res, 0));
            }
            return inputReg.ToArray();
        }

        private static long[] GetInt64Values(byte[][] byteResult)
        {
            List<long> inputReg = new List<long>();
            foreach (var res in byteResult)
            {
                inputReg.Add(BitConverter.ToInt64(res, 0));
            }
            return inputReg.ToArray();
        }

        private static ushort[] GetUInt16Values(byte[][] byteResult)
        {
            List<ushort> inputReg = new List<ushort>();
            foreach (var res in byteResult)
            {
                inputReg.Add(BitConverter.ToUInt16(res, 0));
            }
            return inputReg.ToArray();
        }

        private static double[] GetDoubleValues(byte[][] byteResult)
        {
            List<double> inputReg = new List<double>();
            foreach (var res in byteResult)
            {
                inputReg.Add(BitConverter.ToDouble(res, 0));
            }
            return inputReg.ToArray();
        }
    }
}
