using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class SpawnMonsterRequest : ApiRequest
    {
        [SerializableProperty]
        public float Latitude { get; set; }

        [SerializableProperty]
        public float Longitude { get; set; }

        [SerializableProperty]
        public int Type { get; set; }

        [SerializableProperty]
        public int Level { get; set; }

        [SerializableProperty]
        public int Ttl { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.SpawnMonster;
        }
    }
}
