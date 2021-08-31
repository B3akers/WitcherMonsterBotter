using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Bot.Brewer;
using WitcherMonsterBotter.Bot.Character;
using WitcherMonsterBotter.Bot.Farm;
using WitcherMonsterBotter.Bot.Geo;
using WitcherMonsterBotter.Bot.Inventory;
using WitcherMonsterBotter.Bot.Quests.Daily;
using WitcherMonsterBotter.Core.Logging;
using WitcherMonsterBotter.Core.Packets;
using WitcherMonsterBotter.Core.Packets.Data.Json;

namespace WitcherMonsterBotter.Core.Connection
{
    public class ClientConnection
    {
        public async Task WriteBuffer(byte[] buffer)
        {
            try
            {
                await _stream.WriteAsync(buffer);
            }
            catch (Exception es)
            {
                Logger.Log(Logger.LogType.ERROR, es.ToString());
            }
        }

        public async Task Connect()
        {
            try
            {
                Handler = new(this);

                DailyQuestWorker = new(this);
                FarmIngredientsWorker = new(this);
                MonsterSlayerWorker = new(this);
                InventoryWorker = new(this);
                LocationWorker = new(this);
                BrewerWorker = new(this);
                CharacterWorker = new(this);
                NestFarmingWorker = new(this);

                _client = new();

                await _client.ConnectAsync(ConnectionSettings.IP, ConnectionSettings.PORT);

                _stream = _client.GetStream();

                _ = Task.Run(ConnectionLoop);
            }
            catch (Exception es)
            {
                Logger.Log(Logger.LogType.ERROR, es.ToString());
            }
        }

        private TcpClient _client;
        private NetworkStream _stream = null;

        public PacketHandler Handler;
        public DailyQuestWorker DailyQuestWorker;
        public StaticGameDataJson StaticGameData;
        public FarmIngredientsWorker FarmIngredientsWorker;
        public MonsterSlayerWorker MonsterSlayerWorker;
        public InventoryWorker InventoryWorker;
        public LocationWorker LocationWorker;
        public BrewerWorker BrewerWorker;
        public CharacterWorker CharacterWorker;
        public NestFarmingWorker NestFarmingWorker;

        private async Task ConnectionLoop()
        {
            var buffer = new byte[1024];

            while (true)
            {
                var readedBytes = await _stream.ReadAsync(buffer, 0, 5);
                var bufferOffset = readedBytes;

                if (readedBytes != 5)
                {
                    //Handle reconnect
                    //

                    Logger.Log(Logger.LogType.ERROR, $"We coudn't read packet header {readedBytes} {_client.Connected} {_client.Available}!");
                    continue;
                }

                var packetSizeBigEndian = new byte[4];
                Buffer.BlockCopy(buffer, 1, packetSizeBigEndian, 0, 4);
                Array.Reverse(packetSizeBigEndian);

                var packetSize = BitConverter.ToInt32(packetSizeBigEndian);

                if (buffer.Length < packetSize + 5)
                {
                    var tempBuffer = new byte[buffer.Length];
                    Buffer.BlockCopy(buffer, 0, tempBuffer, 0, buffer.Length);

                    buffer = new byte[packetSize + 1024];
                    Buffer.BlockCopy(tempBuffer, 0, buffer, 0, tempBuffer.Length);
                }

                do
                {
                    readedBytes = await _stream.ReadAsync(buffer, bufferOffset, packetSize - (bufferOffset - 5));
                    bufferOffset += readedBytes;
                } while (bufferOffset != (packetSize + 5));

                Handler.OnRecived(buffer, bufferOffset);
            }
        }
    }
}
