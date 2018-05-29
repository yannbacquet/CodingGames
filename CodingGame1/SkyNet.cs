using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAGames
{
    class SkyNet
    {
        static int L;
        static int E;
        private static int[,] links;
        private static int[] gates;

       static  List<Tuple<int,int>> lstArbre =new List<Tuple<int, int>>();

        class Noeud
        {
            public int ID { get; set; }
            public bool IsGate { get; set; }
            
            private HashSet<Noeud> _children;

            public Noeud(int id)
            {
                ID = id;
                _children = new HashSet<Noeud>();
                IsGate = false;
              
            }
          
            public HashSet<Noeud> Children => _children;
        }

        static void Main(string[] args)
        {
            /*string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
             L = int.Parse(inputs[1]); // the number of links
             E = int.Parse(inputs[2]); // the number of exit gateways*/
            int N = 12;
            L = 23;
            E = 1;

            links = new int[L, 3]; // list de liens composé de 2 noeuds + isOpen
            HashSet<Noeud> noeudLst = new HashSet<Noeud>();
            gates = new int[E]; //les noeuds gates

            for (int i = 0; i < L; i++)
            {
               // inputs = Console.ReadLine().Split(' ');
                int N1 = 2;//int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = 3;//int.Parse(inputs[1]);

               
               Noeud  noeud1 = noeudLst.FirstOrDefault(n => n.ID == N1);
               if (noeud1 == null)
                    noeudLst.Add(noeud1 = new Noeud(N1));
               
               Noeud noeud2 = noeudLst.FirstOrDefault(n => n.ID == N2);
               if (noeud2 == null)
                    noeudLst.Add(noeud2 = new Noeud(N2));
               
               noeud1.Children.Add(noeud2);
               noeud2.Children.Add(noeud1);

            }
   
             for (int i = 0; i < E; i++)
             {
                 int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
                // gates[i] = EI;
                 var noeud = noeudLst.FirstOrDefault(n => n.ID == EI);
                 if (noeud != null) noeud.IsGate = true;
             }

            
            // game loop
            while (true)
            {
                int SI = 11;//= int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

              /*  Node agent = nodes.Where(n => n.Index == SI).First();
                Node exit = nodes.Where(n => n.IsExit && agent.Children.Contains(n)).FirstOrDefault();
                Node largestNode = nodes.OrderBy(n => n.Children.Count).Last();
                Node bestNode = exit == null ? agent.Children.Where(n => n.Children.Contains(largestNode)).OrderBy(n => n.Children.Count).FirstOrDefault() : exit;
                Node kill = bestNode == null ? agent.Children.OrderBy(n => n.Children.Count).Last() : bestNode;
                */


                var noeudSk =  noeudLst.First(n => n.ID == SI);
                //si le noeud Skynet est a cote d'une gate =>noeud2 = gate , sinon on prend le 1er children
                var noeud2 = noeudSk.Children.FirstOrDefault(c => c.IsGate) ?? noeudSk.Children.ToList()[0];

                //remove link :
                Console.WriteLine(noeudSk.ID + " " + noeud2.ID);
                noeudSk.Children.Remove(noeud2);
                noeud2.Children.Remove(noeudSk);

             

              
            }//END WHILE
        }

        static void LiensDe(int noeudP)
        {
            //si noeud est une gate
            for (int i = 0; i < E; i++)
            {
                if (noeudP == gates[i])
                {
                    Console.Error.WriteLine("gate atteinte" + noeudP);
                    return;
                }
            }

            for (int j = 0; j < L; j++)
            {
                //si pas contenu deja dans la liste
                //if( !lstNoeuds.Contains(new Tuple<int, int>(links[j, 1], noeudP)) && !lstNoeuds.Contains(new Tuple<int, int>(noeudP, links[j, 1]))) {
                if (lstArbre.Find(l=> (l.Item1== links[j, 0] && l.Item2 == links[j, 1]) || (l.Item1 == links[j, 1] && l.Item2 == links[j, 0]))==null){

                    if (links[j, 0] == noeudP && links[j, 2] == 1)
                    {
                        Console.Error.WriteLine("add "+ links[j, 1] + " -> "+ noeudP);

                        lstArbre.Add(new Tuple<int, int>(links[j, 1], noeudP)); //noeud, noeud parent

                        LiensDe(links[j, 1]);
                        break;
                    }
                    if (links[j, 1] == noeudP && links[j, 2] == 1)
                    {
                        Console.Error.WriteLine("add " + links[j, 0] + " -> " + noeudP);

                        lstArbre.Add(new Tuple<int, int>(links[j, 0], noeudP)); //noeud, noeud parent

                        LiensDe(links[j, 0]);
                        break;
                    }
               }
            }
        }

    }
}
