using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW6502
{
    public class Bus
    {
        // creates 64k ram
        private byte[] Ram;
        public _6502 CPU;
        public Bus()
        {
            CPU = new _6502();
            Ram = new byte[0x10000];
            ClearRam();
            CPU.ConnectBus(this);
        }

        public void ClearRam()
        {
            for (int i = 0; i < Ram.Length; i++)
            {
                Ram[i] = 0;
            }
        }

        public void Write(ushort a, byte data)
        {
            Ram[a] = data;
        }

        public byte Read(ushort a, bool readOnly = false)
        {
            if (a >= 0x0000 && a <= 0xffff)
                return Ram[a];
            else
                return 0x00;
        }
    }
}
