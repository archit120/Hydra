using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra
{

    public enum MethodType
    {
        //Client methods are from 0x00 to 0x0f
        ClientConnect = 0x00, //The client connects       
        ClientGet = 0x01, //Client get's the most profitable POW ATM

        //No client returns but reserved from 0x10 to 0x1f

        //Server methods are from 0x20 to 0x2f
        ServerManagerUpdate = 0x20, //Server tells client to update the miner manager, useful for purposes such as new POWs
        ServerWrite = 0x21, //Server tells client to print something

        //Server returns are from 0x30 to 0x3f
        ServerConnect = 0x30, //Server response to 0x00
        ServerGet = 0x31, //Server response to 0x11

        //Reserved from 0x40 to 0xef

        //Specials are from 0xff to 0xf0
        ClientReceiveError = 0xff, //The client didn't understand
    }

}
