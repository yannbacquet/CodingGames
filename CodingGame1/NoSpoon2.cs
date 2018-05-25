using System;
using System.Collections.Generic;
using System.Text;

namespace IAGames

{
    public class NoSpoon2
    {
        public static int Id = 0;
        public static List<Cell> cells;
       // static List<Tuple<int, int>> interCells;
        static HashSet<Cell> linkedCells = new HashSet<Cell>();

        static void Main(string[] args)
        {
            //- int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
            //- int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
            // int width = 8; // the number of cells on the X axis
            const int height =5; // the number of cells on the Y axis

            // string[] lines = new string[height] { "25.1", "47.4", "..1.", "3344" };//-
            //  string[] lines = new string[height] { "14.3", "....", ".4.4" };//-
            // string[] lines = new string[height] { "2..2.1.", ".3..5.3", ".2.1...", "2...2..",".1....2" };

            // string[] lines = new string[height] { "3.4.6.2.", ".1......", "..2.5..2", "1.......","..1.....",
            //                                   ".3..52.3",".2.17..4",".4..51.2" };//-
            //  string[] lines = new string[height] { "21", "21" };//-
            // string[] lines = { "3..41", ".22..", "..1.1", ".2.2.", "3...3" };

            // string[] lines = {"22221","2....","2....","2....","2....","22321",".....",".....","22321","2....",
            // "2....","2.131","2..2.","2222."};

            string[] lines =
            {
                ".12..",
                ".2421",
                "24442",
                "1242.",
                "..21."
            };

           /* string[] lines = {"3..2.2..1....3........4",
            ".2..1....2.6.........2.",
            "..3..6....3............",
            ".......2........1..3.3.",
            "..1.............3..3...",
            ".......3..3............",
            ".3...8.....8.........3.",
            "6.5.1...........1..3...",
            "............2..6.31..2.",
            "..4..4.................",
            "5..........7...7...3.3.",
            ".2..3..3..3............",
            "......2..2...1.6...3...",
            "....2..................",
            ".4....5...3............",
            ".................2.3...",
            ".......3.3..2.44....1..",
            "3...1.3.2.3............",
            ".2.....3...6.........5.",
            "................1......",
            ".1.......3.6.2...2...4.",
            "5...............3.....3",
            "4...................4.2"};*/

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Calculs(height, lines);

            //PRINT
            PrintRes();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.Error.WriteLine("time" + elapsedMs);

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
            Id = 0;
            cells = new List<Cell>();
          
            for (int y = 0; y < height; y++)
            {
                string line;
                if (lines==null) //codingGame
                     line = Console.ReadLine(); // width characters, each either a number or a '.'
                else
                    line = lines[y];

                Console.Error.WriteLine("\"" + line + "\"" + ",");
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


            //--- BOUCLE PRINCIPALE
            while (Id < cells.Count)
            {
                Cell mainCell = cells[Id];
                int valRest = mainCell.ValRest;//val - pondsExt

                mainCell.IsLastSolution = true;

                //cas particuliers
                if (valRest < 0 || valRest > 4)
                {
                    Back();
                }
               // else if (valRest == 0){//-pas d'autres possibilites}
                else if (valRest!=0)
                {
                    //1) Ni RIGHT Ni DOWN 
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
                          
                            if (mainCell.RightCell != null)
                                isAllow = PondsRightDown(mainCell, valRest, 0);
                            if (mainCell.DownCell != null)
                                isAllow = PondsRightDown(mainCell, 0, valRest);

                            if (!isAllow)
                            {
                                Back();
                            }
                        }
                        else //probleme 
                            Back();
                    }

                    // 3) Right ET  DOWN : --- cas General ---
                    if (mainCell.nbVoisins() == 2)
                    {
                        if (valRest == 4) //2 right 2 down
                        {
                            bool isAllow = PondsRightDown(mainCell, 2, 2);
                            if (!isAllow)
                            {
                                Back();
                            }

                        }
                         else if (valRest < 4 && mainCell.PondsRights < 2 && mainCell.PondsRights < valRest)
                         {
                             int right = mainCell.PondsRights + 1;  //a chaque passage on fait 0,v / 1,v-1 / 2,v-2
                             if (valRest - right > 2) //cas 0,3 pas possible
                                 right++;

                             mainCell.IsLastSolution = false; //dans tous les autres cas c'est true
                             int down = valRest - right;
                             bool isAllow =  PondsRightDown(mainCell, right, down);
                        
                            if (!isAllow) //on ne back pas mais on reste sur le meme Id
                            {
                                Id--;
                            }
                        }
                        else
                        { //toutes les cas ont ete faits , on back
                            Back();
                        }
                    }
                }
               

                //FIN CHECK VERIF GRAPHE CONNEXE
                if (Id == cells.Count-1)
                {
                    Console.Error.WriteLine("CHECK:");
                   
                    //creation liste principale chainee
                    linkedCells.Clear();
                    LinkCell(linkedCells,cells[0]);

                    if(linkedCells.Count!= cells.Count) {
                        Console.Error.WriteLine("GRAPHE NON CONNEXE:");
                        Back();
                        
                    } //end If

                }//END check connexe

                Id++;
                

            } //END WHILE
        }

        //methode recursive qui trouve la liste des cellules liees entre elles
        private static void LinkCell(HashSet<Cell> setCells,  Cell cell)
        {
            //verifier qu'elle nexiste pas deja sinon boucle infinie
            if (!setCells.Contains(cell))
                setCells.Add(cell);
            else
                 return;
           

            if (cell.RightCell != null && cell.PondsRights>0) { 
                LinkCell(setCells,cell.RightCell);
            }
            if (cell.DownCell != null && cell.PondsDown  > 0){
               LinkCell(setCells,cell.DownCell);
            }

            Cell upCell = cells.Find(c => c.DownCell == cell && c.PondsDown > 0);
            if (upCell!=null)
                LinkCell(setCells, upCell);
            Cell leftCell = cells.Find(c => c.RightCell == cell && c.PondsRights>0);
            if (leftCell != null)
                LinkCell(setCells, leftCell);



        }


