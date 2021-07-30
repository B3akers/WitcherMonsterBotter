using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Logging;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Bot.Farm
{
    public class NestFarmingWorker
    {
        private ClientConnection _client;
        public NestFarmingWorker(ClientConnection client) { _client = client; }
        public async Task Update()
        {
            if (_client.CharacterWorker.Level >= 10)
            {
                var nest = _client.LocalizationWorker.GetNests().FirstOrDefault(x => x.Entity.NestState == (int)NestState.Default || x.Entity.NestState == (int)NestState.Lured);

                if (nest == null)
                {
                    Logger.Log(Logger.LogType.WARNING, "We coudn't find nests!");
                    _client.LocalizationWorker.RequestLocationUpdate = true;
                }
                else
                {
                    var nestEnter = await _client.Handler.EncounterNest(nest.Entity.InstanceId);
                    if (nestEnter.Result)
                    {
                        //Check if we can get gold from nest
                        //
                        if (nestEnter.Gold > 0)
                        {
                            var nestBattle = await _client.Handler.EndNestCombat(true, nest.Entity.InstanceId, _client.MonsterSlayerWorker.GetRandomCombatDetails(), _client.MonsterSlayerWorker.GetRandomCombatDetails(), _client.MonsterSlayerWorker.GetRandomCombatDetails());
                            if (nestBattle.Success)
                            {   
                                Logger.Log(Logger.LogType.SUCCESS, $"We won! Nest completed! Gold: {nestBattle.Reward} Loot: {nestBattle.Loot.Count}");

                                await _client.MonsterSlayerWorker.DropItems(nestBattle.Loot);

                                nest.Entity.NestState++;
                            }
                        }
                    }
                    else
                    {
                        nest.Entity.NestState++;
                    }
                }
            }
            else
            {
                Logger.Log(Logger.LogType.INFO, $"We have {_client.CharacterWorker.Level}. Farming 10lvl to clear nests!");

                await _client.MonsterSlayerWorker.KillAllPossibleMonsters();
            }
        }
    }
}
