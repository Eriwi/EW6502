using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW6502
{
    public class StatusFlags
    {

        //Just convenient references for the bits of the status register.
        public enum Flag
        {
            C = (byte)1, //Carry
            Z = (byte)(1 << 1), //Zero
            I = (byte)(1 << 2), //Interrupt, disable such if 1
            D = (byte)(1 << 3), //Decimal mode
            B = (byte)(1 << 4), //Break
            U = (byte)(1 << 5), //Not used
            V = (byte)(1 << 6), //Overflow
            N = (byte)(1 << 7) //Negative
        }
    }
}
