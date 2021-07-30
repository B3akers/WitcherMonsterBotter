using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class Monster
    {
        private long _spawnTime;
        [SerializableProperty]
        public long SpawnTime
        {
            get
            {
                return _spawnTime;
            }
            set
            {
                _spawnTime = value / 1000;
            }
        }

        [SerializableProperty]
        public int Ttl { get; set; }

        [SerializableProperty]
        public int Type { get; set; }

        [SerializableProperty]
        public int Level { get; set; }

        [SerializableProperty]
        public long InstanceId { get; set; }
    }
}
