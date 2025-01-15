using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EW6502.StatusFlags;

namespace EW6502.Opcodes
{
    public static class Operations
    {
        public static byte BRK(_6502 cpu)
        {
			cpu.PC++;

			cpu.SetFlag(Flag.C);
			cpu.Bus.Write((ushort)(0x0100 + cpu.Sptr), (byte)((cpu.PC >> 8) & 0x00FF));
			cpu.Sptr--;
			cpu.Bus.Write((ushort)(0x0100 + cpu.Sptr), (byte)(cpu.PC & 0x00FF));
			cpu.Sptr--;

			cpu.SetFlag(Flag.B);
			cpu.Bus.Write((ushort)(0x0100 + cpu.Sptr), cpu.Status);
			cpu.Sptr--;
			cpu.ClearFlag(Flag.B);

			cpu.PC = (ushort)((ushort)cpu.Bus.Read(0xFFFE) | (ushort)(cpu.Bus.Read(0xFFFF) << 8));
			return 0;
		}
    }
}
