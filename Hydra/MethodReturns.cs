using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Hydra
{
    public class MethodReturns
    {

        public MethodType Method { get; set; }

        private object Return { get; set; }

        public T GetReturn<T>()
        {
            //Sanity checks
            if (Return == null)
                throw new Exception("Internal Return is null");

            if (!(Return is IMethodReturn))
                throw new Exception("Internal Return is of the wrong type");

            return (T)Return;
        }

        public void SetReturn<T>(MethodType method,T methodReturn)
        {
            Method = method;

            if (methodReturn == null)
                throw new Exception("Return is null");

            if (!(methodReturn is IMethodReturn))
                throw new Exception("Return is of the wrong type");

            Return = methodReturn;
        }
        public MethodReturns()
        {}

        public MethodReturns(MethodType method,ref byte[] binary)
        {
            Method = method;
            switch (method)
            {
                case MethodType.ServerConnect:
                    ConnectReturn connectReturn = new ConnectReturn();
                    connectReturn.HandleErrorOrSuccess(ref binary);
                    if (connectReturn.Success)
                    {
                        connectReturn.Pow = BinarySerializer.EnumFromBinary<PowType>(ref binary);
                        connectReturn.Uri = BinarySerializer.StringFromBinary(ref binary);
                    }
                    Return = connectReturn;
                    break;
                case MethodType.ServerGet:
                    GetReturn getReturn = new GetReturn();
                    getReturn.HandleErrorOrSuccess(ref binary);
                    if (getReturn.Success)
                    {
                        getReturn.Pow = BinarySerializer.EnumFromBinary<PowType>(ref binary);
                        getReturn.Uri = BinarySerializer.StringFromBinary(ref binary);
                    }
                    Return = getReturn;
                    break;
                default:
                    throw new Exception("Invalid method type supplied to MethodReturns Initializer");
                    break;

            }
        }

        public byte[] GetBytes()
        {
            List<byte> binary = new List<byte>();
            switch (Method)
            {
                case MethodType.ServerConnect:
                    ConnectReturn connectReturn = (ConnectReturn) Return;
                    binary.Add(BinarySerializer.BinaryFromBool(connectReturn.Success));
                    if (connectReturn.Success)
                    {
                        binary.Add((byte)connectReturn.Pow);
                        binary.AddRange(BinarySerializer.BinaryFromString(connectReturn.Uri));
                    }
                    else
                    {
                        binary.AddRange(BinarySerializer.BinaryFromString(connectReturn.Error));   
                    }
                    break;
                case MethodType.ServerGet:
                    GetReturn getReturn = (GetReturn)Return;
                    binary.Add(BinarySerializer.BinaryFromBool(getReturn.Success));
                    if (getReturn.Success)
                    {
                        binary.Add((byte)getReturn.Pow);
                        binary.AddRange(BinarySerializer.BinaryFromString(getReturn.Uri));
                    }
                    else
                    {
                        binary.AddRange(BinarySerializer.BinaryFromString(getReturn.Error));   
                    }
                    break;

            }
            return binary.ToArray();
        }

     
    }
}
