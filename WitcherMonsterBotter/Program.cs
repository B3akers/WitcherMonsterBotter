using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Geo;
using WitcherMonsterBotter.Core.Logging;
using WitcherMonsterBotter.Core.Packets;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter
{
    class Program
    {
        private static ClientConnection client;

        static async Task OnApiMessage(object e, Core.Packets.Api.Response.ApiResponse message)
        {
            var handler = (PacketHandler)e;

            if (message.GetMethodId() == Core.Packets.Api.TypeMessage.Method.DailyContractCompleted)
            {
                var dailyContractCompletedResponse = (Core.Packets.Api.Response.DailyContractCompletedResponse)message;
                var result = await handler.ClaimDailyContract(dailyContractCompletedResponse.Contract);

                if (result.Success)
                    Logger.Log(Logger.LogType.SUCCESS, $"Claimed daily quest from DailyContractCompletedResponse {dailyContractCompletedResponse.Contract}");
            }
        }

        static async Task BotMainLoop()
        {
            try
            {
                client.StaticGameData = await client.Handler.FetchStaticGameData();
                client.Handler.OnApiMessage += OnApiMessage;

                await client.BrewerWorker.Update();
                await client.LocalizationWorker.Update(50.020912, 20.006865);

                // var result = await client.Handler.GetActiveQuestNodeInstances();
                //
                // Console.WriteLine("ExpiringQuestNodeInstances");
                //
                // foreach (var quest in result.ExpiringQuestNodeInstances)
                // {
                //     Console.WriteLine($"{quest.Key} {quest.Value}");
                // }
                //
                // Console.WriteLine("GetAllFacts");
                //
                // var reusltFacts = await client.Handler.GetAllFacts();
                //
                // foreach(var fact in reusltFacts.Facts)
                // {
                //     Console.WriteLine($"{fact.Key} {fact.Value}");
                // }

/*
10144 0
96 1
10145 4
2 1
3 1
420 1
102 1
71 0
103 1
107 1
203 0
108 1
77 2
113 1
114 3
87 0
88 1
89 0
90 0
91 1
123 0
190 0
94 2
95 0
*/

                //SetFacts
                //SetFacts
                //SetFacts
                //EndBehaviourGraph
                // Console.WriteLine("QuestNodeInstances");
                //
                // foreach (var quest in result.QuestNodeInstances)
                // {
                //     Console.WriteLine($"{quest.InstanceId} {quest.QuestNodeId}");
                // }

                while (true)
                {
                    //Update localization
                    //
                    await client.LocalizationWorker.Update();

                    //Update character
                    //
                    await client.CharacterWorker.Update();

                    //Update inventory
                    //
                    await client.InventoryWorker.Update();

                    //BrewerWorker update
                    //
                    await client.BrewerWorker.UpdateBrewers();
                    await client.BrewerWorker.ClaimReadyBrewers();

                    //QuestWorker update
                    //
                    await client.DailyQuestWorker.DoDailyQuests();

                    //NestFarming update
                    //
                    await client.NestFarmingWorker.Update();

                    await Task.Delay(5000);
                }
            }
            catch (Exception es)
            {
                Logger.Log(Logger.LogType.ERROR, es.ToString());
            }
        }

        static async Task Main(string[] args)
        {
            Logger.Log(Logger.LogType.INFO, $"Starting bot...");

            Logger.Log(Logger.LogType.INFO, $"Connecting...");

            client = new ClientConnection();
            await client.Connect();

            Logger.Log(Logger.LogType.SUCCESS, $"Connected");
            Logger.Log(Logger.LogType.INFO, $"Sending authentication request...");

            client.Handler.OnAuthResponse += (e, response) =>
            {
                Logger.Log(response.Success ? Logger.LogType.SUCCESS : Logger.LogType.ERROR, $"AuthenticationResponse: {response.ErrCode}");

                if (response.Success)
                {
                    _ = Task.Run(BotMainLoop);
                }
            };

            //fd33479528dabb2875d9a670ce1c99e6 - new
            //e4265b869128d74b2d9a84357afbfee7 - old

            await client.WriteBuffer(SocketMessageFactory.SeralizeMessageToSend(SocketMessageFactory.BuildAuthenticationRequest(15, SocketMessageFactory.GetVersionAsLong("1.0.32"), "e4265b869128d74b2d9a84357afbfee7", "")));

            Console.ReadLine();
        }
    }
}
