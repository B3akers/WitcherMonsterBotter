using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class Item
    {
        [SerializableProperty]
        public int Type { get; set; }

        [SerializableProperty]
        public int ItemId { get; set; }
    }
}
