using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra
{
    public static class BinarySerializer
    {
        public static void CheckHeader(byte header, ref byte[] binary)
        {
            if (binary[0] != header)
                throw new Exception("Invalid Binary Header!");

            binary = binary.Skip(1).ToArray();

            VarInt length = VarIntFromBinary(ref binary);

            if (length != binary.Length)
            {
                throw new Exception("Invalid length of Binary!");
            }
        }

        public static void CheckFooter(byte header, ref byte[] binary)
        {
            if (binary[0] != header)
                throw new Exception("Invalid Binary Footer!");

            binary = binary.Skip(1).ToArray();

            if(binary.Length!=0)
                throw new Exception("No end found!");
        }

        public static byte[] BinaryFromBytes(byte[] bytes)
        {
           List<byte> binary= new List<byte>();
            VarInt length = new VarInt(bytes.Length);
            binary.AddRange((byte[])length);
            binary.AddRange(bytes);
            return binary.ToArray();
        }
        public static byte[] BytesFromBinary(ref byte[] binary)
        {
            VarInt length = VarIntFromBinary(ref binary);
            byte[] ret = binary.Take((int) length).ToArray();
            binary= binary.Skip((int) length).ToArray();
            return ret;
        }

  
        public static TEnum EnumFromBinary<TEnum>(ref byte[] binary)
        {
            byte b = binary[0];
            binary = binary.Skip(1).ToArray();
            return (TEnum)Enum.ToObject(typeof(TEnum), b);
        }

        public static byte BinaryFromBool(bool b)
        {
            return (byte) (b ? 0x01 : 0x00);
        }
        public static bool BoolFromBinary(ref byte[] binary)
        {
            bool ret = binary[0] == 0x01 ? true : false;
            binary = binary.Skip(1).ToArray();
            return ret;
        }
        public static VarInt VarIntFromBinary(ref byte[] binary)
        {
            VarInt varInt = new VarInt(binary);
            binary = binary.Skip(((byte[])varInt).Length).ToArray();

            return varInt;
        }

        public static byte[] BinaryFromString(string str)
        {
            List<byte> binary = new List<byte>();
            VarInt length = str.Length;
            binary.AddRange((byte[])length);
            binary.AddRange(Encoding.UTF8.GetBytes(str));
            return binary.ToArray();
        }
        public static string StringFromBinary(ref byte[] binary)
        {
            VarInt length = VarIntFromBinary(ref binary);

            string str = Encoding.UTF8.GetString(binary, 0, (int)length);

            binary = binary.Skip((int)length).ToArray();

            return str;
        }
    }
}
