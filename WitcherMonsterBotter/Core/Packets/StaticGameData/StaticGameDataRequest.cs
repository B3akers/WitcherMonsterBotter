using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.StaticGameData
{
    public abstract class StaticGameDataRequest
    {
        public abstract int GetMethodId();
        public abstract byte[] GetData();
    }
}
