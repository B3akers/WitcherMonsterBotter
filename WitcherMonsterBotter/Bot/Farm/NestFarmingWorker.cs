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
        public async Task Update(bool forceFight = false)
        {
            if (_client.CharacterWorker.Level >= 10)
            {
                var nest = _client.LocationWorker.GetNests().FirstOrDefault(x => x.Entity.NestState == (int)NestState.Default || x.Entity.NestState == (int)NestState.Lured);

                if (nest == null)
                {
                    Logger.Log(Logger.LogType.WARNING, "We coudn't find nests!");

                    var locationResult = await Geo.GeoLocationFinder.FindLocation(true, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 });
                    if (locationResult.Item1)
                        _client.LocationWorker.UpdateLocation(locationResult.Item2, locationResult.Item3);
                }
                else
                {
                    var nestEnter = await _client.Handler.EncounterNest(nest.Entity.InstanceId);
                    if (nestEnter.Result)
                    {
                        //Check if we can get gold from nest
                        //
                        if (forceFight || nestEnter.Gold > 0)
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
