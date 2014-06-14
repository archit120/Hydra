using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra
{

    //The different types of POWs we are going to support

    public enum PowType
    {
        Scrypt = 0x01, //The golden one
        Keccak = 0x02,
        X11 = 0x03,
        X13 = 0x04,
        Groestl = 0x05,
        Skein = 0x06,
        Qubit = 0x0

        //Reserved for whatever alt coin developers come up with
    }
}
