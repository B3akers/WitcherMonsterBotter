using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.StaticGameData
{
    public class FetchStaticGameDataResponse
    {
        [SerializableProperty]
        public byte[] Data { get; set; }
    }
}
