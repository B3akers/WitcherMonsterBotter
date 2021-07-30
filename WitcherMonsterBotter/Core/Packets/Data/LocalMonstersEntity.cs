using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class LocalMonstersEntity
    {
        [SerializableProperty]
        public int SummoningItemType { get; set; }
        [SerializableProperty]
        public int SummoningItemId { get; set; }
        [SerializableProperty]
        public int StartTime { get; set; }
        [SerializableProperty]
        public int DespawnTime { get; set; }
        [SerializableProperty]
        public int Longitude { get; set; }
        [SerializableProperty]
        public int Latitude { get; set; }
        [SerializableProperty]
        public List<SummonedMonsterEntity> SummonedMonsterEntities { get; set; }
    }
}
