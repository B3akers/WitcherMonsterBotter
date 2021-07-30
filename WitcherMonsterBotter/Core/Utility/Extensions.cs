using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Utility
{
    public static class Extensions
    {
        public static void WriteBigEndian(this BinaryWriter writer, byte[] buffer)
        {
            Array.Reverse(buffer);
            writer.Write(buffer);
        }

        public static void WriteBigEndian(this BinaryWriter writer, int value)
        {
            writer.WriteBigEndian(BitConverter.GetBytes(value));
        }

        public static void WriteBigEndian(this BinaryWriter writer, long value)
        {
            writer.WriteBigEndian(BitConverter.GetBytes(value));
        }

        public static void WriteBigEndian(this BinaryWriter writer, ulong value)
        {
            writer.WriteBigEndian(BitConverter.GetBytes(value));
        }

        public static int ReadBigEndianInt(this BinaryReader reader)
        {
            var buffer = reader.ReadBytes(4);

            Array.Reverse(buffer);

            return BitConverter.ToInt32(buffer);
        }

        public static float ReadBigEndianFloat(this BinaryReader reader)
        {
            var buffer = reader.ReadBytes(4);

            Array.Reverse(buffer);

            return BitConverter.ToSingle(buffer);
        }

        public static long ReadBigEndianLong(this BinaryReader reader)
        {
            var buffer = reader.ReadBytes(8);

            Array.Reverse(buffer);

            return BitConverter.ToInt64(buffer);
        }

        public static ulong ReadBigEndianUlong(this BinaryReader reader)
        {
            var buffer = reader.ReadBytes(8);

            Array.Reverse(buffer);

            return BitConverter.ToUInt64(buffer);
        }
    }
}
