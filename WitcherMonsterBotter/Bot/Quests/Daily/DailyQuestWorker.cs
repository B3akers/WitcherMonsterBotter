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
            var daily = await _client.Handler.GetDailyContracts();

            foreach (var dailyContract in daily.Contracts)
            {
                if (!DailyQuests.TryGetValue(dailyContract.DailyContractId, out var questInfo))
                {
                    Logger.Log(Logger.LogType.WARNING, $"We coudn't find QuestInfo for {dailyContract.DailyContractId}");
                    continue;
                }

                if (questInfo.Progress == dailyContract.Progress[0])
                {
                    var claimResponse = await _client.Handler.ClaimDailyContract(dailyContract.DailyContractId);

                    if (claimResponse.Success)
                        Logger.Log(Logger.LogType.SUCCESS, $"Claimed daily quest");

                    continue;
                }

                if (questInfo.Job == DailyQuestJob.CreateOil)
                {
                    await _client.BrewerWorker.CreateItem(ItemTypeId.OILS, -1, questInfo.Progress - dailyContract.Progress[0]);
                }
                else if (questInfo.Job == DailyQuestJob.CreateBombs)
                {
                    await _client.BrewerWorker.CreateItem(ItemTypeId.BOMBS, -1, questInfo.Progress - dailyContract.Progress[0]);
                }
                else if (questInfo.Job == DailyQuestJob.OilWeapon)
                {
                    var oilNeeded = questInfo.Progress - dailyContract.Progress[0];
                    var totalOils = _client.InventoryWorker.GetInventory().OilMap.Values.Sum();

                    if (oilNeeded > totalOils)
                    {
                        Logger.Log(Logger.LogType.INFO, $"We don't have enough oils!");

                        await _client.BrewerWorker.CreateItem(ItemTypeId.OILS, -1, oilNeeded - totalOils);
                    }
                    else
                    {
                        if ((await _client.MonsterSlayerWorker.FightWithMonster(_client.InventoryWorker.GetInventory().OilMap.Keys.First())) != null)
                        {
                            Logger.Log(Logger.LogType.SUCCESS, $"Fight with oil weapon success!");
                        }
                    }
                }
                else if (questInfo.Job == DailyQuestJob.UseBombs)
                {
                    var bombsNeeded = questInfo.Progress - dailyContract.Progress[0];
                    var totalBombs = _client.InventoryWorker.GetInventory().BombMap.Values.Sum();

                    if (bombsNeeded > totalBombs)
                    {
                        Logger.Log(Logger.LogType.INFO, $"We don't have enough bombs!");

                        await _client.BrewerWorker.CreateItem(ItemTypeId.BOMBS, -1, bombsNeeded - totalBombs);
                    }
                    else
                    {
                        var currentBomb = _client.InventoryWorker.GetInventory().BombMap.FirstOrDefault(x => x.Value > 0);
                        if ((await _client.MonsterSlayerWorker.FightWithMonster(-1, new List<int>() { currentBomb.Key })) != null)
                        {
                            Logger.Log(Logger.LogType.SUCCESS, $"Fight with throwed bomb success!");
                        }
                    }

                }
                else
                {
                    Logger.Log(Logger.LogType.WARNING, $"{questInfo.Job} not implemented!");
                    continue;
                }
            }
        }

        public static readonly Dictionary<int, DailyQuestsInfo> DailyQuests = new()
        {
            { 23, new DailyQuestsInfo() { ContractId = 23, Progress = 3, Job = DailyQuestJob.CreateOil } },
            { 24, new DailyQuestsInfo() { ContractId = 24, Progress = 2, Job = DailyQuestJob.CreateBombs } },
            { 29, new DailyQuestsInfo() { ContractId = 29, Progress = 4, Job = DailyQuestJob.OilWeapon } },
            { 27, new DailyQuestsInfo() { ContractId = 27, Progress = 3, Job = DailyQuestJob.UseBombs } }
        };
    }
}
