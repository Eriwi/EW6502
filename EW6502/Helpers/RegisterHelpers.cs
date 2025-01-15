using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EW6502.StatusFlags;

namespace EW6502.Helpers
{
    public static class RegisterHelpers
    {
        public static void SetFlag(Flag flag, byte register)
        {
            register |= (byte)flag;
        }

        public static void ClearFlag(Flag flag, byte register)
        {
            register &= (byte)~flag;
        }

        public static bool IsFlag(Flag flag, byte register)
        {
            return (register & (byte)flag) != 0;
        }

        public static void ToggleFlag(Flag flag, byte register)
        {
            register = (byte)(register ^ (byte)flag);
        }

        public static List<Flag> GetSetFlags(byte register)
        {
            return Enum.GetValues(typeof(Flag)).OfType<Flag>().ToList().Where(f => IsFlag(f, register)).ToList();
        }
    }
}
