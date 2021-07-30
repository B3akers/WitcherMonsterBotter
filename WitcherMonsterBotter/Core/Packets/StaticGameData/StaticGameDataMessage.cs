using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.StaticGameData
{
    class StaticGameDataMessage
    {
		public struct TypeId
		{
			public const int FETCH = 1;
			public const int GET_DATA_URL = 2;
		}

		public int MethodId;
		public byte[] Data;
	}
}
