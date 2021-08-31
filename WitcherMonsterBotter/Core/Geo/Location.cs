using Google.Common.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Geo
{
    class Location
    {
        public const int CELL_LEVEL = 14;
        public static List<ulong> GetPlayerCells(double lat, double lng)
        {
            List<ulong> cells = new();

            var cellId = S2CellId.FromLatLng(S2LatLng.FromDegrees(lat, lng)).ParentForLevel(CELL_LEVEL);

            cells.Add(cellId.Id);

            List<S2CellId> cellsIds = new();

            cellId.GetAllNeighbors(CELL_LEVEL, cellsIds);

            foreach (var cell in cellsIds)
                cells.Add(cell.Id);

            return cells;
        }
    }
}
