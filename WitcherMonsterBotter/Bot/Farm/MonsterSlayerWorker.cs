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

        public async Task KillAllMonster()
        {
            var killedMonsters = await _client.Handler.GetKilledMonsters();

            //Kill monsters
            //
            foreach (var monster in _client.LocationWorker.GetMonsters())
            {
                var requiredKillCount = _client.StaticGameData.monster_descriptions.Where(x => x.monster_id == monster.Entity.Type).Sum(x => x.threshold);

                if (killedMonsters.KilledMonsters.TryGetValue(monster.Entity.Type, out var killedCount))
                {
                    if (killedCount >= requiredKillCount)
                        continue;
                }

                var encounterMonster = await _client.Handler.EncounterMonster(monster.Entity.InstanceId);
                if (encounterMonster.Result)
                {
                    var result = await _client.Handler.CombatEnd(true, GetRandomCombatDetails(), false);
                    await DropItems(result.Loot);

                    killedMonsters.KilledMonsters[monster.Entity.Type] = killedCount + 1;

                    var monsterInfo = _client.StaticGameData.monsters.FirstOrDefault(x => x.id == monster.Entity.Type);

                    Logger.Log(Logger.LogType.SUCCESS, $"Killing monster {monsterInfo.name} {monsterInfo.rarity} {killedCount} < {requiredKillCount}");
                }
            }

            //Kill nests
            //

            if (_client.CharacterWorker.Level >= 10)
            {
                foreach (var nest in _client.LocationWorker.GetNests())
                {
                    if (nest.Entity.NestState != (int)NestState.Default && nest.Entity.NestState != (int)NestState.Lured)
                        continue;

                    var nestEnter = await _client.Handler.EncounterNest(nest.Entity.InstanceId);

                    if (nestEnter.Result)
                    {
                        bool fightWithNest = false;
                        foreach (var monster in nestEnter.Monsters)
                        {
                            var requiredKillCount = _client.StaticGameData.monster_descriptions.Where(x => x.monster_id == monster).Sum(x => x.threshold);

                            if (killedMonsters.KilledMonsters.TryGetValue(monster, out var killedCount))
                            {
                                if (killedCount >= requiredKillCount)
                                    continue;
                            }

                            fightWithNest = true;
                            break;
                        }

                        if (fightWithNest)
                        {
                            var nestBattle = await _client.Handler.EndNestCombat(true, nest.Entity.InstanceId, _client.MonsterSlayerWorker.GetRandomCombatDetails(), _client.MonsterSlayerWorker.GetRandomCombatDetails(), _client.MonsterSlayerWorker.GetRandomCombatDetails());
                            if (nestBattle.Success)
                            {
                                await _client.MonsterSlayerWorker.DropItems(nestBattle.Loot);

                                nest.Entity.NestState++;

                                foreach (var monster in nestEnter.Monsters)
                                {
                                    var requiredKillCount = _client.StaticGameData.monster_descriptions.Where(x => x.monster_id == monster).Sum(x => x.threshold);

                                    killedMonsters.KilledMonsters.TryGetValue(monster, out var killedCount);
                                    killedMonsters.KilledMonsters[monster] = killedCount + 1;

                                    var monsterInfo = _client.StaticGameData.monsters.FirstOrDefault(x => x.id == monster);

                                    Logger.Log(Logger.LogType.SUCCESS, $"Killing monster from nest {monsterInfo.name} {monsterInfo.rarity} {killedCount} < {requiredKillCount}");
                                }
                            }
                        }
                    }
                }
            }

            //Claim already killed monsters
            //
            foreach (var monster in killedMonsters.KilledMonsters)
            {
                var requiredKillCountLevels = _client.StaticGameData.monster_descriptions.Where(x => x.monster_id == monster.Key).OrderBy(x => x.level).ToList();
                var totalKillsToClaim = 0;

                if (!killedMonsters.ClaimedMonsterKnowledgeTiers.TryGetValue(monster.Key, out var claimedLevel))
                    claimedLevel = 0;

                foreach (var requiredKills in requiredKillCountLevels)
                {
                    totalKillsToClaim += requiredKills.threshold;

                    if (requiredKills.level > claimedLevel && monster.Value >= totalKillsToClaim)
                    {
                        var claimResult = await _client.Handler.ClaimMonsterKnowledgeReward(monster.Key);

                        if (claimResult.Success)
                        {
                            var monsterInfo = _client.StaticGameData.monsters.FirstOrDefault(x => x.id == monster.Key);

                            Logger.Log(Logger.LogType.SUCCESS, $"We ClaimMonsterKnowledgeReward from {monsterInfo.name} level {requiredKills.level}");

                            killedMonsters.ClaimedMonsterKnowledgeTiers[monster.Key] = claimedLevel + 1;
                        }
                    }
                }
            }

            //Make list which we need to kill
            //
            var monsterMissing = new HashSet<int>();

            foreach (var monster in _client.StaticGameData.monsters)
            {
                if (!killedMonsters.ClaimedMonsterKnowledgeTiers.TryGetValue(monster.id, out var claimedLevel))
                    claimedLevel = 0;

                var maxLevel = _client.StaticGameData.monster_descriptions.Where(x => x.monster_id == monster.id).Max(x => x.level);

                if (claimedLevel < maxLevel)
                {
                    monsterMissing.Add(monster.id);
                }
            }

            var newLocationResult = await _client.LocationWorker.GetLocationForMonsters(monsterMissing.ToList());
            if (newLocationResult.Item1)
                _client.LocationWorker.UpdateLocation(newLocationResult.Item2, newLocationResult.Item3);
        }

        public async Task KillAllPossibleMonsters()
        {
            foreach (var monster in _client.LocationWorker.GetMonsters())
            {
                var encounterMonster = await _client.Handler.EncounterMonster(monster.Entity.InstanceId);
                if (encounterMonster.Result)
                {
                    var result = await _client.Handler.CombatEnd(true, GetRandomCombatDetails(), false);
                    await DropItems(result.Loot);
                }
            }

            var locationResult = await Geo.GeoLocationFinder.FindLocation(true, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 });
            if (locationResult.Item1)
                _client.LocationWorker.UpdateLocation(locationResult.Item2, locationResult.Item3);
            _client.LocationWorker.GetMonsters().Clear();
        }

        public async Task<Core.Packets.Api.Response.CombatEndResponse> FightWithMonster(List<int> types, int[] combatDetails = null)
        {
        TRY_AGAIN:
            var monster = _client.LocationWorker.GetMonsters().FirstOrDefault(x => types.Contains(x.Entity.Type));
            if (monster != null)
            {
                var encounterMonster = await _client.Handler.EncounterMonster(monster.Entity.InstanceId);
                if (encounterMonster.Result)
                {
                    if (combatDetails == null)
                        combatDetails = GetRandomCombatDetails();

                    _client.LocationWorker.GetMonsters().Remove(monster);

                    return await _client.Handler.CombatEnd(true, combatDetails, false);
                }
                else
                {
                    _client.LocationWorker.GetMonsters().Remove(monster);
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

        public async Task<Core.Packets.Api.Response.CombatEndResponse> FightWithMonster(int oil = -1, List<int> bombs = null, List<int> potions = null, bool dropLoot = true, int[] combatDetails = null)
        {
            var monsters = _client.LocationWorker.GetMonsters();

            for (int i = monsters.Count - 1; i >= 0; i--)
            {
                var encounterMonster = await _client.Handler.EncounterMonster(monsters[i].Entity.InstanceId);
                if (encounterMonster.Result)
                {
                    bool process = true;
                    if (oil != -1 || bombs != null || potions != null)
                    {
                        process = (await _client.Handler.PrepareToCombat(bombs == null ? new() : bombs, potions == null ? new() : potions, oil)).Result;
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
