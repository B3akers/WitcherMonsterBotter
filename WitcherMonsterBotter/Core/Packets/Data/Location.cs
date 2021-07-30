using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class Location
    {
        [SerializableProperty]
        public string PlaceId { get; set; }
        [SerializableProperty]
        public float Latitude { get; set; }
        [SerializableProperty]
        public float Longitude { get; set; }
        [SerializableProperty]
        public List<int> Biomes { get; set; }
    }
}
