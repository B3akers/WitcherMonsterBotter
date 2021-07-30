using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets
{
    public class AuthMessage
    {
        public struct Methods
        {
            public const int AUTHENTICATE_RESPONSE = 1;
        }

        public struct Type
        {
            public const int AUTHENTICATE = 1;
            public const int DISCONNECT = 2;
            public const int REMOVE = 3;
        }

        public int MethodId;
        public byte[] Data;
    }
}
