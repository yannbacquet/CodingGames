
using IAGames;
using System;
using Xunit;


namespace XUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            const int height = 8;
            string[] lines = new string[height] { "3.4.6.2.", ".1......", "..2.5..2", "1.......","..1.....",
                                                  ".3..52.3",".2.17..4",".4..51.2" };//-

            string resR, resD;

            NoSpoon2.Calculs(height, lines);

            NoSpoon2.Cell cell = NoSpoon2.cells[2];
            PrintStrings(cell, out resR, out resD);
            Assert.Equal("4 0 6 0 2", resR);
            Assert.Equal("4 0 4 2 2", resD);

            cell = NoSpoon2.cells[5];
            PrintStrings(cell, out resR, out resD);
            Assert.Equal("2 2 4 2 1", resR);
            Assert.Equal("2 2 2 4 1", resD);



        }

        private static void PrintStrings(NoSpoon2.Cell cell, out string resR, out string resD)
        {
            resR = cell.X + " " + cell.Y + " " + cell.RightCell.X + " " + cell.RightCell.Y + " " + cell.PondsRights;
            resD = cell.X + " " + cell.Y + " " + cell.DownCell.X + " " + cell.DownCell.Y + " " + cell.PondsDown;
        }
    }
}
