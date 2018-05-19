using System;
using System.Collections.Generic;
using System.Text;

namespace IAGames

{
    public class NoSpoon2
    {
        static int Id = 0;
        public static List<Cell> cells = new List<Cell>();
        static List<Tuple<int, int>> interCells = new List<Tuple<int, int>>();
        static void Main(string[] args)
        {
            //- int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
            //- int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
            int width = 8; // the number of cells on the X axis
            const int height = 8; // the number of cells on the Y axis


            // string[] lines = new string[height] { "25.1", "47.4", "..1.", "3344" };//-
            //  string[] lines = new string[height] { "14.3", "....", ".4.4" };//-
            // string[] lines = new string[height] { "2..2.1.", ".3..5.3", ".2.1...", "2...2..",".1....2" };

            string[] lines = new string[height] { "3.4.6.2.", ".1......", "..2.5..2", "1.......","..1.....",
                                                  ".3..52.3",".2.17..4",".4..51.2" };//-


            //-  Un graphe connexe à  n sommets possède au moins  n - 1 arêtes.

            Calculs(height, lines);

            //PRINT
            PrintRes();

            Console.ReadKey();
        }

        private static void PrintRes()
        {
            foreach (Cell cell in cells)
            {
                if (cell.RightCell != null && cell.PondsRights > 0)
                    Console.WriteLine(cell.X + " " + cell.Y + " " + cell.RightCell.X + " " + cell.RightCell.Y + " " + cell.PondsRights);
                if (cell.DownCell != null && cell.PondsDown > 0)
                    Console.WriteLine(cell.X + " " + cell.Y + " " + cell.DownCell.X + " " + cell.DownCell.Y + " " + cell.PondsDown);

            }
        }

        public static void Calculs(int height, string[] lines)
        {
            for (int y = 0; y < height; y++)
            {
                //- string line = Console.ReadLine(); // width characters, each either a number or a '.'
                string line = lines[y];

                int x = 0;
                foreach (char v in line)
                {
                    if (v != '.')
                        cells.Add(new Cell(x, y, (int)Char.GetNumericValue(v)));

                    x++;
                }
            }

            //remplissage right et down
            foreach (Cell curCell in cells)
            {

                Cell rightCell = cells.Find(c => c.Y == curCell.Y && c.X > curCell.X);
                curCell.RightCell = rightCell;

                Cell downCell = cells.Find(c => c.X == curCell.X && c.Y > curCell.Y);
                curCell.DownCell = downCell;

            }


            while (Id < cells.Count)
            {

                //NoeudArbre noeud = new NoeudArbre(curCell); arbre.Push(noeud);
                //is NoeudValid
                Cell mainCell = cells[Id];
                int valRest = mainCell.ValRest;//val - pondsExt

                //cas particuliers
                if (valRest < 0 || valRest > 4)
                {
                    Back();
                }
                else if (valRest == 0)
                {
                    mainCell.IsLastSolution = true; //pas d'autres possibilites
                }
                else //valRest!=0
                {
                    //1) Ni RIGHT Ni DOWN : verifier que valrest= new val
                    if (mainCell.nbVoisins() == 0)
                    {
                        Back();
                    }

                    //2) que RIGHT ou  DOWN : 1 seule possibilité 

                    if (mainCell.nbVoisins() == 1)
                    {
                        if (valRest <= 2)
                        {
                            bool isAllow = true;
                            mainCell.IsLastSolution = true; //pas d'autres possibilites
                            if (mainCell.RightCell != null)
                                isAllow = PondsRightDown(mainCell, valRest, 0);
                            if (mainCell.DownCell != null)
                                isAllow = PondsRightDown(mainCell, 0, valRest);

                            if (!isAllow)
                            {
                                Id++;
                                Back();
                            }
                        }
                        else //probleme pop
                            Back();
                    }

                    // 3) Right ET  DOWN : --- cas General ---
                    if (mainCell.nbVoisins() == 2)
                    {
                        if (valRest == 4) //2 right 2 down
                        {
                            mainCell.IsLastSolution = true; //pas d'autres possibilites
                            bool isAllow = PondsRightDown(mainCell, 2, 2);
                            if (!isAllow)
                            {
                                Id++;
                                Back();
                            }

                        }
                        else if (valRest < 4 && mainCell.PondsRights < 2 && mainCell.PondsRights < valRest)
                        {
                            int right = mainCell.PondsRights + 1;  //a chaque passage on fait 0,v / 1,v-1 / 2,v-2
                            if (valRest - right > 2) //cas 0,3 pas possible
                                right++;

                            int down = valRest - right;

                            PondsRightDown(mainCell, right, down);
                        }
                        else
                        { //toutes les cas ont ete faits , on back
                            mainCell.PondsRights = -1;
                            mainCell.PondsDown = -1;
                            mainCell.IsLastSolution = false;
                            Back();
                        }
                    }
                }
                Id++;

            } //END WHILE
        }

