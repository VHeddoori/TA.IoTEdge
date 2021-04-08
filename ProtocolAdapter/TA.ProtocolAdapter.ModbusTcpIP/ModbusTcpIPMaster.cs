using NModbus;
using NModbus.Device;
using System;
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
        private readonly byte slaveAddress;
        private readonly IConcurrentModbusMaster modbusMaster;
        private TcpClient tcpClient;

        private readonly Coils coils;
        private readonly InputStatus inputStatus;
        private readonly HoldingRegister holdingRegister;
        private readonly InputRegister inputRegister;

        #endregion

        #region Ctor
        public ModbusTcpIPMaster(byte slaveAddress)
        {
            this.slaveAddress = slaveAddress;
            ModbusFactory modbusFactory = new();
            IModbusMaster master = modbusFactory.CreateMaster(tcpClient);
            modbusMaster = new ConcurrentModbusMaster(master, new TimeSpan());

            coils = new Coils(modbusMaster, slaveAddress);
            inputStatus = new InputStatus(modbusMaster, slaveAddress);
            holdingRegister = new HoldingRegister(modbusMaster, slaveAddress);
            inputRegister = new InputRegister(modbusMaster, slaveAddress);

        }
        #endregion

        #region Member functions

        internal void Connect(string host, int port)
        {
            tcpClient = new TcpClient(host, port);
        }

        internal void Disconnect()
        {
            tcpClient.Close();
            modbusMaster.Dispose();
        }


        internal Dictionary<RegisterType, List<RegisterBlock>> GetRegisterChunks(IEnumerable<Tags> tags)
        {
            Dictionary<RegisterType, List<RegisterBlock>> registerTypeBlocksMap = new Dictionary<RegisterType, List<RegisterBlock>>();
            var tagGroups = tags.GroupBy(x => x.FunctionCode);
            foreach (var tagGroup in tagGroups)
            {
                switch ((RegisterType)tagGroup.Key)
                {
                    case RegisterType.Coils:
                        var coilBlocks = coils.GetRegisterBlocks(tagGroup);
                        registerTypeBlocksMap.Add(RegisterType.Coils, coilBlocks);
                        break;
                    case RegisterType.InputStatus:
                        var inputStatusBlocks = inputStatus.GetRegisterBlocks(tagGroup);
                        registerTypeBlocksMap.Add(RegisterType.InputStatus, inputStatusBlocks);
                        break;
                    case RegisterType.HoldingRegisters:
                        var holdingRegisterBlocks = holdingRegister.GetRegisterBlocks(tagGroup);
                        registerTypeBlocksMap.Add(RegisterType.HoldingRegisters, holdingRegisterBlocks);
                        break;
                    case RegisterType.InputRegisters:
                        var inputRegisterBlocks = inputRegister.GetRegisterBlocks(tagGroup);
                        registerTypeBlocksMap.Add(RegisterType.InputRegisters, inputRegisterBlocks);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid register type found while reading from modbus registers");
                }
            }

            return registerTypeBlocksMap;

        }


        internal ConcurrentDictionary<int, string> Read(Dictionary<RegisterType, List<RegisterBlock>> registerTypeBlocksMap)
        {
            foreach(var registerType in registerTypeBlocksMap)
            {
                
                switch(registerType.Key)
                {
                    case RegisterType.Coils:
                        break;
                    case RegisterType.InputStatus:
                        break;
                    case RegisterType.HoldingRegisters:
                        break;
                    case RegisterType.InputRegisters:
                        break;
                }
            }
            return null;
        }

        internal void Write(Dictionary<RegisterType, List<RegisterBlock>> registerTypeBlocksMap)
        {
            foreach (var registerType in registerTypeBlocksMap)
            {

                switch (registerType.Key)
                {
                    case RegisterType.Coils:
                        break;
                    case RegisterType.InputStatus:
                        break;
                    case RegisterType.HoldingRegisters:
                        break;
                    case RegisterType.InputRegisters:
                        break;
                }
            }
        }


        #endregion
    }
}
