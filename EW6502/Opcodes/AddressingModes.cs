using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW6502.Opcodes
{
    public static class AddressingModes
    {
        public static byte IMP(_6502 cpu)
        {
            cpu.Fetched = cpu.A;
            return 0;
        }
        public static byte IMM(_6502 cpu)
        {
            cpu.Address++;
            return 0;
        }
        public static byte ZP0(_6502 cpu)
        {
            cpu.Address = cpu.Bus.Read(cpu.PC);
            cpu.PC++;
            cpu.Address &= 0x00FF;
            return 0;
        }
        public static byte ZPX(_6502 cpu)
        {
            cpu.Address = (ushort)(cpu.Bus.Read(cpu.PC) + cpu.X);
            cpu.PC++;
            cpu.Address &= 0x00FF;
            return 0;
        }
        public static byte ZPY(_6502 cpu)
        {
            cpu.Address = (ushort)(cpu.Bus.Read(cpu.PC) + cpu.Y);
            cpu.PC++;
            cpu.Address &= 0x00FF;
            return 0;
        }
        public static byte REL(_6502 cpu)
        {
            cpu.AddressRel = cpu.Bus.Read(cpu.PC);
            cpu.PC++;
            if ((cpu.AddressRel & 0x80) != 0)
                cpu.AddressRel |= 0xFF00;
            return 0;
        }
        public static byte ABS(_6502 cpu)
        {
            ushort lo = cpu.Bus.Read(cpu.PC);
            cpu.PC++;
            ushort hi = cpu.Bus.Read(cpu.PC);
            cpu.PC++;

            cpu.Address = (ushort)((hi << 8) | lo);

            return 0;
        }
        public static byte ABX(_6502 cpu)
        {
            ushort lo = cpu.Bus.Read(cpu.PC);
            cpu.PC++;
            ushort hi = cpu.Bus.Read(cpu.PC);
            cpu.PC++;

            cpu.Address = (ushort)((hi << 8) | lo);
            cpu.Address += cpu.X;

            if ((cpu.Address & 0xFF00) != (hi << 8))
                return 1;
            else
                return 0;
        }
        public static byte ABY(_6502 cpu)
        {
            ushort lo = cpu.Bus.Read(cpu.PC);
            cpu.PC++;
            ushort hi = cpu.Bus.Read(cpu.PC);
            cpu.PC++;

            cpu.Address = (ushort)((hi << 8) | lo);
            cpu.Address += cpu.Y;

            if ((cpu.Address & 0xFF00) != (hi << 8))
                return 1;
            else
                return 0;
        }
        public static byte IND(_6502 cpu)
        {
            ushort ptr_lo = cpu.Bus.Read(cpu.PC);
            cpu.PC++;
            ushort ptr_hi = cpu.Bus.Read(cpu.PC);
            cpu.PC++;

            ushort ptr = (ushort)((ptr_hi << 8) | ptr_lo);

            if (ptr_lo == 0x00FF) // Simulate page boundary hardware bug
            {
                cpu.Address = (ushort)((cpu.Bus.Read((ushort)(ptr & 0xFF00)) << 8) | cpu.Bus.Read((ushort)(ptr + 0)));
            }
            else // Behave normally
            {
                cpu.Address = (ushort)((cpu.Bus.Read((ushort)(ptr + 1)) << 8) | cpu.Bus.Read((ushort)(ptr + 0)));
            }

            return 0;
        }
        public static byte IZX(_6502 cpu)
        {
            ushort t = cpu.Bus.Read(cpu.PC);
            cpu.PC++;

            ushort lo = cpu.Bus.Read((ushort)((t + (ushort)cpu.X) & 0x00FF));
            ushort hi = cpu.Bus.Read((ushort)((t + (ushort)cpu.X + 1) & 0x00FF));

            cpu.Address = (ushort)((hi << 8) | lo);

            return 0;
        }
        public static byte IZY(_6502 cpu)
        {
            ushort t = cpu.Bus.Read(cpu.PC);
            cpu.PC++;

            ushort lo = cpu.Bus.Read((ushort)(t & 0x00FF));
            ushort hi = cpu.Bus.Read((ushort)((t + 1) & 0x00FF));

            cpu.Address = (ushort)((hi << 8) | lo);
            cpu.Address += cpu.Y;

            if ((cpu.Address & 0xFF00) != (hi << 8))
                return 1;
            else
                return 0;
        }

    }
}
