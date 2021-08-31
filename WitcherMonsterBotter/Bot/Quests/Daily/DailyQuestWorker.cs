using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Logging;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Bot.Quests.Daily
{
    public class DailyQuestWorker
    {
        private ClientConnection _client;
        public DailyQuestWorker(ClientConnection client) { _client = client; }

        public async Task DoDailyQuests()
        {
            var weeklyStatus = await _client.Handler.GetWeeklyContractProgress();

            if (weeklyStatus.Stamps.Count >= 7)
            {
                var claimResult = await _client.Handler.ClaimWeeklyContract();

                if (claimResult.Result)
                {
                    Logger.Log(Logger.LogType.SUCCESS, $"[DailyQuestWorker] Claimed weekly quest");
                }
            }

            var daily = await _client.Handler.GetDailyContracts();

            if (daily.CanAdd == 1)
            {
                var result = await _client.Handler.AddDailyContract();

                if (result.Success)
                    daily.Contracts.Add(new DailyContract() { DailyContractId = result.Contract, Progress = new int[] { 0 } });
            }

            foreach (var dailyContract in daily.Contracts)
            {
                Logger.Log(Logger.LogType.INFO, $"[DailyQuestWorker] Doing quest id {dailyContract.DailyContractId}");

                var questInfo = _client.StaticGameData.daily_quests.FirstOrDefault(x => x.id == dailyContract.DailyContractId);
                var contract = _client.StaticGameData.contracts.FirstOrDefault(x => x.id == questInfo.contract_id);
                var contractType = _client.StaticGameData.contract_types.FirstOrDefault(x => x.id == contract.contract_type_id).name;
                var questProgress = -1;

                switch (contractType)
                {
                    case "monster slaying":
                        {
                            questProgress = contract.value1;

                            var monsterList = _client.StaticGameData.contract_monsters.Where(x => x.contract_id == contract.id).Select(x => x.monster_id).ToList();
                            var fight = await _client.MonsterSlayerWorker.FightWithMonster(monsterList);
                            if (fight != null)
                            {
                                Logger.Log(Logger.LogType.SUCCESS, $"[DailyQuestWorker] Fight success!");

                                await _client.MonsterSlayerWorker.DropItems(fight.Loot);
                            }
                            else
                            {
                                Logger.Log(Logger.LogType.WARNING, $"[DailyQuestWorker] We coudn't find any proper monsters!");

                                var newLocationResult = await _client.LocationWorker.GetLocationForMonsters(monsterList);
                                if (newLocationResult.Item1)
                                {
                                    _client.LocationWorker.UpdateLocation(newLocationResult.Item2, newLocationResult.Item3);
                                }
                                else if (daily.CanReshuffle == 1)
                                {
                                    Logger.Log(Logger.LogType.INFO, "[DailyQuestWorker] Can't slay this type of monster, reshuffle contract");
                                }
                            }
                        }
                        break;
                    case "items crafted":
                        {
                            questProgress = contract.value2;

                            var itemType = contract.value1;
                            var itemCount = contract.value2;
                            await _client.BrewerWorker.CreateItem(itemType, -1, itemCount - dailyContract.Progress[0]);
                        }
                        break;
                    case "combat_action":
                        {
                            questProgress = contract.value3;

                            var combatDetailsIndex = contract.value2 - 1;
                            var combatValue = contract.value3;

                            var combatDetails = _client.MonsterSlayerWorker.GetRandomCombatDetails();

                            combatDetails[combatDetailsIndex] = combatValue;

                            var fight = await _client.MonsterSlayerWorker.FightWithMonster(_client.StaticGameData.monsters.Select(x => x.id).ToList(), combatDetails);

                            if (fight != null)
                            {
                                Logger.Log(Logger.LogType.SUCCESS, $"[DailyQuestWorker] Fight success!");

                                await _client.MonsterSlayerWorker.DropItems(fight.Loot);
                            }
                        }
                        break;
                    case "nest clearing":
                        {
                            questProgress = contract.value1;

                            await _client.NestFarmingWorker.Update(true);
                        }
                        break;
                    case "combat_prep":
                        {
                            questProgress = contract.value2;

                            var combatItemType = contract.value1;
                            var combatItemCount = contract.value2 - dailyContract.Progress[0];

                            var itemsMap = _client.InventoryWorker.GetItemsByItemType(combatItemType);

                            var totalItems = itemsMap.Values.Sum();
                            if (combatItemCount > totalItems)
                            {
                                Logger.Log(Logger.LogType.INFO, $"[DailyQuestWorker] We don't have enough items!");

                                await _client.BrewerWorker.CreateItem(combatItemType, -1, combatItemCount - totalItems);
                            }
                            else
                            {
                                Core.Packets.Api.Response.CombatEndResponse result = null;

                                switch (combatItemType)
                                {
                                    case ItemTypeId.OILS:
                                        result = await _client.MonsterSlayerWorker.FightWithMonster(itemsMap.FirstOrDefault(x => x.Value > 0).Key);
                                        break;
                                    case ItemTypeId.POTIONS:
                                        result = await _client.MonsterSlayerWorker.FightWithMonster(-1, null, new List<int>() { itemsMap.FirstOrDefault(x => x.Value > 0).Key });
                                        break;
                                }

                                if (result != null)
                                {
                                    Logger.Log(Logger.LogType.SUCCESS, $"[DailyQuestWorker] Fight with prep success!");
                                }
                            }
                        }
                        break;
                    case "bombs used":
                        {
                            questProgress = contract.value2;

                            var bombsCount = contract.value2;
                            var bombsMap = _client.InventoryWorker.GetItemsByItemType(ItemTypeId.BOMBS);

                            var bombsNeeded = bombsCount - dailyContract.Progress[0];
                            var totalBombs = bombsMap.Values.Sum();

                            if (bombsNeeded > totalBombs)
                            {
                                Logger.Log(Logger.LogType.INFO, $"[DailyQuestWorker] We don't have enough bombs!");

                                await _client.BrewerWorker.CreateItem(ItemTypeId.BOMBS, -1, bombsNeeded - totalBombs);
                            }
                            else
                            {
                                var currentBomb = bombsMap.FirstOrDefault(x => x.Value > 0);
                                if ((await _client.MonsterSlayerWorker.FightWithMonster(-1, new List<int>() { currentBomb.Key })) != null)
                                {
                                    Logger.Log(Logger.LogType.SUCCESS, $"[DailyQuestWorker] Fight with throwed bomb success!");
                                }
                            }
                        }
                        break;
                    case "herb_gathering":
                        {
                            questProgress = contract.value1;

                            var plantsCount = contract.value1;

                            foreach (var herb in _client.LocationWorker.GetHerbs().Take(plantsCount))
                            {
                                var resultGather = await _client.Handler.GatherHerb(herb.Entity.InstanceId);
                                if (resultGather.Success)
                                {
                                    Logger.Log(Logger.LogType.SUCCESS, $"[DailyQuestWorker] Gather plant success!");

                                    await _client.MonsterSlayerWorker.DropItems(resultGather.Loot);
                                }
                            }
                        }
                        break;
                    case "distance traveled":
                        {
                            questProgress = contract.value1;
                            var result = await _client.Handler.DistanceTraveled(contract.value1);
                            Logger.Log(result.Result ? Logger.LogType.SUCCESS : Logger.LogType.WARNING, $"[DailyQuestWorker] Distance traveled {result.Result}!");
                        }
                        break;
                    default:
                        Logger.Log(Logger.LogType.WARNING, $"[DailyQuestWorker] We don't have support for contractType: {contractType} contract: {contract.id} quest: {dailyContract.DailyContractId}");
                        break;
                }

                if (dailyContract.Progress[0] >= questProgress)
                {
                    var claimResponse = await _client.Handler.ClaimDailyContract(dailyContract.DailyContractId);

                    if (claimResponse.Success)
                        Logger.Log(Logger.LogType.SUCCESS, $"[DailyQuestWorker] Claimed daily quest");

                    continue;
                }
            }
        }
    }
}
