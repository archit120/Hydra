using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Hydra
{


    //Messages always begin with 0xa2
    public class Message
    {
        public MethodType Method { get;  set; }

        //public bool ReturnOrCall { get; private set; } //This is bad but the best way I could come up with
                                                                
        public MethodParameters Parameters { get;  set; }

        public MethodReturns Return { get;  set; }

        public Message()
        { }

        public Message(MethodType method, MethodParameters parameters)
        {
            Method = method;
            Parameters = parameters;
        }

        public Message(MethodType method, MethodReturns returns)
        {
            Method = method;
            Return = returns;
        }

        public Message(byte[] binary)
        {
            BinarySerializer.CheckHeader(0xa2, ref binary);

            Method = BinarySerializer.EnumFromBinary<MethodType>(ref binary);

            if ((int) Method >= 0x30 && (int) Method <= 0x40)
            {
                Return = new MethodReturns(Method,ref binary);
            }
            else
            {
                Parameters = new MethodParameters(Method,ref binary);
            }

            BinarySerializer.CheckFooter(0xa2, ref binary);
        }

        public byte[] GetBytes()
        {
            List<byte> binary = new List<byte>();
            binary.Add(0xa2);
            //binary.Add(0x00);//To be updated later
            binary.Add((byte)(Method));
            if ((int)Method >= 0x30 && (int)Method <= 0x40)
            {
                binary.AddRange(Return.GetBytes());
            }
            else
            {
                binary.AddRange(Parameters.GetBytes());
            }
            binary.Add(0xa2);
            binary.InsertRange(1, (byte[]) (new VarInt(binary.Count - 1)));
            return binary.ToArray();
        }
    }
}
