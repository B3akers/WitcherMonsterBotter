using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.StaticGameData
{
    public class GetStaticDataUrlRequest : StaticGameDataRequest
    {
        public override byte[] GetData()
        {
            return null;
        }

        public override int GetMethodId()
        {
            return StaticGameDataMessage.TypeId.GET_DATA_URL;
        }
    }
}
