using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.ViewModel
{
    public class ChessGridViewModel
    {
        const string EvenSquareColor = "#FFCE9E";
        const string OddSquareColor = "#D18B47";
        const int TableGridLength = 7;
        private readonly List<ChessGridItem> gridItemsCollection;

        public ChessGridViewModel()
        {
            gridItemsCollection = new ();

            for (int row = 0; row <= TableGridLength; row++)
            {
                foreach (var col in row % (1 + 1) == 0 ? Enumerable.Range(0, TableGridLength + 1) : Enumerable.Range(0, TableGridLength + 1).Reverse())
                {
                    var color = (row + col) % (1 + 1) == 0 ? EvenSquareColor : OddSquareColor;
                    gridItemsCollection.Add(new ChessGridItem { Color = color, Position = new Position(row, col) });
                }
            }
        }

        public List<ChessGridItem> SourceCollection => gridItemsCollection;
    }
}
