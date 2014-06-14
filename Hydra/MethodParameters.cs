using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;

namespace Hydra
{


    public class MethodParameters
    {
        public MethodType Method { get; set; }

        private object Parameter { get; set; }

        public T GetParameter<T>()
        {
            //Sanity checks
            if(Parameter == null)
                throw  new Exception("Internal Parameter is null");

            if(!(Parameter is IMethodParameter))              
                throw  new Exception("Internal Parameter is of the wrong type");

           return (T) Parameter;
        }

        public void SetParamater<T>(MethodType method,T parameter)
        {
            Method = method;
            if (parameter == null)
                throw new Exception("Parameter is null");

            if (!(parameter is IMethodParameter))
                throw new Exception("Parameter is of the wrong type");

            Parameter = parameter;
        }

        public MethodParameters()
        {}

        public MethodParameters(MethodType method, ref byte[] binary)
        {
            Method = method;
            switch (method)
            {
                case MethodType.ClientConnect:
                    ConnectParameter connectParameter = new ConnectParameter();
                    connectParameter.UserName = BinarySerializer.StringFromBinary(ref binary);
                    connectParameter.PassWord = BinarySerializer.StringFromBinary(ref binary);
                    Parameter = connectParameter;
                    break;
                case MethodType.ClientGet:
                    GetParameter getParameter = new GetParameter();
                    Parameter = getParameter;
                    break;
                case MethodType.ClientReceiveError:
                    ReceiveErrorParameter receiveError = new ReceiveErrorParameter();
                    Parameter = receiveError;
                    break;
                case MethodType.ServerManagerUpdate:
                    ManagerUpdateParameter managerUpdate = new ManagerUpdateParameter();
                    managerUpdate.Version = (int) BinarySerializer.VarIntFromBinary(ref binary);
                    managerUpdate.FileName = BinarySerializer.StringFromBinary(ref binary);
                    managerUpdate.Data = BinarySerializer.BytesFromBinary(ref binary);
                    Parameter = managerUpdate;
                    break;
                case MethodType.ServerWrite:
                    WriteParameter writeParameter = new WriteParameter();
                    writeParameter.Message = BinarySerializer.StringFromBinary(ref binary);
                    Parameter = writeParameter;
                    break;
            }
        }
        public byte[] GetBytes()
        {
            List<byte> binary = new List<byte>();
            switch (Method)
            {
                case MethodType.ClientConnect:
                    ConnectParameter connectParameter = (ConnectParameter) Parameter;
                    binary.AddRange(BinarySerializer.BinaryFromString(connectParameter.UserName));                    
                    binary.AddRange(BinarySerializer.BinaryFromString(connectParameter.PassWord));
                    break;
                case MethodType.ClientGet:
                    break;
                case MethodType.ClientReceiveError:
                    break;
                case MethodType.ServerManagerUpdate:
                    ManagerUpdateParameter managerUpdate = (ManagerUpdateParameter) Parameter;
                    binary.AddRange((byte[])(new VarInt(managerUpdate.Version)));
                    binary.AddRange(BinarySerializer.BinaryFromString(managerUpdate.FileName));
                    binary.AddRange(BinarySerializer.BinaryFromBytes(managerUpdate.Data));
                    break;
                case MethodType.ServerWrite:
                    WriteParameter writeParameter = (WriteParameter) Parameter;
                    binary.AddRange(BinarySerializer.BinaryFromString(writeParameter.Message));
                    break;
            }
            return binary.ToArray();
        }
    }
}
