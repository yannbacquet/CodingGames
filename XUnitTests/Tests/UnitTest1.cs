
using IAGames;
using System;
using Xunit;


namespace XUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test22()
        {
            const int height = 2;
            string[] lines = new string[height] { "21", "21" };
            NoSpoon2.Calculs(height, lines);


            NoSpoon2.Cell cell = NoSpoon2.cells[0];
            PrintStrings(cell, "0 0 1 0 1", "0 0 0 1 1");

            cell = NoSpoon2.cells[2];
            PrintStrings(cell, "0 1 1 1 1", null);

        }


        [Fact]
        public void Test1()
        {
            const int height = 8;
            string[] lines = new string[height] { "3.4.6.2.", ".1......", "..2.5..2", "1.......","..1.....",
                                                  ".3..52.3",".2.17..4",".4..51.2" };

            
            NoSpoon2.Calculs(height, lines);


            NoSpoon2.Cell cell = NoSpoon2.cells[2];
            PrintStrings(cell, "4 0 6 0 2", "4 0 4 2 2");
           
            cell = NoSpoon2.cells[5];
            PrintStrings(cell, "2 2 4 2 1", "2 2 2 4 1");

            cell = NoSpoon2.cells[16];
            PrintStrings(cell, "4 6 7 6 2", "4 6 4 7 2");

            cell = NoSpoon2.cells[19];
            PrintStrings(cell, "4 7 5 7 1", null);

        }

        [Fact]
        public void Test2()
        {
            const int height =5;
            string[] lines = new string[height] { "2..2.1.", ".3..5.3", ".2.1...", "2...2..", ".1....2" };


            NoSpoon2.Calculs(height, lines);


            NoSpoon2.Cell cell = NoSpoon2.cells[3];
            PrintStrings(cell, "1 1 4 1 2", "1 1 1 2 1");

            cell = NoSpoon2.cells[5];
            PrintStrings(cell, null, "6 1 6 4 1");

            cell = NoSpoon2.cells[8];
            PrintStrings(cell, "0 3 4 3 1", null);

            cell = NoSpoon2.cells[10];
            PrintStrings(cell, "1 4 6 4 1", null);

        }

        private static void PrintStrings(NoSpoon2.Cell cell,  string attR,  string attD)
        {
            if (attR != null) {
                string resR = cell.X + " " + cell.Y + " " + cell.RightCell.X + " " + cell.RightCell.Y + " " + cell.PondsRights;
                Assert.Equal(attR, resR);
            }
            if (attD != null) {
                string resD = cell.X + " " + cell.Y + " " + cell.DownCell.X + " " + cell.DownCell.Y + " " + cell.PondsDown;
                Assert.Equal(attD, resD);
            }

        }
    }
}
