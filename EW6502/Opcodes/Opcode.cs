using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW6502.Opcodes
{
    public class Opcode
    {
        public Func<_6502, byte> AddressMode { get; set; }
        public Func<_6502, byte> Operation { get; set; }
        public string Mnemonic { get; set; }

        public byte run(_6502 cpu)
        {
            byte res = 0;
            res += AddressMode.Invoke(cpu);
            res += Operation.Invoke(cpu);
            return res;
        }

    }
}
