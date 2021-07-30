using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Utility;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public abstract class ApiRequest
    {
        private static void SerializeProperty(BinaryWriter writer, Type type, object property)
        {
            if (type == typeof(int))
            {
                writer.WriteBigEndian((int)property);
            }
            else if (type == typeof(long))
            {
                writer.WriteBigEndian((long)property);
            }
            else if (type == typeof(ulong))
            {
                writer.WriteBigEndian((ulong)property);
            }
            else if (type == typeof(bool))
            {
                writer.Write((bool)property);
            }
            else if (type == typeof(byte))
            {
                writer.Write((byte)property);
            }
            else if (type == typeof(string))
            {
                var str = (string)property;
                writer.WriteBigEndian(str.Length);
                writer.Write(Encoding.UTF8.GetBytes(str));
            }
            else if (type.IsArray)
            {
                var array = (Array)property;
                if (array != null && array.Length > 0)
                {
                    writer.WriteBigEndian(array.Length);

                    var elementType = type.GetElementType();

                    for (var i = 0; i < array.Length; i++)
                    {
                        SerializeProperty(writer, elementType, array.GetValue(i));
                    }
                }
                else
                {
                    writer.WriteBigEndian(0);
                }
            }
            else if ((type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>))))
            {
                var list = (IList)property;
                if (list != null && list.Count > 0)
                {
                    writer.WriteBigEndian(list.Count);

                    Type itemType = type.GetGenericArguments()[0];

                    foreach (var value in list)
                    {
                        SerializeProperty(writer, itemType, value);
                    }
                }
                else
                {
                    writer.WriteBigEndian(0);
                }
            }
            else
            {
                SerializeClass(writer, property);
            }
        }

        private static void SerializeClass(BinaryWriter writer, object property)
        {
            foreach (var prop in property.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).FirstOrDefault(x => (x as SerializablePropertyAttribute) != null) == null)
                    continue;

                SerializeProperty(writer, prop.PropertyType, prop.GetValue(property));
            }
        }

        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    SerializeClass(writer, this);

                    return ms.ToArray();
                }
            }
        }

        public abstract TypeMessage.Method GetMethodId();
    }
}
