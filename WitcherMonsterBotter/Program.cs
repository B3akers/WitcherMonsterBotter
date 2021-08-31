using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
                Logger.Log(Logger.LogType.INFO, "Starting BOT...");

                client.StaticGameData = await client.Handler.FetchStaticGameData();
                client.Handler.OnApiMessage += OnApiMessage;

                await client.BrewerWorker.Update();

                await client.LocationWorker.UpdateCells(50.0614, 19.9372);
              
                Logger.Log(Logger.LogType.SUCCESS, "Loop started!");

                while (true)
                {
                    //Update location
                    //
                    await client.LocationWorker.Update();

                    //Update character
                    //
                    await client.CharacterWorker.Update();
                    
                    //Update inventory
                    //
                    await client.InventoryWorker.Update();
                    await client.InventoryWorker.ClenupInventory();

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

                    //MonsterFarmer update
                    //
                    //await client.MonsterSlayerWorker.KillAllMonster();

                    await Task.Delay(10000);
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

            //e4265b869128d74b2d9a84357afbfee7 - old - d4a88c97b4d8430a //109506898966433352737
            //c89ca114a6a882601302604a220cef78 - new - bd4f3f01624bc833 //104577116967584286117
            await client.WriteBuffer(SocketMessageFactory.SeralizeMessageToSend(SocketMessageFactory.BuildAuthenticationRequest(15, SocketMessageFactory.GetVersionAsLong("1.0.32"), "e4265b869128d74b2d9a84357afbfee7", "109506898966433352737")));

            await Task.Delay(-1);
        }
    }
}
