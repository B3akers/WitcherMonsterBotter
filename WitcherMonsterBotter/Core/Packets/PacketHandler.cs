using Hina.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Logging;
using WitcherMonsterBotter.Core.Packets.Auth.Methods;
using WitcherMonsterBotter.Core.Utility;

namespace WitcherMonsterBotter.Core.Packets
{
    public class PacketHandler
    {
        private readonly static HttpClient _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip
        });

        private ClientConnection _client;
        private Random _random;

        public event AuthenticationResponseHandler OnAuthResponse = delegate { };
        public delegate void AuthenticationResponseHandler(object sender, AuthenticationResponse e);

        public event Func<object, Api.Response.ApiResponse, Task> OnApiMessage;

        private readonly TaskCallbackManager<long, object> _callbacksApiRequests;

        private readonly TaskCallbackManager<int, object> _callbacksStaticGameDataRequests;

        private ConcurrentBag<long> _receivedApiMessages;

        public PacketHandler(ClientConnection clientConnection)
        {
            _client = clientConnection;
            _random = new();
            _callbacksApiRequests = new();
            _receivedApiMessages = new();
            _callbacksStaticGameDataRequests = new();
        }

        public async Task<Api.Response.GetWeeklyContractProgressResponse> GetWeeklyContractProgress()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetWeeklyContractProgressResponse>((await SendApiMessage(new Api.Request.GetWeeklyContractProgressRequest())).Message.Data);
        }
        public async Task<Api.Response.DistanceTraveledResponse> DistanceTraveled(int param)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.DistanceTraveledResponse>((await SendApiMessage(new Api.Request.DistanceTraveledRequest() { Param = param })).Message.Data);
        }
        public async Task<Api.Response.ClaimWeeklyContractResponse> ClaimWeeklyContract()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.ClaimWeeklyContractResponse>((await SendApiMessage(new Api.Request.ClaimWeeklyContractRequest())).Message.Data);
        }
        public async Task<Api.Response.SetNameResponse> SetName(string name)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.SetNameResponse>((await SendApiMessage(new Api.Request.SetNameRequest() { Name = name })).Message.Data);
        }
        public async Task<Api.Response.SpawnMonsterResponse> SpawnMonster(float latitude, float longitude, int type, int level, int ttl)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.SpawnMonsterResponse>((await SendApiMessage(new Api.Request.SpawnMonsterRequest() { Latitude = latitude, Longitude = longitude, Type = type, Level = level, Ttl = ttl })).Message.Data);
        }
        public async Task<Api.Response.ClaimMonsterKnowledgeRewardResponse> ClaimMonsterKnowledgeReward(int monsterId)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.ClaimMonsterKnowledgeRewardResponse>((await SendApiMessage(new Api.Request.ClaimMonsterKnowledgeRewardRequest() { MonsterId = monsterId })).Message.Data);
        }
        public async Task<Api.Response.CombatEndResponse> CombatEnd(bool win, int[] details, bool surrender)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.CombatEndResponse>((await SendApiMessage(new Api.Request.CombatEndRequest() { Win = win, Details = details, PlayerSurrendered = surrender })).Message.Data);
        }
        public async Task<Api.Response.EncounterMonsterResponse> EncounterMonster(long instanceId)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.EncounterMonsterResponse>((await SendApiMessage(new Api.Request.EncounterMonsterRequest() { Param = instanceId })).Message.Data);
        }
        public async Task<Api.Response.GetAllFactsResponse> GetAllFacts()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetAllFactsResponse>((await SendApiMessage(new Api.Request.GetAllFactsRequest())).Message.Data);
        }
        public async Task<Api.Response.GetInitialPlayerDataResponse> GetInitialPlayerData()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetInitialPlayerDataResponse>((await SendApiMessage(new Api.Request.GetInitialPlayerDataRequest())).Message.Data);
        }
        public async Task<Api.Response.GetActiveQuestNodeInstancesResponse> GetActiveQuestNodeInstances()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetActiveQuestNodeInstancesResponse>((await SendApiMessage(new Api.Request.GetActiveQuestNodeInstancesRequest())).Message.Data);
        }
        public async Task<Api.Response.LoadCellsResponse> LoadCells(List<ulong> cellsId)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.LoadCellsResponse>((await SendApiMessage(new Api.Request.LoadCellsRequest() { _cellIds = cellsId })).Message.Data);
        }
        public async Task<Api.Response.GetLocationsByCellResponse> GetLocationsByCell(List<ulong> cellsId)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetLocationsByCellResponse>((await SendApiMessage(new Api.Request.GetLocationsByCellRequest() { _cellIds = cellsId })).Message.Data);
        }
        public async Task<Api.Response.GetKilledMonstersResponse> GetKilledMonsters()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetKilledMonstersResponse>((await SendApiMessage(new Api.Request.GetKilledMonstersRequest())).Message.Data);
        }
        public async Task<Api.Response.GetDailyContractsResponse> GetDailyContracts()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetDailyContractsResponse>((await SendApiMessage(new Api.Request.GetDailyContractsRequest())).Message.Data);
        }
        public async Task<Api.Response.GetInventoryResponse> GetInventory()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetInventoryResponse>((await SendApiMessage(new Api.Request.GetInventoryRequest())).Message.Data);
        }
        public async Task<Api.Response.GetKnownRecipesResponse> GetKnownRecipes()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetKnownRecipesResponse>((await SendApiMessage(new Api.Request.GetKnownRecipesRequest())).Message.Data);
        }
        public async Task<Api.Response.GetBrewersResponse> GetBrewers()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetBrewersResponse>((await SendApiMessage(new Api.Request.GetBrewersRequest())).Message.Data);
        }
        public async Task<Api.Response.ClaimRecipeResponse> ClaimRecipe(long instanceId)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.ClaimRecipeResponse>((await SendApiMessage(new Api.Request.ClaimRecipeRequest() { BrewerInstanceId = instanceId })).Message.Data);
        }
        public async Task<Api.Response.EncounterNestResponse> EncounterNest(long instanceId)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.EncounterNestResponse>((await SendApiMessage(new Api.Request.EncounterNestRequest() { InstanceId = instanceId })).Message.Data);
        }
        public async Task<Api.Response.EndNestCombatResponse> EndNestCombat(bool win, long instanceId, int[] detailsFirst, int[] detailsSecond, int[] detailsBoss)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.EndNestCombatResponse>((await SendApiMessage(new Api.Request.EndNestCombatRequest() { Win = win, DetailsFirst = detailsFirst, DetailsSecond = detailsSecond, DetailsBoss = detailsBoss, InstanceId = instanceId })).Message.Data);
        }
        public async Task<Api.Response.DropIngredientsResponse> DropIngredients(int itemId, int amount)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.DropIngredientsResponse>((await SendApiMessage(new Api.Request.DropIngredientsRequest() { ItemId = itemId, Amount = amount })).Message.Data);
        }
        public async Task<Api.Response.DropFriendPackResponse> DropFriendPack(int itemId, int amount)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.DropFriendPackResponse>((await SendApiMessage(new Api.Request.DropFriendPackRequest() { ItemId = itemId, Amount = amount })).Message.Data);
        }
        public async Task<Api.Response.DropBombResponse> DropBomb(int itemId, int amount)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.DropBombResponse>((await SendApiMessage(new Api.Request.DropBombRequest() { ItemId = itemId, Amount = amount })).Message.Data);
        }
        public async Task<Api.Response.DropOilResponse> DropOil(int itemId, int amount)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.DropOilResponse>((await SendApiMessage(new Api.Request.DropOilRequest() { ItemId = itemId, Amount = amount })).Message.Data);
        }
        public async Task<Api.Response.DropPotionResponse> DropPotion(int itemId, int amount)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.DropPotionResponse>((await SendApiMessage(new Api.Request.DropPotionRequest() { ItemId = itemId, Amount = amount })).Message.Data);
        }
        public async Task<Api.Response.GatherHerbResponse> GatherHerb(long instanceId)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GatherHerbResponse>((await SendApiMessage(new Api.Request.GatherHerbRequest() { InstanceId = instanceId })).Message.Data);
        }
        public async Task<Api.Response.ClaimDailyContractResponse> ClaimDailyContract(int id)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.ClaimDailyContractResponse>((await SendApiMessage(new Api.Request.ClaimDailyContractRequest() { Param = id })).Message.Data);
        }
        public async Task<Api.Response.ThrowBombResponse> ThrowBomb(int id)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.ThrowBombResponse>((await SendApiMessage(new Api.Request.ThrowBombRequest() { Param = id })).Message.Data);
        }
        public async Task<Api.Response.PrepareToCombatResponse> PrepareToCombat(List<int> bombs, List<int> potions, int oil)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.PrepareToCombatResponse>((await SendApiMessage(new Api.Request.PrepareToCombatRequest() { Bombs = bombs, Potions = potions, Oil = oil })).Message.Data);
        }
        public async Task<Api.Response.CraftItemResponse> CraftItem(long instanceId, int recipeId, int itemType)
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.CraftItemResponse>((await SendApiMessage(new Api.Request.CraftItemRequest() { RecipeId = recipeId, BrewerInstanceId = instanceId, ItemType = itemType })).Message.Data);
        }
        public async Task<Api.Response.GetPlayerInfoResponse> GetPlayerInfo()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.GetPlayerInfoResponse>((await SendApiMessage(new Api.Request.GetPlayerInfoRequest())).Message.Data);
        }
        public async Task<Api.Response.AddDailyContractResponse> AddDailyContract()
        {
            return Api.Response.ApiResponse.Deserialize<Api.Response.AddDailyContractResponse>((await SendApiMessage(new Api.Request.AddDailyContractRequest())).Message.Data);
        }
        public async Task<Data.Json.StaticGameDataJson> GetStaticGameDataFromUrl()
        {
            return JsonConvert.DeserializeObject<Data.Json.StaticGameDataJson>(await _httpClient.GetStringAsync(await GetStaticDataUrl()));
        }

        public async Task<Data.Json.StaticGameDataJson> FetchStaticGameData()
        {
            return JsonConvert.DeserializeObject<Data.Json.StaticGameDataJson>(
                Encoding.UTF8.GetString(
                    GZipHelper.Decompress(
                        Api.Response.ApiResponse.Deserialize<StaticGameData.FetchStaticGameDataResponse>(
                            (await SendStaticGameDataMessage(new StaticGameData.FetchStaticGameDataRequest()))
                            .Data).
                            Data)
                    ));
        }
        public async Task<string> GetStaticDataUrl()
        {
            return Api.Response.ApiResponse.Deserialize<StaticGameData.GetStaticDataUrlResponse>(
                (await SendStaticGameDataMessage(new StaticGameData.GetStaticDataUrlRequest()))
                .Data)
                .StaticDataUrl;
        }
        private async Task<StaticGameData.StaticGameDataMessage> SendStaticGameDataMessage(StaticGameData.StaticGameDataRequest request)
        {
            var message = new StaticGameData.StaticGameDataMessage() { MethodId = request.GetMethodId(), Data = request.GetData() };
            var staticGameDataMessage = SocketMessageFactory.SeralizeStaticGameDataMessage(message);
            var socketMessage = new Message() { Type = Message.TypeId.STATIC_GAME_DATA, Data = staticGameDataMessage, Size = staticGameDataMessage.Length };

            var task = _callbacksStaticGameDataRequests.Create(request.GetMethodId());

            await _client.WriteBuffer(SocketMessageFactory.SeralizeMessageToSend(socketMessage));

            return (StaticGameData.StaticGameDataMessage)(await task);
        }
        private async Task<Api.ApiMessage> SendApiMessage(Api.Request.ApiRequest apiRequest)
        {
            var typeMessage = new Api.TypeMessage() { Id = GetRandomLong(), Data = apiRequest.Serialize(), MethodId = (int)apiRequest.GetMethodId() };
            var apiMessage = new Api.ApiMessage() { Type = Api.ApiMessage.TypeId.REQUEST, Message = typeMessage, Received = null };

            if (_receivedApiMessages.Count > 0)
            {
                apiMessage.Received = new long[_receivedApiMessages.Count];

                for (var i = 0; i < _receivedApiMessages.Count; i++)
                {
                    apiMessage.Received[i] = _receivedApiMessages.ElementAt(i);
                }

                _receivedApiMessages.Clear();
            }

            var apiMessageData = SocketMessageFactory.SeralizeApiMessage(apiMessage);
            var socketMessage = new Message() { Type = Message.TypeId.API, Data = apiMessageData, Size = apiMessageData.Length };

            var task = _callbacksApiRequests.Create(apiMessage.Message.Id);

            await _client.WriteBuffer(SocketMessageFactory.SeralizeMessageToSend(socketMessage));

            return (Api.ApiMessage)(await task);
        }
        private long GetRandomLong()
        {
            var buffer = new byte[8];
            _random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer);
        }
        private void HandleAuthMessage(AuthMessage message)
        {
            switch (message.MethodId)
            {
                case AuthMessage.Methods.AUTHENTICATE_RESPONSE:
                    OnAuthResponse(this, new AuthenticationResponse(message.Data[0]));
                    break;
                default:
                    Logger.Log(Logger.LogType.ERROR, $"HandleAuthMessage invalid methodId {message.MethodId}");
                    break;
            }
        }

        private async Task InvokeOnApiMessage(Api.Response.ApiResponse args)
        {
            Func<object, Api.Response.ApiResponse, Task> handler = OnApiMessage;

            if (handler == null)
            {
                return;
            }

            Delegate[] invocationList = handler.GetInvocationList();
            Task[] handlerTasks = new Task[invocationList.Length];

            for (int i = 0; i < invocationList.Length; i++)
            {
                handlerTasks[i] = ((Func<object, Api.Response.ApiResponse, Task>)invocationList[i])(this, args);
            }

            await Task.WhenAll(handlerTasks);
        }


        private void HandleApiMessage(Api.ApiMessage message)
        {
            try
            {
                if (message.Type == Api.ApiMessage.TypeId.RESPONSE)
                {
                    if (!_callbacksApiRequests.SetResult(message.Message.Id, message))
                    {
                        var apiResponseMessage = Api.Response.ApiResponse.Deserialize(message.Message.Data, message.Message.MethodId);
                        if (apiResponseMessage != null)
                        {
                            _ = Task.Run(() => InvokeOnApiMessage(apiResponseMessage));
                        }
                        else
                        {
                            Logger.Log(Logger.LogType.ERROR, $"_callbacksApiRequests coudn't find {message.Message.Id} {(Api.TypeMessage.Method)message.Message.MethodId}");
                        }
                    }
                    _receivedApiMessages.Add(message.Message.Id);
                }
                else
                    Logger.Log(Logger.LogType.ERROR, $"HandleApiMessage invalid message.Type {message.Type}");
            }
            catch (Exception es) { Logger.Log(Logger.LogType.ERROR, es.ToString()); }
        }

        private void HandleStaticGameDataMessage(StaticGameData.StaticGameDataMessage message)
        {
            _callbacksStaticGameDataRequests.SetResult(message.MethodId, message);
        }

        private void HandlePacket(BinaryReader reader)
        {
            var type = reader.ReadByte();
            var size = reader.ReadBigEndianInt();
            var buffer = reader.ReadBytes(size);

            switch (type)
            {
                case Message.TypeId.AUTHENTICATION:
                    HandleAuthMessage(SocketMessageFactory.DeserializeAuthMessage(buffer));
                    break;
                case Message.TypeId.API:
                    HandleApiMessage(SocketMessageFactory.DeserializeApiMessage(buffer));
                    break;
                case Message.TypeId.STATIC_GAME_DATA:
                    HandleStaticGameDataMessage(SocketMessageFactory.DeserializeStaticGameDataMessage(buffer));
                    break;
                default:
                    Logger.Log(Logger.LogType.ERROR, $"HandlePacket invalid type {type}");
                    break;
            }
        }

        public void OnRecived(byte[] buffer, int readedSize)
        {
            using (var ms = new MemoryStream(buffer, 0, readedSize))
            {
                using (var reader = new BinaryReader(ms))
                {
                    HandlePacket(reader);

                    if (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        Logger.Log(Logger.LogType.ERROR, $"OnRecived failed to read packet it seems like two packets in one buffer shoudn't not happend");
                    }
                }
            }
        }
    }
}
