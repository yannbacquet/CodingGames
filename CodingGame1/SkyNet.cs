using System;
using System.Collections.Generic;
using System.Text;

namespace IAGames
{
    class SkyNet
    {
        static int L;
        static int E;
        private static int[,] links;
        private static int[] gates;

       static  List<Tuple<int,int>> lstNoeuds =new List<Tuple<int, int>>();

        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
             L = int.Parse(inputs[1]); // the number of links
             E = int.Parse(inputs[2]); // the number of exit gateways

            links = new int[L, 3]; // list de liens composé de 2 noeuds + isOpen
            gates = new int[E]; //les noeuds gates

            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);

                links[i, 0] = N1;
                links[i, 1] = N2;
                links[i, 2] = 1; //isOpen

            }
            for (int i = 0; i < E; i++)
            {
                int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
                gates[i] = EI;
            }

            // game loop
            while (true)
            {
                int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                bool hasClosed = false;

                //Si va gagner  : Skynet sur lien avec 1 gate
                for (int i = 0; i < E; i++) //pour toutes les gates
                {
                    int gate = gates[i];
                    for (int j = 0; j < L; j++) //pour tous les liens
                    {
                        // si 1 noeud = gate et  1 noeud = Skynet    et lien open
                        if ((links[j, 0] == gate || links[j, 1] == gate) &&
                            (links[j, 0] == SI || links[j, 1] == SI) && links[j, 2] == 1)
                        {
                            if (!hasClosed)
                            {
                                Console.WriteLine(links[j, 0] + " " + links[j, 1]);
                                links[j, 2] = 0;
                                hasClosed = true;
                            }
                        }
                    }
                }

                //Sinon chercher le lien le plus loin et fermer le n-1.
                if (!hasClosed)
                {
                    LiensDe(SI);
                    var res = lstNoeuds[lstNoeuds.Count - 1];
                    Console.WriteLine(res.Item1 + " " + res.Item2);
                }


               /* if (!hasClosed) //NE DEVRAIT PAS PASSER on prend nimporte quel lien a closer  ,mais lié a une autre gate
                {
                    for (int j = 0; j < L; j++)
                    {
                        // si lien open 
                        if (links[j, 2] == 1 && !hasClosed)
                        {
                            Console.WriteLine(links[j, 0] + " " + links[j, 1]);
                            links[j, 2] = 0;
                            hasClosed = true;
                            break;
                        }
                    }
                }*/
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
               if( !lstNoeuds.Contains(new Tuple<int, int>(links[j, 1], noeudP)) && !lstNoeuds.Contains(new Tuple<int, int>(noeudP, links[j, 1]))) { 
                    if (links[j, 0] == noeudP && links[j, 2] == 1)
                    {
                        Console.Error.WriteLine("add"+ links[j, 1] + " "+ noeudP);

                        lstNoeuds.Add(new Tuple<int, int>(links[j, 1], noeudP)); //noeud, noeud parent

                        LiensDe(links[j, 1]);
                        break;
                    }
                    if (links[j, 1] == noeudP && links[j, 2] == 1)
                    {
                        Console.Error.WriteLine("add " + links[j, 0] + "  P" + noeudP);

                        lstNoeuds.Add(new Tuple<int, int>(links[j, 0], noeudP)); //noeud, noeud parent

                        LiensDe(links[j, 0]);
                        break;
                    }
               }
            }
        }

    }
}
