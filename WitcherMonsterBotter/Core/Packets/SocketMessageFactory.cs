using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;
using WitcherMonsterBotter.Core.Utility;

namespace WitcherMonsterBotter.Core.Packets
{
    class SocketMessageFactory
    {
        static readonly int _secretValue = 1244152720;

        public static long GetVersionAsLong(string verion)
        {
            var splited = verion.Split('.');

            if (splited.Length == 3)
                if (int.TryParse(splited[0], out int first))
                    if (int.TryParse(splited[1], out int second))
                        if (int.TryParse(splited[2], out int third))
                            return (long)(second | (first << 16)) << 32 | (long)(uint)(third);

            return 0;
        }

        public static StaticGameData.StaticGameDataMessage DeserializeStaticGameDataMessage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    var staticGameDataMessage = new StaticGameData.StaticGameDataMessage();

                    staticGameDataMessage.MethodId = reader.ReadBigEndianInt();
                    staticGameDataMessage.Data = reader.ReadBytes(data.Length - 0x4);

                    return staticGameDataMessage;
                }
            }
        }

        public static ApiMessage DeserializeApiMessage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    reader.ReadByte(); //API_VERSION

                    var apiMessage = new ApiMessage();
                    apiMessage.Type = reader.ReadByte();

                    var recivedSize = reader.ReadBigEndianInt();
                    if (recivedSize > 0)
                    {
                        apiMessage.Received = new long[recivedSize];

                        for (var i = 0; i < recivedSize; i++)
                            apiMessage.Received[i] = reader.ReadBigEndianLong();
                    }
                    else
                        apiMessage.Received = null;

                    //TypeMessage
                    apiMessage.Message = new();

                    apiMessage.Message.Id = reader.ReadBigEndianLong();
                    apiMessage.Message.MethodId = reader.ReadBigEndianInt();
                    apiMessage.Message.Data = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));

                    return apiMessage;
                }
            }
        }

        public static AuthMessage DeserializeAuthMessage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    var methodId = reader.ReadBigEndianInt();

                    return new AuthMessage { MethodId = methodId, Data = reader.ReadBytes(data.Length - 4) };
                }
            }
        }

        public static byte[] SeralizeStaticGameDataMessage(StaticGameData.StaticGameDataMessage msg)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.WriteBigEndian(msg.MethodId);

                    if (msg.Data != null)
                        writer.Write(msg.Data);

                    return ms.ToArray();
                }
            }
        }

        public static byte[] SeralizeApiMessage(ApiMessage msg)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.Write((byte)1); //API_VERSION
                    writer.Write(msg.Type);

                    if (msg.Received != null)
                    {
                        writer.WriteBigEndian(msg.Received.Length);
                        for (var i = 0; i < msg.Received.Length; i++)
                            writer.WriteBigEndian(msg.Received[i]);
                    }
                    else
                    {
                        writer.WriteBigEndian(0);
                    }

                    //TypeMessage

                    writer.WriteBigEndian(msg.Message.Id);
                    writer.WriteBigEndian(msg.Message.MethodId);
                    writer.Write(msg.Message.Data);

                    return ms.ToArray();
                }
            }
        }

        public static byte[] SeralizeMessageToSend(Message msg)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.Write(_secretValue);

                    writer.Write(msg.Type);
                    writer.WriteBigEndian(msg.Size);
                    writer.Write(msg.Data);

                    return ms.ToArray();
                }
            }
        }

        public static Message BuildAuthenticationRequest(int apiVersion, long clientVersion, string deviceId, string accountId)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.WriteBigEndian(1);
                    writer.WriteBigEndian(apiVersion);
                    writer.WriteBigEndian(clientVersion);

                    var deviceIdBytes = Encoding.ASCII.GetBytes(deviceId);
                    var accountIdBytes = Encoding.ASCII.GetBytes(accountId);

                    writer.WriteBigEndian(deviceIdBytes.Length);
                    writer.Write(deviceIdBytes);

                    writer.WriteBigEndian(accountIdBytes.Length);
                    writer.Write(accountIdBytes);

                    var bufferArray = ms.ToArray();

                    return new Message() { Type = Message.TypeId.AUTHENTICATION, Data = bufferArray, Size = bufferArray.Length };
                }
            }
        }
    }
}
