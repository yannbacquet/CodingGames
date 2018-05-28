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

       static  List<Tuple<int,int>> lstArbre =new List<Tuple<int, int>>();

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
            gates = new int[E]; //les noeuds gates

            /*for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);

                links[i, 0] = N1;
                links[i, 1] = N2;
                links[i, 2] = 1; //isOpen

            }*/
            links[0, 0] = 11; links[0, 1] = 6; links[0, 2] = 1;
            links[1, 0] = 0; links[1, 1] = 9; links[1, 2] = 1;
            links[2, 0] = 1; links[2, 1] = 2; links[2, 2] = 1;
            links[3, 0] = 0; links[3, 1] = 1; links[3, 2] = 1;
            links[4, 0] = 10; links[4, 1] = 1; links[4, 2] = 1;
            links[5, 0] = 11; links[5, 1] = 5; links[5, 2] = 1;
            links[6, 0] = 2; links[6, 1] = 3; links[6, 2] = 1;
            links[7, 0] = 4; links[7, 1] = 5; links[7, 2] = 1;
            links[8, 0] = 8; links[8, 1] = 9; links[8, 2] = 1;
            links[9, 0] = 6; links[9, 1] = 7; links[9, 2] = 1;
            links[10, 0] = 7; links[10, 1] = 8; links[10, 2] = 1;
            links[11, 0] = 0; links[11, 1] = 6; links[11, 2] = 1;
            links[12, 0] = 3; links[12, 1] = 4; links[12, 2] = 1;
            links[13, 0] = 0; links[13, 1] = 2; links[13, 2] = 1;
            links[14, 0] = 11; links[14, 1] = 7; links[14, 2] = 1;
            links[15, 0] = 0; links[15, 1] = 8; links[15, 2] = 1;
            links[16, 0] = 0; links[16, 1] = 4; links[16, 2] = 1;
            links[17, 0] = 9; links[17, 1] = 10; links[17, 2] = 1;
            links[18, 0] = 0; links[18, 1] = 5; links[18, 2] = 1;
            links[19, 0] = 0; links[19, 1] = 7; links[19, 2] = 1;
            links[20, 0] = 0; links[20, 1] = 3; links[20, 2] = 1;
            links[21, 0] = 0; links[21, 1] = 10; links[21, 2] = 1;
            links[22, 0] = 5; links[22, 1] = 6; links[22, 2] = 1;


            /* for (int i = 0; i < E; i++)
             {
                 int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
                 gates[i] = EI;
             }*/
            gates[0] = 0;

            // game loop
            while (true)
            {
                int SI = 11;//= int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

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
                    var res = lstArbre[lstArbre.Count - 2];
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
