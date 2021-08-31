using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Logging;
using WitcherMonsterBotter.Core.Packets.Data;
using WitcherMonsterBotter.Core.Utility;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public abstract class ApiResponse
    {
        private static object ReadObject(BinaryReader reader, Type type)
        {
            try
            {
                if (type == typeof(int))
                {
                    return reader.ReadBigEndianInt();
                }
                if (type == typeof(float))
                {
                    return reader.ReadBigEndianFloat();
                }
                else if (type == typeof(bool))
                {
                    return reader.ReadBoolean();
                }
                else if (type == typeof(byte))
                {
                    return reader.ReadByte();
                }
                else if (type == typeof(long))
                {
                    return reader.ReadBigEndianLong();
                }
                else if (type == typeof(ulong))
                {
                    return reader.ReadBigEndianUlong();
                }
                else if (type == typeof(Dictionary<int, ApiResponse>))
                {
                    var methodsResponse = new Dictionary<int, ApiResponse>();

                    var size = reader.ReadBigEndianInt();
                    if (size > 0)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            var methodId = reader.ReadBigEndianInt();

                            if (MethodsTypes.TryGetValue(methodId, out var methodType))
                            {
                                var methodObject = Activator.CreateInstance(methodType);

                                Deserialize(reader, methodObject);

                                methodsResponse.Add(methodId, (ApiResponse)methodObject);
                            }
                            else
                            {
                                Logger.Log(Logger.LogType.ERROR, $"Unsupported methodId type {(TypeMessage.Method)methodId}");
                            }
                        }
                    }

                    return methodsResponse;
                }
                else if ((type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(HashSet<>))))
                {
                    Type itemType = type.GetGenericArguments()[0];

                    var size = reader.ReadBigEndianInt();
                    var values = Array.CreateInstance(itemType, size);

                    if (size > 0)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            values.SetValue(ReadObject(reader, itemType), i);
                        }
                    }

                    return Activator.CreateInstance(type, new object[] { values });
                }
                else if ((type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Dictionary<,>))))
                {
                    Type keyType = type.GetGenericArguments()[0];
                    Type valueType = type.GetGenericArguments()[1];

                    var dict = (IDictionary)Activator.CreateInstance(type);
                    var size = reader.ReadBigEndianInt();
                    if (size > 0)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            dict[ReadObject(reader, keyType)] = ReadObject(reader, valueType);
                        }
                    }

                    return dict;
                }
                else if (type == typeof(string))
                {
                    var size = reader.ReadBigEndianInt();
                    var buffer = reader.ReadBytes(size);

                    return Encoding.UTF8.GetString(buffer);
                }
                else if (type.IsArray)
                {
                    var size = reader.ReadBigEndianInt();
                    var elementType = type.GetElementType();
                    var array = Array.CreateInstance(elementType, size);

                    if (size > 0)
                    {
                        for (var i = 0; i < size; i++)
                            array.SetValue(ReadObject(reader, elementType), i);
                    }

                    return array;
                }
                else if ((type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>))))
                {
                    Type itemType = type.GetGenericArguments()[0];

                    var list = (IList)Activator.CreateInstance(type);

                    var size = reader.ReadBigEndianInt();

                    if (size > 0)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            list.Add(ReadObject(reader, itemType));
                        }
                    }

                    return list;
                }

                var obj = Activator.CreateInstance(type);

                Deserialize(reader, obj);

                return obj;
            }
            catch (Exception es)
            {
                Logger.Log(Logger.LogType.ERROR, es.ToString());
                return null;
            }
        }

        private static void Deserialize(BinaryReader reader, object obj)
        {
            var type = obj.GetType();

            bool checkSuccess = type.GetCustomAttributes(true).FirstOrDefault(x => (x as SerializableOnlySuccessAttribute) != null) != null;

            foreach (var prop in type.GetProperties())
            {
                if (prop.GetCustomAttributes(true).FirstOrDefault(x => (x as SerializablePropertyAttribute) != null) == null)
                    continue;

                var propertyValue = ReadObject(reader, prop.PropertyType);
                prop.SetValue(obj, propertyValue);

                if (checkSuccess)
                {
                    if (prop.PropertyType == typeof(bool) && (bool)propertyValue == false)
                        return;

                    checkSuccess = false;
                }
            }
        }

        public static ApiResponse Deserialize(byte[] buffer, int methodId)
        {
            if (MethodsTypes.TryGetValue(methodId, out var type))
            {
                using (var ms = new MemoryStream(buffer))
                {
                    using (var reader = new BinaryReader(ms))
                    {
                        var obj = Activator.CreateInstance(type);

                        Deserialize(reader, obj);

                        if (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            Logger.Log(Logger.LogType.ERROR, $"Deserialize failed check classes structures and fields {obj.GetType()} Diff: {reader.BaseStream.Length - reader.BaseStream.Position}");
                        }

                        return (ApiResponse)obj;
                    }
                }
            }

            return null;
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(ms))
                {
                    var obj = (T)Activator.CreateInstance(typeof(T));

                    Deserialize(reader, obj);

                    if (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        Logger.Log(Logger.LogType.ERROR, $"Deserialize failed check classes structures and fields {obj.GetType()} Diff: {reader.BaseStream.Length - reader.BaseStream.Position}");
                    }

                    return obj;
                }
            }
        }

        public abstract TypeMessage.Method GetMethodId();

        private static Dictionary<int, Type> _methodsTypes = null;

        public static Dictionary<int, Type> MethodsTypes
        {
            get
            {
                if (_methodsTypes == null)
                {
                    _methodsTypes = new Dictionary<int, Type>();

                    var type = typeof(ApiResponse);
                    var responsePackets = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

                    foreach (var response in responsePackets)
                    {
                        var instance = Activator.CreateInstance(response);
                        var methodId = ((ApiResponse)instance).GetMethodId();
                        _methodsTypes.Add((int)methodId, response);
                    }
                }

                return _methodsTypes;
            }
        }
    }
}
