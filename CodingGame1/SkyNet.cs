using System;
using System.Collections.Generic;
using System.Text;

namespace IAGames
{
    class SkyNet
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways

            int[,] links = new int[L, 3]; // list de liens composé de 2 noeuds + isOpen
            int[] gates = new int[E]; //les noeuds gates

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

               
                for (int i = 0; i < E; i++)
                {
                    int gate = gates[i];
                    for (int j = 0; j < L; j++)
                    {
                        // si lien avec gate a un noeud et Skynet a l'autre   et liens open
                        if((links[j,0]== gate || links[j, 1] == gate) &&
                            (links[j, 0] == SI || links[j, 1] == SI) &&  links[j, 2] == 1)
                        {
                            if (!hasClosed) {
                                Console.WriteLine(links[j, 0] + " " + links[j, 1]);
                                links[j, 2] = 0;
                                hasClosed = true;
                            }
                        }
                    }
                }

                if (!hasClosed) //on prend nimporte quel lien a closer /*mais lié a une autre gate*/
                {
                 
                    for (int j = 0; j < L; j++)
                    {
                        // si lien open 
                        if (links[j, 2] == 1 && !hasClosed) {
                            Console.WriteLine(links[j, 0] + " " + links[j, 1]);
                            links[j, 2] = 0;
                            hasClosed = true;
                        break;
                        }
                    }

                }
            }//END WHILE
        }
    }
}