        private static void Back()
        {
            Console.Error.WriteLine("-BACK-");

            Id--;
            while (Id >= 0 && cells[Id].IsLastSolution)
            {
             
                cells[Id].PondsRights = -1;
                cells[Id].PondsDown = -1;
                cells[Id].IsLastSolution = false;

               Id--;
            }
                        
            reCreateAllInterAndExt();
            Id--;
        }

        private static void reCreateAllInterAndExt()
        {
            interCells.Clear();
            //clear des pondsExt
            foreach (Cell cell in cells)
                cell.PondsExt = 0;

            for (int i = 0; i <Id; i++)
            {
                var cell = cells[i];
                //cell de droite
                if (cell.PondsRights>0)
                {
                    cell.RightCell.PondsExt += cell.PondsRights;
                    for (int x = cell.X + 1; x < cell.RightCell.X; x++)
                        interCells.Add(new Tuple<int, int>(x, cell.Y));
                }

                //cell du bas
                if (cell.PondsDown>0)
                {
                    cell.DownCell.PondsExt += cell.PondsDown;
                    for (int y = cell.Y + 1; y < cell.DownCell.Y; y++)
                        interCells.Add(new Tuple<int, int>(cell.X, y));
                }
            }
        }

        private static bool PondsRightDown(Cell cell,int right, int down)
        {

            bool isAllow = true;
            //check intermediaires (y a t-il croisement)
            if (cell.RightCell != null && right != 0)
            {
                for (int x = cell.X + 1; x < cell.RightCell.X; x++) {
                    if (interCells.Exists(c => c.Item1 == x && c.Item2 == cell.Y))
                        isAllow = false;
                }
            }
            if (cell.DownCell != null && down != 0)
            {
                for (int y = cell.Y + 1; y < cell.DownCell.Y; y++)
                    if (interCells.Exists(c => c.Item1 == cell.X && c.Item2 == y))
                        isAllow = false;
            }

            cell.PondsRights = right;
            cell.PondsDown = down;

            Console.Error.WriteLine("PRD("+cell.X+","+cell.Y+ ")  v r/d: " + cell.Val + "  "+ right + "/" + down + "   isAllow:" + isAllow);

            if (isAllow)
            {

                //cell de droite
                if (cell.RightCell != null && right != 0)
                {
                    cell.RightCell.PondsExt += right;

                    for (int x = cell.X + 1; x < cell.RightCell.X; x++)
                        interCells.Add(new Tuple<int, int>(x, cell.Y));
                }

                //cell du bas
                if (cell.DownCell != null && down != 0)
                {
                    cell.DownCell.PondsExt +=  down;

                    for (int y = cell.Y + 1; y < cell.DownCell.Y; y++)
                        interCells.Add(new Tuple<int, int>(cell.X, y));
                }

            }
            else { //back
                Id--;
            }

            return isAllow;
        }



      

        public class Cell
        {
            int _x;
            int _y;

            int _val;
            int _pondsExt = 0; //ponds venant d'un lien precedents, a decompter de val
                                 
            Cell _rightCell;
            Cell _downCell;

            //------
            int _pondsRights = -1;
            int _pondsDown = -1;

            bool _isLastSolution = false;


            //int valRest
            //bool rightok, downok

            public Cell(int x, int y, int val)//, bool isNode)
            {
                _x = x;
                _y = y;
                _val = val;
                //_isNode = isNode;

              


            }

            //val - ponds exterieurs
            public int ValRest => _val - _pondsExt;

            public int nbVoisins() {
                if (RightCell == null && DownCell == null) return 0;
                if (RightCell != null && DownCell == null) return 1;
                if (RightCell == null && DownCell != null) return 1;
                if (RightCell != null && DownCell != null) return 2;

                return -1;
            }

            public int Val { get => _val; set => _val = value; }
            public int PondsExt { get => _pondsExt; set => _pondsExt = value; }
            public int PondsRights { get => _pondsRights; set => _pondsRights = value; }
            public int PondsDown { get => _pondsDown; set => _pondsDown = value; }
            public bool IsLastSolution { get => _isLastSolution; set => _isLastSolution = value; }
            public int X { get => _x; set => _x = value; }
            public int Y { get => _y; set => _y = value; }
            public Cell RightCell { get => _rightCell; set => _rightCell = value; }
            public Cell DownCell { get => _downCell; set => _downCell = value; }

            
        }

        internal class NoeudArbreOld
        {
            Cell _mainCell;

            int _pondsRights = -1;
            int _pondsDown = -1;

            bool _isLastSolution = false;
            int _nbChilds = -1;
            int _curChild = -1;

            //NoeudArbre[] childs = new NoeudArbre[3];

            public NoeudArbreOld(Cell mainCell)
            {
                _mainCell = mainCell;

            }

            public int PondsRights { get => _pondsRights; set => _pondsRights = value; }
            public int PondsDown { get => _pondsDown; set => _pondsDown = value; }
            public bool IsLastSolution { get => _isLastSolution; set => _isLastSolution = value; }
            public int NbChilds { get => _nbChilds; set => _nbChilds = value; }
            public int CurChild { get => _curChild; set => _curChild = value; }
            internal Cell MainCell { get => _mainCell; set => _mainCell = value; }


        }
    }
}

