using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class Placement<T>
    {
        [SerializableProperty]
        public byte Type { get; set; }

        [SerializableProperty]
        public string PlaceId { get; set; }

        [SerializableProperty]
        public T Entity { get; set; }
    }
}
