using System;
using System.Collections.Generic;
using System.Linq;

namespace IAGames
{
    class SkyNet2
    {
        static int L;
        static int E;
       
        class Noeud
        {
            public int ID { get; set; }
            public bool IsGate { get; set; }
            public bool IsPreGate { get; set; }

            public bool InTree { get; set; }

            private HashSet<Noeud> _children;

            public Noeud(int id)
            {
                ID = id;
                _children = new HashSet<Noeud>();
                IsGate = false;
                IsPreGate = false;

                InTree = false;


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
                
                Noeud  noeud1 = noeudLst.First(n => n.ID == N1);
                Noeud noeud2 = noeudLst.First(n => n.ID == N2);
               
                noeud1.Children.Add(noeud2);
                noeud2.Children.Add(noeud1);

            }
   
            for (int i = 0; i < E; i++)
            {
                int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
                // gates[i] = EI;
                var noeud = noeudLst.First(n => n.ID == EI);
                noeud.IsGate = true;
                noeudsGate.Add(noeud);
            }

            var largestNode = noeudLst.OrderBy(c => c.Children.Count).Last(); //ca fait un count et prend le max
            Console.Error.WriteLine("largest ID:" + largestNode.ID);

            //select distinct des noeuds PreGate
            var noeudsPreGate = (HashSet<Noeud>) noeudsGate.SelectMany(g => g.Children).ToList().Distinct();

            foreach (var noeud in noeudsPreGate) { 
                noeud.IsPreGate = true;
                Console.Error.WriteLine("noeudsPreGate: " +noeud.ID);
            }




            // --- GAME LOOP -----
            while (true)
            {
                int SI = 11;//= int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn
                
                // var nbchild = noeudSky.Children.Select(c => c.Children.Count).Max();

                var noeudSI =  noeudLst.First(n => n.ID == SI);

                //on va vhercher le noeudPreGate le plus proche de SI
                //reInit de larbre
                foreach (var noeud in noeudLst)
                    noeud.InTree = false;

                noeudSI = noeudLst.First(n => n.ID == 4);//pour test only
                Noeud preGate =  ParcoursLargeur(noeudSI);
                Console.Error.WriteLine("PreGate  trouve: " + preGate.ID);

                //-- Les noeuds ont reliés à au plus 2 passerelles !
                Noeud gate = preGate.Children.First(); // il peut y avoir 2 gates voir si algo pour choisir

                //remove link :
                Console.WriteLine(preGate.ID + " " + gate.ID);
                preGate.Children.Remove(gate);
                gate.Children.Remove(preGate);

               



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
        }

        private static Noeud ParcoursLargeur(Noeud sommet)
        {
            var queue = new Queue<Noeud>();
            queue.Enqueue(sommet);
            sommet.InTree = true;//flagué

            while (queue.Count > 0)
            {
                Noeud curNode = queue.Dequeue();
                //--manage specific : on cherche le premier PreGate possible
                    Console.Error.WriteLine("manage " + curNode.ID);
                    if (curNode.IsPreGate) return curNode;
                //--
                foreach (var child in curNode.Children)
                {
                    if (!child.InTree)
                    {
                        queue.Enqueue(child);
                        child.InTree = true;
                    }
                }
            }

            return null;
        }

       
        
    }
}