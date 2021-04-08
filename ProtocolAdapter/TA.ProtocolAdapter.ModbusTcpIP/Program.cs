using NModbus;
using NModbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;

namespace TA.ProtocolAdapter.ModbusTcpIP
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var tags = new List<Tags>();

                var t1 = new Tags
                {
                    Id = 1,
                    Address = 1,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "One",
                    ByteOrder = "LittleEndian",
                };
                var t2 = new Tags
                {
                    Id = 2,
                    Address = 3,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Two",
                    ByteOrder = "LittleEndian",
                };

                var t3 = new Tags
                {
                    Id = 3,
                    Address = 5,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Three",
                    ByteOrder = "LittleEndian",
                };
                var t4 = new Tags
                {
                    Id = 3,
                    Address = 7,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Four",
                    ByteOrder = "BigEndian",
                };


                var t5 = new Tags
                {
                    Id = 5,
                    Address = 9,
                    FunctionCode = 3,
                    DataType = "Int64",
                    Name = "Five",
                    ByteOrder = "BigEndian",
                };
                var t6 = new Tags
                {
                    Id = 6,
                    Address = 21,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Six",
                    ByteOrder = "LittleEndian",
                };

                var t7 = new Tags
                {
                    Id = 7,
                    Address = 23,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Seven",
                    ByteOrder = "LittleEndian",
                };
                var t8 = new Tags
                {
                    Id = 8,
                    Address = 25,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Eight",
                    ByteOrder = "LittleEndian",
                };

                var t9 = new Tags
                {
                    Id = 9,
                    Address = 65533,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Seven",
                    ByteOrder = "LittleEndian",
                };
                var t10 = new Tags
                {
                    Id = 10,
                    Address = 65535,
                    FunctionCode = 3,
                    DataType = "Int32",
                    Name = "Eight",
                    ByteOrder = "LittleEndian",
                };

                tags.Add(t1);
                tags.Add(t2);
                tags.Add(t3);
                tags.Add(t4);
                tags.Add(t5);
                tags.Add(t6);
                tags.Add(t7);
                tags.Add(t8);
                tags.Add(t9);
                tags.Add(t10);

                var blocks = RegisterBlock.GetMemoryBlocks(tags);


                IEnumerable<IGrouping<object, Tags>> gt = tags.OrderBy(t => t.Address).GroupBy(x => new { x.FunctionCode, x.DataType, x.ByteOrder });

                var result = tags.OrderBy(t => t.Address).GroupBy(x => new { x.FunctionCode, x.DataType, x.ByteOrder });

                foreach (var r in result)
                {

                    var keyType = r.Key.GetType();
                    var tagList = r.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void ModbusTcpMasterReadInputs()
        {
            using (TcpClient client = new TcpClient("localhost", 502))
            {
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);

                IConcurrentModbusMaster concurrentModbusMaster = new ConcurrentModbusMaster(master, new TimeSpan(100));
                // read five input values
                ushort startAddress = 0;
                ushort numInputs = 5;
                //var inputs = master.(1, startAddress, numInputs);

                for (int i = 0; i < numInputs; i++)
                {
                    //Console.WriteLine($"Input {(startAddress + i)}={(inputs[i])}");
                }
            }
        }

    }
}
