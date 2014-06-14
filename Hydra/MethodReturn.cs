using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra
{
    internal interface IMethodReturn
    {
        bool Success { get; set; }

        string Error { get; set; }

        void HandleErrorOrSuccess(ref byte[] binary);
    }

    //Return for client methods

    //0x30
    public class ConnectReturn : IMethodReturn
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public PowType Pow { get; set; } //The POW to be used, determines miner launched

        public string Uri { get; set; } //The mining URI for example stratum+tcp://localhost:7706

         public ConnectReturn(bool success, string error, PowType pow, string uri)
            : base()
         {
             Success = success;
             Error = error;
             Pow = pow;
             Uri = uri;
         }

         public ConnectReturn()
            : base()
        {
        }

        public void HandleErrorOrSuccess(ref byte[] binary)
        {

            if (binary[0] == 0x00)
            {
                Success = BinarySerializer.BoolFromBinary(ref binary);

                Error = BinarySerializer.StringFromBinary(ref binary);
            }
            else
            {
                Success = BinarySerializer.BoolFromBinary(ref binary);
            }
        }
 
    }

    //0x31
    public class GetReturn : IMethodReturn
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public PowType Pow { get; set; } //The POW to be used, determines miner launched

        public string Uri { get; set; } //The mining URI for example stratum+tcp://localhost:7706

        public GetReturn(bool success, string error, PowType pow, string uri)
            : base()
        {
            Success = success;
            Error = error;
            Pow = pow;
            Uri = uri;
        }

        public GetReturn()
            : base()
        {
        }

        public void HandleErrorOrSuccess(ref byte[] binary)
        {

            if (binary[0] == 0x00)
            {
                Success = BinarySerializer.BoolFromBinary(ref binary);

                Error = BinarySerializer.StringFromBinary(ref binary);
            }
            else
            {
                Success = BinarySerializer.BoolFromBinary(ref binary);
            }
        }
    }


}
