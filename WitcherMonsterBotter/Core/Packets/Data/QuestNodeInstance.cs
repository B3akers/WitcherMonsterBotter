using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class QuestNodeInstance
    {
        [SerializableProperty]
        public long InstanceId { get; set; }

        [SerializableProperty]
        public int QuestNodeId { get; set; }

        [SerializableProperty]
        public string PlaceId { get; set; }

        [SerializableProperty]
        public string SettingsPath { get; set; }

        [SerializableProperty]
        public string BehaviourGraphName { get; set; }

        [SerializableProperty]
        public int DisplayMode { get; set; }
    }
}
