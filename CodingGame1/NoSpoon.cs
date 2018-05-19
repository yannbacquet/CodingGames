using System;
using System.Collections.Generic;
using System.Text;

namespace IAGames
{
    class NoSpoon
    {
        static void Main2(string[] args)
        {
            int width = 2; // the number of cells on the X axis
            const int height = 2; // the number of cells on the Y axis

            Cell[,] cells = new Cell[width,height];

            string[] lines = new string[height] { "00" , "0." };//-

            for (int y = 0; y < height; y++)
            {
                //-string line = Console.ReadLine();
                string line = lines[y];

                int x = 0;
                foreach (char ch in line)
                {
                    cells[x,y] =  new Cell(x, y, ch == '0');

                    x++;
                }
            }
            //------ligne a ligne on cherche voisins de droite
            for (int y = 0; y < height; y++)
            {
                int prevX = -1;
                for (int x = 0; x < width; x++)
                {
                    if(cells[x, y].IsNode)
                    {
                       if(prevX!=-1) cells[prevX, y].XRight = x;
                       prevX = x;
                    }
                }
                
            }



            //------colonne a colonne on cherche voisins du bas
            for (int x = 0; x < width; x++) 
            {
                int prevY = -1;
                for (int y = 0; y < height; y++)
                {
                    if (cells[x, y].IsNode)
                    {
                        if (prevY != -1) cells[x,prevY].YDown = y;
                        prevY = y;
                    }
                }
            }

            //Display
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (cells[x, y].IsNode)
                    {
                        Console.WriteLine(cells[x, y].CellInfos());
                    }
                }
            }



            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // Three coordinates: a node, its right neighbor, its bottom neighbor

            Console.ReadKey();//-
        }

        internal class Cell
        {
            int _coordX;
            int _coordY;
            bool _isNode;

            int _xRight;
            int _yDown;

            public Cell(int x, int y, bool isNode)
            {
                _coordX = x;
                _coordY = y;
                _isNode = isNode;
                _xRight = -1;
                _yDown = -1;

            }

            public bool IsNode { get => _isNode; set => _isNode = value; }
            public int XRight { get => _xRight; set => _xRight = value; }
            public int YDown { get => _yDown; set => _yDown = value; }

            public String CellInfos()
            {
                String nodeInfos = _coordX + " " + _coordY + " ";
                nodeInfos += (_xRight!=-1) ? _xRight + " " + _coordY + " " : "-1 -1 ";
                nodeInfos += (_yDown != -1) ? _coordX + " " + _yDown + " " : "-1 -1 ";

                return nodeInfos;
            }

        }
    }

   
}
