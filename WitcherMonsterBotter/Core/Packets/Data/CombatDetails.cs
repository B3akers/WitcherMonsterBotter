using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public enum CombatDetails : int
    {
        Duration = 0,
        PerfectAttacks = 1,
        Parries = 2,
        Deflects = 3,
        AardCasts = 4,
        IgniCasts = 5,
        QuenCasts = 6,
        StrongAttacks = 7,
        FastAttacks = 8,
        CriticalHits = 9,
        UsedProperOil = 10,
        PerfectParries = 11,
        RequiredDetailsCount = 12
    }
}
