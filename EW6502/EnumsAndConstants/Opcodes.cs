using EW6502.Opcodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW6502.EnumsAndConstants
{
    public static class Opcodes
    {
        public static IDictionary<byte, Opcode> OpcodeLookup = new Dictionary<byte, Opcode>()
        {
            {(byte)0x00,new Opcode(){Mnemonic="BRK", AddressMode=AddressingModes.IMM, Operation=Operations.BRK } },
        };

        
    }
}
