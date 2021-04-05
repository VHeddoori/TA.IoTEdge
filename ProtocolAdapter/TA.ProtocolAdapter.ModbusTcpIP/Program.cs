using NModbus;
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
                var tags = new List<Tag>();

                var t1 = new Tag
                {
                    Id = 1,
                    Address = 1,
                    FunctionCode = 4,
                    DataType = "Int32",
                    Name = "One",
                    ByteOrder = "BigEndian",
                };
                var t2 = new Tag
                {
                    Id = 2,
                    Address = 2,
                    FunctionCode = 4,
                    DataType = "Int32",
                    Name = "Two",
                    ByteOrder = "BigEndian",
                };

                var t3 = new Tag
                {
                    Id = 3,
                    Address = 3,
                    FunctionCode = 4,
                    DataType = "Int64",
                    Name = "Three",
                    ByteOrder = "BigEndian",
                };
                var t4 = new Tag
                {
                    Id = 3,
                    Address = 3,
                    FunctionCode = 4,
                    DataType = "Int64",
                    Name = "Four",
                    ByteOrder = "BigEndian",
                };


                var t5 = new Tag
                {
                    Id = 5,
                    Address = 5,
                    FunctionCode = 4,
                    DataType = "Int64",
                    Name = "Five",
                    ByteOrder = "LittleEndian",
                };
                var t6 = new Tag
                {
                    Id = 6,
                    Address = 6,
                    FunctionCode = 4,
                    DataType = "Int64",
                    Name = "Six",
                    ByteOrder = "LittleEndian",
                };

                var t7 = new Tag
                {
                    Id = 7,
                    Address = 7,
                    FunctionCode = 3,
                    DataType = "Int64",
                    Name = "Seven",
                    ByteOrder = "LittleEndian",
                };
                var t8 = new Tag
                {
                    Id = 8,
                    Address = 1,
                    FunctionCode = 3,
                    DataType = "Int64",
                    Name = "Eight",
                    ByteOrder = "LittleEndian",
                };

                var t9 = new Tag
                {
                    Id = 9,
                    Address = 2,
                    FunctionCode = 3,
                    DataType = "Int64",
                    Name = "Seven",
                    ByteOrder = "LittleEndian",
                };
                var t10 = new Tag
                {
                    Id = 10,
                    Address = 10,
                    FunctionCode = 3,
                    DataType = "Int64",
                    Name = "Eight",
                    ByteOrder = "LittleEndian",
                };

                tags.Add(t1);
                tags.Add(t2);
                tags.Add(t3);
                tags.Add(t4);
                tags.Add(t5);
                tags.Add(t6);
                tags.Add(t9);
                tags.Add(t10);
                tags.Add(t7);
                tags.Add(t8);

                IEnumerable<IGrouping<object,Tag>> gt = tags.OrderBy(t => t.Address).GroupBy(x => new { x.FunctionCode, x.DataType, x.ByteOrder });
                
                var  result = tags.OrderBy(t => t.Address).GroupBy(x => new { x.FunctionCode, x.DataType, x.ByteOrder });

                foreach(var r in result)
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

                // read five input values
                ushort startAddress = 0;
                ushort numInputs = 5;
               var inputs = master.ReadInputRegisters(1, startAddress, numInputs);

                for (int i = 0; i < numInputs; i++)
                {
                    Console.WriteLine($"Input {(startAddress + i)}={(inputs[i])}");
                }
            }
        }

    }
}
