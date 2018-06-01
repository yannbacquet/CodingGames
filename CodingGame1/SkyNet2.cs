using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace IAGames
{
    class SkyNet2
    {
        static int L;
        static int E;
       
        class Noeud
        {
            public int ID { get; set; }
            private HashSet<Noeud> _children;

            public bool IsGate { get; set; }
            public int  nbGates { get; set; }

            public int Level { get; set; } //niveau de profondeur dun noeud

            
            public Noeud(int id)
            {
                ID = id;
                _children = new HashSet<Noeud>();
                IsGate = false;
                nbGates = 0;

                Level = -1;
            }
          
            public HashSet<Noeud> Children => _children;
        }


        static HashSet<Noeud> noeudLst = new HashSet<Noeud>();

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

           
            HashSet<Noeud> noeudsGate = new HashSet<Noeud>();

            for (int i = 0; i < N; i++)
                noeudLst.Add( new Noeud(i));
            
            for (int i = 0; i < L; i++)
            {
                // inputs = Console.ReadLine().Split(' ');
                int N1 = 2;//int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = 3;//int.Parse(inputs[1]);

               /*TESTS Console.Error.WriteLine("Noeud  noeud1 = noeudLst.First(n => n.ID ==" + N1 + ");");
                Console.Error.WriteLine("Noeud  noeud2 = noeudLst.First(n => n.ID ==" + N2 + ");");

                Console.Error.WriteLine("noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);");
                */
                /* Noeud  noeud1 = noeudLst.First(n => n.ID == N1);
                 Noeud noeud2 = noeudLst.First(n => n.ID == N2);
                
                 noeud1.Children.Add(noeud2);
                 noeud2.Children.Add(noeud1);*/
            }
            Noeud noeud1 = noeudLst.First(n => n.ID == 6);
            Noeud noeud2 = noeudLst.First(n => n.ID == 2);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 7);
             noeud2 = noeudLst.First(n => n.ID == 3);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 6);
             noeud2 = noeudLst.First(n => n.ID == 3);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 5);
             noeud2 = noeudLst.First(n => n.ID == 3);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 3);
             noeud2 = noeudLst.First(n => n.ID == 4);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 7);
             noeud2 = noeudLst.First(n => n.ID == 1);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 2);
             noeud2 = noeudLst.First(n => n.ID == 0);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 0);
             noeud2 = noeudLst.First(n => n.ID == 1);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 0);
             noeud2 = noeudLst.First(n => n.ID == 3);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 1);
             noeud2 = noeudLst.First(n => n.ID == 3);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 2);
             noeud2 = noeudLst.First(n => n.ID == 3);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 7);
             noeud2 = noeudLst.First(n => n.ID == 4);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
             noeud1 = noeudLst.First(n => n.ID == 6);
             noeud2 = noeudLst.First(n => n.ID == 5);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);



            /*for (int i = 0; i < E; i++)
            {
                int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
                // gates[i] = EI;
                var noeud = noeudLst.First(n => n.ID == EI);
                noeud.IsGate = true;
                noeudsGate.Add(noeud);
            }*/
            var noeud = noeudLst.First(n => n.ID == 4);
            noeud.IsGate = true;
            noeudsGate.Add(noeud);
            noeud = noeudLst.First(n => n.ID == 5);
            noeud.IsGate = true;
            noeudsGate.Add(noeud);




            var largestNode = noeudLst.OrderBy(c => c.Children.Count).Last(); //ca fait un count et prend le max
            Console.Error.WriteLine("largest ID:" + largestNode.ID);

            //select distinct des noeuds PreGate
            //var noeudsPreGate = (HashSet<Noeud>) noeudsGate.SelectMany(g => g.Children).ToList().Distinct();
            foreach (var noeudGate in noeudsGate)
            {
                foreach (var noeudPreGate in noeudGate.Children)
                {
                    noeudPreGate.nbGates++;
                }
            }


            int[] SIS = {0, 3, 6, 3};
            int siId = -1;
            // --- GAME LOOP -----
            while (siId<=2)
            {
                siId++;
                int SI = SIS[siId];//= int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

                // var nbchild = noeudSky.Children.Select(c => c.Children.Count).Max();
                //noeudSI = noeudLst.First(n => n.ID == 4);//pour test only
                var noeudSI =  noeudLst.First(n => n.ID == SI);


                Console.Error.WriteLine("SI se deplace en " + SI + " nb gates="+noeudSI.nbGates);

                 Noeud preGate =  ParcoursLargeurPreGate(noeudSI,2) ?? ParcoursLargeurPreGate(noeudSI, 1);

                Console.Error.WriteLine("PreGate  trouve: " + preGate.ID);

                //-- Les noeuds sont reliés à au plus 2 passerelles !
                Noeud gate = preGate.Children.First(c=>c.IsGate); // il peut y avoir 2 gates voir si algo pour choisir

                //remove link :
                Console.WriteLine(preGate.ID + " " + gate.ID);
                preGate.Children.Remove(gate);
                gate.Children.Remove(preGate);
                preGate.nbGates--;
                Console.Error.WriteLine("PreGate  " + preGate.ID + " nbGates: " + preGate.nbGates);

                //remover aussi le flag isPregate ou plustot pregate--;

             

                //-------------
                // var noeud2 = noeudSI.Children.FirstOrDefault(c => c.IsGate);

                //le noeud a supprimer doit etre lie a une passerelle
                //noeud1 = une passerelle  noeud2 = 1 child de la passerellle 
                // var noeudGate = monNoeud.Children.FirstOrDefault(c => c.IsGate);



                /***
                //  var noeudsNonMarques = noeud.Children.Where(c => c.InTree == false);

                //Skynet nest pas pres d'une porte
                if (noeud2 == null)
                {
                    //noeud child de SI qui pointe vers 'largestNode'
                    noeud2 =  noeudSI.Children.Where(c => c.Children.Contains(largestNode)).OrderBy(n => n.Children.Count).FirstOrDefault();
                    if (noeud2!=null) Console.Error.WriteLine("contains Largest:" + noeud2.ID);
                    if (noeud2 == null) {
                        noeud2 = noeudSI.Children.OrderBy(c => c.Children.Count).Last();//noeud conteant le + de children
                        Console.Error.WriteLine("Not contain Largest:" + noeud2.ID);
                    }
                }
               
                //remove link :
                Console.WriteLine(noeudSI.ID + " " + noeud2.ID);
                noeudSI.Children.Remove(noeud2);
                noeud2.Children.Remove(noeudSI);
                 **/



            }//END WHILE

            Console.ReadKey();
        }


        private static Noeud ParcoursLargeurPreGate(Noeud sommet,int nbGates)
        {
            Console.Error.WriteLine("Parcours (" + sommet.ID+","+nbGates+")");
            foreach (var noeud in noeudLst)
                noeud.Level = -1;

            int level = 0;

            var queue = new Queue<Noeud>();
            queue.Enqueue(sommet);
            sommet.Level = level;//flagué

            if (sommet.nbGates>0)
               return sommet;

            while (queue.Count > 0)
            {
                Noeud curNode = queue.Dequeue();
                //--manage specific : on cherche le premier PreGate possible
                Console.Error.WriteLine("manage " + curNode.ID);
                if (curNode.nbGates == nbGates)
                {
                    return curNode;
                    /* int nbGates = curNode.Children.Count(c => c.IsGate);//Noeud gate = curNode.Children.FirstOrDefault(c => c.IsGate);
                     if (nbGates==2)//cest le max
                        return curNode;
                     if (nbGates == 1) //on peut peut etre trouver 2 gates sur la meme profondeur( meme level)
                     {
                        var nodeWithtwoGates = queue.FirstOrDefault(n => n.IsPreGate && n.Level == curNode.Level && n.Children.Count(c => c.IsGate) == 2);
                         if (nodeWithtwoGates != null)
                             return nodeWithtwoGates;
                         else
                             return curNode;
                     }*/
                    //else nbGates==0 on continue
                }
                //--
                level++;
               


                foreach (var child in curNode.Children)
                {
                    if (child.Level==-1)//not in tree
                    {
                        queue.Enqueue(child);
                        child.Level = level;
                    }
                }
            }

            return null;
        }

       
        
    }
}