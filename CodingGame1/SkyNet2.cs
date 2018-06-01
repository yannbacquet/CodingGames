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
            public int  nbGates { get; set; } //pour un noeud pregate 1 ou 2 sinon 0

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
        static HashSet<Noeud> noeudsGate = new HashSet<Noeud>();

        static void Main(string[] args)
        {
            int N = 37;
           
            for (int i = 0; i < N; i++)
                noeudLst.Add( new Noeud(i));
           
            //-- for Tests
            AddLink(2, 5);AddLink(14, 13);AddLink(16, 13);
            AddLink(19, 21);AddLink(13, 7);AddLink(16, 8);
            AddLink(35, 5);AddLink(2, 35);AddLink(10, 0);
            AddLink(8, 3);AddLink(23, 16);AddLink(0, 1);
            AddLink(31, 17);AddLink(19, 22);AddLink(12, 11);
            AddLink(1, 2);AddLink(1, 4);AddLink(14, 9);
            AddLink(17, 16);AddLink(30, 29);AddLink(32, 22);
            AddLink(28, 26);AddLink(24, 23);AddLink(20, 19);
            AddLink(15, 13);AddLink(18, 17);AddLink(6, 1);
            AddLink(29, 28);AddLink(15, 14);AddLink(9, 13);
            AddLink(32, 18);AddLink(25, 26);AddLink(1, 7);
            AddLink(34, 35);AddLink(33, 34);AddLink(27, 16);
            AddLink(27, 26);AddLink(23, 25);AddLink(33, 3);
            AddLink(16, 30);AddLink(25, 24);AddLink(3, 2);
            AddLink(5, 4);AddLink(31, 32);AddLink(27, 25);
            AddLink(19, 3);AddLink(17, 8);AddLink(4, 2);
            AddLink(32, 17);AddLink(10, 11);AddLink(29, 27);
            AddLink(30, 27);AddLink(6, 4);AddLink(24, 15);
            AddLink(9, 10);AddLink(34, 2);AddLink(9, 7);
            AddLink(11, 6);AddLink(33, 2);AddLink(14, 10);
            AddLink(12, 6);AddLink(0, 6);AddLink(19, 17);
            AddLink(20, 3);AddLink(21, 20);AddLink(21, 32);
            AddLink(15, 16);AddLink(0, 9);AddLink(23, 27);
            AddLink(11, 0);AddLink(28, 27);AddLink(22, 18);
            AddLink(3, 1);AddLink(23, 15);AddLink(18, 19);
            AddLink(7, 0);AddLink(19, 8);AddLink(21, 22);
            AddLink(7, 36);AddLink(13, 36);AddLink(8, 36);
            
            AddGates(new int[] {18, 0, 16, 26});

            //-- for Tests



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


            //-- for Tests
            int[] SIS = {2,1,7,13,15,23,27,26};
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

                 Noeud preGate =  FindPregateFrom(noeudSI,2) ?? FindPregateFrom(noeudSI, 1);

                Console.Error.WriteLine("PreGate  trouvé: " + preGate.ID);

                //-- Les noeuds sont reliés à au plus 2 passerelles !
                Noeud gate = preGate.Children.First(c=>c.IsGate); // il peut y avoir 2 gates voir si algo pour choisir

                //remove link :
                Console.WriteLine(preGate.ID + " " + gate.ID);
                preGate.Children.Remove(gate);
                gate.Children.Remove(preGate);
                preGate.nbGates--;
                Console.Error.WriteLine("PreGate  " + preGate.ID + "new  nbGates: " + preGate.nbGates+"\n");

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

        private static void AddGates(int[] gates)
        {
            foreach (var gateId in gates)
            {
                var noeud = noeudLst.First(n => n.ID == gateId);
                noeud.IsGate = true;
                noeudsGate.Add(noeud);
            }
          
        }

        private static void AddLink(int n1, int n2)
        {
            Noeud noeud1 = noeudLst.First(n => n.ID == n1);
            Noeud noeud2 = noeudLst.First(n => n.ID == n2);
            noeud1.Children.Add(noeud2); noeud2.Children.Add(noeud1);
        }

        private static Noeud FindPregateFrom(Noeud sommet,int nbGates)
        {
            Console.Error.WriteLine("FindPregateFrom (" + sommet.ID+","+nbGates+")");
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
                //Console.Error.WriteLine("manage " + curNode.ID);
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