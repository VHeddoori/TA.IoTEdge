using NModbus;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TA.ProtocolAdapter.ModbusTcpIP.Entities;
using TA.ProtocolAdapter.ModbusTcpIP.Enums;
using TA.ProtocolAdapter.ModbusTcpIP.Registers;

namespace TA.ProtocolAdapter.ModbusTcpIP
{
    public class ModbusTcpIPMaster
    {
        #region Data members
        private IModbusMaster modbusMaster;
        private TcpClient tcpClient;

        private Coils rCoils;
        private InputStatus rInputStatus;
        private HoldingRegister rHoldingRegister;
        private InputRegister rInputRegister;

        private Coils wCoils;
        private InputStatus wInputStatus;
        private HoldingRegister wHoldingRegister;
        private InputRegister wInputRegister;

        private IEnumerable<IGrouping<int, Tag>> readTags;
        private IEnumerable<IGrouping<int, Tag>> writeTags;

        #endregion

        #region Ctor
        public ModbusTcpIPMaster()
        {
        }
        #endregion

        #region Member functions

        internal void Connect(string host, int port)
        {
            tcpClient = new TcpClient(host, port);
            ModbusFactory modbusFactory = new();
            modbusMaster = modbusFactory.CreateMaster(tcpClient);
        }

        internal void Disconnect()
        {
            tcpClient.Close();
            modbusMaster.Dispose();
        }


        internal void ReadRegisterSetUp(IEnumerable<Tag> tags)
        {
            readTags = tags.OrderBy(x => x.Address).GroupBy(y => y.FunctionCode);
            foreach (var r in readTags)
            {
                switch ((RegisterType)r.Key)
                {
                    case RegisterType.Coils:
                        rCoils = new Coils(r);
                        break;
                    case RegisterType.InputStatus:
                        rInputStatus = new InputStatus(r);
                        break;
                    case RegisterType.HoldingRegisters:
                        rHoldingRegister = new HoldingRegister(r);
                        break;
                    case RegisterType.InputRegisters:
                        rInputRegister = new InputRegister(r);
                        break;
                    default:
                        throw new System.Exception("Invalid function code found while data from registers");
                }
            }
        }
        internal ConcurrentDictionary<int, string> ReadRegisters(IEnumerable<Tag> tags)
        {
            return null;
        }


        internal void WriteRegisterSetUp(IEnumerable<Tag> tags)
        {
            writeTags = tags.OrderBy(x => x.Address).GroupBy(y => y.FunctionCode);
            foreach (var ot in writeTags)
            {
                switch ((RegisterType)ot.Key)
                {
                    case RegisterType.Coils:
                        wCoils.Read(ot);
                        break;
                    case RegisterType.InputStatus:
                        rInputStatus.Read(ot);
                        break;
                    case RegisterType.HoldingRegisters:
                        rHoldingRegister.Read(ot);
                        break;
                    case RegisterType.InputRegisters:
                        rInputRegister.Read(ot);
                        break;
                    default:
                        throw new System.Exception("Invalid function code found while data from registers");
                }
            }
        }

        internal void WriteRegisters()
        {
            foreach (var ot in orderedTags)
            {
                switch ((RegisterType)ot.Key)
                {
                    case RegisterType.Coils:
                        wCoils.Write(ot);
                        break;
                    case RegisterType.InputStatus:
                        wInputStatus.Write(ot);
                        break;
                    case RegisterType.HoldingRegisters:
                        wHoldingRegister.Write(ot);
                        break;
                    case RegisterType.InputRegisters:
                        wInputRegister.Write(ot);
                        break;
                    default:
                        throw new System.Exception("Invalid function code found while writing data to registers");
                }
            }
        }
        #endregion
    }
}
