using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets
{
    public class Message
    {
        public struct TypeId
        {
            public const byte API = 1;
            public const byte LOGGING = 2;
            public const byte AUTHENTICATION = 3;
            public const byte STATIC_GAME_DATA = 4;
        }

        public byte Type;
        public int Size;
        public byte[] Data;
    }
}
