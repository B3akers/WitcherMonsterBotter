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
    public class MonsterSlayerWorker
    {
        private Random _random;
        private ClientConnection _client;
        public MonsterSlayerWorker(ClientConnection client) { _client = client; _random = new(); }

        public int[] GetRandomCombatDetails()
        {
            var combatDetails = new int[(int)CombatDetails.RequiredDetailsCount];

            combatDetails[(int)CombatDetails.Duration] = _random.Next(15, 35);
            combatDetails[(int)CombatDetails.PerfectAttacks] = 0;
            combatDetails[(int)CombatDetails.Parries] = 0;
            combatDetails[(int)CombatDetails.Deflects] = 0;
            combatDetails[(int)CombatDetails.AardCasts] = 0;
            combatDetails[(int)CombatDetails.IgniCasts] = 0;
            combatDetails[(int)CombatDetails.QuenCasts] = 0;
            combatDetails[(int)CombatDetails.StrongAttacks] = 0;
            combatDetails[(int)CombatDetails.FastAttacks] = _random.Next(10, 25);
            combatDetails[(int)CombatDetails.CriticalHits] = 0;
            combatDetails[(int)CombatDetails.UsedProperOil] = 0;
            combatDetails[(int)CombatDetails.PerfectParries] = _random.Next(0, 5);

            return combatDetails;
        }

        public async Task KillAllPossibleMonsters()
        {
            foreach (var monster in _client.LocalizationWorker.GetMonsters())
            {
                var encounterMonster = await _client.Handler.EncounterMonster(monster.Entity.InstanceId);
                if (encounterMonster.Result)
                {
                    var result = await _client.Handler.CombatEnd(true, GetRandomCombatDetails(), false);
                    await DropItems(result.Loot);
                }
            }

            _client.LocalizationWorker.RequestLocationUpdate = true;
            _client.LocalizationWorker.GetMonsters().Clear();
        }

        public async Task<Core.Packets.Api.Response.CombatEndResponse> FightWithMonster(List<int> types, int[] combatDetails = null)
        {
            TRY_AGAIN:
            var monster = _client.LocalizationWorker.GetMonsters().FirstOrDefault(x => types.Contains(x.Entity.Type));
            if (monster != null)
            {
                var encounterMonster = await _client.Handler.EncounterMonster(monster.Entity.InstanceId);
                if (encounterMonster.Result)
                {
                    if (combatDetails == null)
                        combatDetails = GetRandomCombatDetails();

                    _client.LocalizationWorker.GetMonsters().Remove(monster);

                    return await _client.Handler.CombatEnd(true, combatDetails, false);
                }
                else
                {
                    _client.LocalizationWorker.GetMonsters().Remove(monster);
                    goto TRY_AGAIN;
                }
            }
            return null;
        }

        public async Task DropItems(Dictionary<int, int> items)
        {
            var ingredientsList = _client.StaticGameData.ingredients.Select(x => x.id).ToList();

            foreach (var item in items)
            {
                if (ingredientsList.Contains(item.Key))
                    await _client.Handler.DropIngredients(item.Key, item.Value);
                else
                    Logger.Log(Logger.LogType.WARNING, $"We wanted to drop item which is not ingredient {item.Key}");
            }
        }

        public async Task DropItems(List<int> loot)
        {
            Dictionary<int, int> itemsDropped = new();
            foreach (var item in loot)
            {
                if (itemsDropped.TryGetValue(item, out var count))
                    count++;
                else
                    count = 1;

                itemsDropped[item] = count;
            }

            await DropItems(itemsDropped);

            loot.Clear();
        }

        public async Task<Core.Packets.Api.Response.CombatEndResponse> FightWithMonster(int oil = -1, List<int> bombs = null, bool dropLoot = true, int[] combatDetails = null)
        {
            var monsters = _client.LocalizationWorker.GetMonsters();

            for (int i = monsters.Count - 1; i >= 0; i--)
            {
                var encounterMonster = await _client.Handler.EncounterMonster(monsters[i].Entity.InstanceId);
                if (encounterMonster.Result)
                {
                    bool process = true;
                    if (oil != -1 || bombs != null)
                    {
                        process = (await _client.Handler.PrepareToCombat((bombs == null || bombs.Count <= 0) ? new() : bombs, new(), oil)).Result;
                    }

                    if (process)
                    {
                        if (combatDetails == null)
                            combatDetails = GetRandomCombatDetails();

                        monsters.RemoveAt(i);

                        if (bombs != null && bombs.Count > 0)
                        {
                            await _client.Handler.ThrowBomb(bombs.First());
                        }

                        var resultBattle = await _client.Handler.CombatEnd(true, combatDetails, false);
                        if (dropLoot)
                        {
                            await DropItems(resultBattle.Loot);
                        }

                        return resultBattle;
                    }
                }

                monsters.RemoveAt(i);
            }
            return null;
        }
    }
}
