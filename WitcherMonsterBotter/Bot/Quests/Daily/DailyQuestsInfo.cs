using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Bot
{
    public enum DailyQuestJob
    {
        CreateOil,
        CreateBombs,
        OilWeapon,
        UseBombs
    }

    public class DailyQuestsInfo
    {
        public int ContractId { get; set; }

        public int Progress { get; set; }

        public DailyQuestJob Job { get; set; }
    }
}
