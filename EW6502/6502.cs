using EW6502.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EW6502.StatusFlags;

namespace EW6502
{
    public class _6502
    {
        // The main registers of the 6502.
        public byte A { get; set; } // The accumulator
        public byte X { get; set; } // The X register
        public byte Y { get; set; } // The Y register
        public byte Sptr { get; set; } // The stack pointer
        public ushort PC { get; set; } // The programcounter, note it is 16 bits since the 6502 is an 8-bit 
        public byte Status { get; set; } // The status register

        //Helper variables to assist emulation
        public byte Fetched { get; set; } // Input to the ALU 
        public ushort Address { get; set; } // Memory address
        public ushort AddressRel { get; set; } // Address following a branch
        public byte Opcode { get; set; } // Byte for current instruction
        public byte Cycles { get; set; } // Keeps track of how many cycles remain in the current instruction
        public int Clocks { get; set; } // Global counter of number of clock cycles since startup/reset.

        //A reference to the shared Bus
        public Bus Bus;

        


        public _6502()
        {
        }

        public void ConnectBus(Bus bus)
        {
            Bus = bus;
        }

        /// <summary>
        /// Resets the CPU to an intial state.
        /// </summary>
        public void Reset()
        {
            // Reset the registers
            A = 0;
            X = 0;
            Y = 0;
            Sptr = 0xFD;
            Status = 0x00 | (byte)Flag.U;

            //The 6502 initializes its pc by looking at the the data in 0xFFFC of Ram
            Address = 0xFFFC;
            //The 6502 is a little endian processor so the low byte is stored in the lower part of the RAM
            var lo = Bus.Read(Address);
            var hi = (Bus.Read((ushort)(Address + 1)));
            PC = (ushort)(lo | (hi << 8));

            //Clear helpers
            Fetched = 0x00;
            Address = 0x0000;
            AddressRel = 0x0000;

            //Account for reset time
            Cycles = 8;

        }

        public void Clock()
        {
            if (Cycles == 0)
            {
                // Fetch the opcode from the memory adress pointed to by the Address helper variable.
                var opcode = Bus.Read(Address);

                RegisterHelpers.SetFlag(Flag.U, Status);
            }

            Cycles--;
            Clocks++;
        }

        //A bunch of helper functions for manipulating the status register
        public void SetFlag(Flag flag)
        {
            Status |= (byte)flag;
        }

        public void ClearFlag(Flag flag)
        {
            Status &= (byte)~flag;
        }

        public bool IsFlag(Flag flag)
        {
            return (Status & (byte)flag) != 0;
        }

        public void ToggleFlag(Flag flag)
        {
            Status = (byte)(Status ^ (byte)flag);
        }

        public List<Flag> GetSetFlags()
        {
            return Enum.GetValues(typeof(Flag)).OfType<Flag>().ToList().Where(f => IsFlag(f)).ToList();
        }
    }
}
