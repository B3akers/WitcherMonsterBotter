using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public abstract class PrepareToCombatAbstractRequest : ApiRequest
    {
        [SerializableProperty]
        public List<int> Bombs { get; set; }
        [SerializableProperty]
        public List<int> Potions { get; set; }
        [SerializableProperty]
        public int Oil { get; set; }
    }
}
