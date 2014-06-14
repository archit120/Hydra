using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra
{
    internal interface IMethodParameter
    {
    }

    //Parameters for client methods

    //0x00
    public class ConnectParameter : IMethodParameter
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }

        public ConnectParameter(string userName, string passWord)
            : base()
        {
            UserName = userName;
            PassWord = passWord;
        }

        public ConnectParameter()
            : base()
        {
        }

    }

    //0x01
    public class GetParameter : IMethodParameter
    {
        //Blank, we need no parameter

        public GetParameter()
            : base()
        {
        }

    }

    //0xff
    public class ReceiveErrorParameter:IMethodParameter
    {
      //Blank, we need no parameter

        public ReceiveErrorParameter()
            : base()
        {
        }  
    }
    //Parameters for Server method(s)

    //0x20
    public class ManagerUpdateParameter : IMethodParameter
    {
        public int Version { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }

        public ManagerUpdateParameter(int version, string fileName, byte[] data) : base()
        {
            Version = version;
            FileName = fileName;
            Data = data;
        }

        public ManagerUpdateParameter() : base()
        {
        }
    }

    //0x21
    public class WriteParameter : IMethodParameter
    {
        public string Message { get; set; }

        public WriteParameter(string message) : base()
        {
            Message = message;
        }

        public WriteParameter() : base()
        {
        }
    }
}