        private static void Back()
        {
            Console.Error.WriteLine("-BACK-");

            while (Id >= 0 && cells[Id].IsLastSolution)
            {
             
                cells[Id].PondsRights = -1;
                cells[Id].PondsDown = -1;
                cells[Id].IsLastSolution = false;

               Id--;
            }
                        
            RecalculPondsExt();
            Id--;
        }

        private static void RecalculPondsExt()
        {
            //clear des pondsExt
            foreach (Cell cell in cells)
                cell.PondsExt = 0;

            for (int i = 0; i <Id; i++)
            {
                var cell = cells[i];
                //cell de droite
                if (cell.PondsRights>0)
                    cell.RightCell.PondsExt += cell.PondsRights;
         
                //cell du bas
                if (cell.PondsDown>0)
                    cell.DownCell.PondsExt += cell.PondsDown;
                
            }
        }

        private static bool PondsRightDown(Cell cell,int right, int down)
        {

            bool isChecked = true;
            //check intermediaires (y a t-il croisement)

            if (cell.RightCell != null && right != 0)
            {
                //right : 
               if ( cells.Find(c => c.X > cell.X && c.X < cell.RightCell.X &&
                                    c.Y < cell.Y && c.DownCell != null && c.DownCell.Y > cell.Y && c.PondsDown>0) !=null)
                   isChecked = false;
            }
            if (cell.DownCell != null && down != 0)
            {
                if (cells.Find(c => c.Y > cell.Y && c.Y < cell.DownCell.Y &&
                                    c.X < cell.X && c.RightCell != null && c.RightCell.X > cell.X && c.PondsRights > 0) != null)
                    isChecked = false;

            }
            
            // cas des connexes particuliers
            // 2=2
            if (cells.Count > 2) { 
            if (cell.RightCell != null && cell.RightCell.PondsExt==0   && cell.RightCell.Val==2 &&  right == 2 && down == 0 &&
                                         cell.PondsExt == 0            && cell.Val==2  )
            {
                isChecked = false;
            }
            if (cell.DownCell != null && cell.DownCell.PondsExt == 0 && cell.DownCell.Val == 2 && right ==0 && down == 2 &&
                cell.PondsExt == 0 && cell.Val == 2)
            {
                isChecked = false;
            }

            // 1-1
            if (cell.RightCell != null && cell.RightCell.PondsExt == 0 && cell.RightCell.Val == 1 && right ==1 && down == 0 &&
                cell.PondsExt == 0 && cell.Val ==1)
            {
                isChecked = false;
            }
            if (cell.DownCell != null && cell.DownCell.PondsExt == 0 && cell.DownCell.Val == 1 && right == 0 && down == 1 &&
                cell.PondsExt == 0 && cell.Val == 1)
            {
                isChecked = false;
            }
            }

            
            cell.PondsRights = right;
            cell.PondsDown = down;

          
          //  Console.Error.WriteLine("("+cell.X + "," + cell.Y + ")  :" + cell.Val + ":  " +cell.ValRest+"  "+   right + "/" + down + "   " +( isChecked? "":"False"));

            if (isChecked)
            {
                //cell de droite
                if (cell.RightCell != null && right != 0)
                     cell.RightCell.PondsExt += right;
               
                //cell du bas
                if (cell.DownCell != null && down != 0)
                    cell.DownCell.PondsExt +=  down;

            }
        
            return isChecked;
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

            public int Val {  get { return _val; }
                                set { _val = value; }
            }

            public int PondsExt
            {
                get { return _pondsExt; }
                set {  _pondsExt = value; }
            }

            public int PondsRights
            {
                get { return _pondsRights; }
                set {  _pondsRights = value; }
            }
           
             public int PondsDown
             {
                 get { return _pondsDown; }
                 set {  _pondsDown = value; }
             }
            
             public bool IsLastSolution
             {
                 get { return _isLastSolution; }
                 set {  _isLastSolution = value; }
             }
            
             public int X
             {
                 get { return _x; }
                 set {  _x = value; }
             }
            
             public int Y
             {
                 get { return _y; }
                 set {  _y = value; }
             }
            
             public Cell RightCell
             {
                 get { return _rightCell; }
                 set {  _rightCell = value; }
             }
            
             public Cell DownCell
             {
                 get { return _downCell; }
                 set {  _downCell = value; }
             }


            }
        //---------------------------------------------------------------------------------------------
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

        }

        // code pour trouver chemin connexe : en doublon par rapport au LinkCell sur les 4 directions.
        /*    HashSet<Cell> otherLinkedCells = new HashSet<Cell>();
                         foreach (Cell curCell in cells)
                         {
                             if (!linkedCells.Contains(curCell))
                             {
                                 otherLinkedCells.Clear();
                                 LinkCell(otherLinkedCells, curCell);

                                 // on prends les cells de 'otherLink' n'appartenant pas a la main
                                 int otherLinkSize = otherLinkedCells.Count;
                                 otherLinkedCells.ExceptWith(linkedCells);

                                 if (otherLinkSize!= otherLinkedCells.Count) //si meme nb c'est que les 2 listes n'ont rien en commun
                                 {
                                     linkedCells.UnionWith(otherLinkedCells); 
                                 }
                                 else
                                 {
                                     Console.Error.WriteLine("GRAPHE NON CONNEXE:");
                                     Back();
                                     break;
                                 }
                             }
                         }*/
    }
}

