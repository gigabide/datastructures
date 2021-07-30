using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes
{
    public class Rope
    {
        public Node root { get; set; }

        // Generic Node Class
        public class Node
        {
            public string S { get; set; }   // The string stored at the given node.
            public int Length { get; set; } // Length of the string represented by a given node.
            public Node Left { get; set; }  // Left and Right children.
            public Node Right { get; set; }

            // Node Constructors
            public Node(){}
            public Node(string s)
            {
                S = s;
                Length = s.Length;
            }
            public Node(string s, Node L, Node R)
            {
                S = s;
                Left = L;
                Right = R;
                Length = s.Length;
            }
        }
        // Empty Rope Constructor
        public Rope()
        {
            root = new Node();
        }
        // Rope from String Constructor
        public Rope(string s)
        {
            root = Build(s);
        }

        // Build
        // Creates a rope to represent a given string. Returns the node at which the rope is rooted.
        // Time Complexity: I don't really know.
        private Node Build(string s)
        {
            if (s.Length <= 4) // If the string is four chars or less, store it at current node.
            {
                return new Node
                {
                    Length = s.Length,
                    S = s,
                };
            }

            int mid = s.Length / 2; // Store position of the middle of the string.
            return new Node
            {
                Length = s.Length, // Node length equals the length of the string it represents.
                Left = Build(s.Substring(0, mid)), // Left built from beginning to middle of string.
                Right = Build(s.Substring(mid)) // Right built from middle to end of string.
            };
        }

        // Concatenate
        // Concatenates two ropes, R1 and R2, into a new rope r. Returns the new rope.
        // Time Complexity: O(1)
        public Rope Concatenate(Rope R1, Rope R2)
        {
            Rope r = new Rope(); // Creates new rope r.
            r.root.Left = R1.root; // Sets left and right children of r to R1 and R2.
            r.root.Right = R2.root;
            r.root.Length = r.root.Left.Length + r.root.Right.Length; // Set total length of r.
            return r;
        }

        // Split
        // Splits one rope into two ropes R1 and R2.
        // Time Complexity: O(n log n)
        public void Split(int i, Rope R1, Rope R2)
        {
            Node curr = root;
            List<Node> path = new List<Node>();
            List<Node> orphans = new List<Node>();
            while (curr.Left != null && curr.Right != null)
            {
                if (i <= curr.Left.Length) { curr = curr.Left; }
                else
                {
                    i -= curr.Left.Length; // Update i to the difference
                    curr = curr.Right; // Look right
                }
                path.Add(curr); // Record node in path
            }
            if (curr.S[i] != curr.S.Max()) // If a split occurs within a node, crack the node
            {
                Node N1 = new Node(curr.S.Substring(0, i));
                Node N2 = new Node(curr.S.Substring(i));
                curr.Left = N1;
                curr.Right = N2;
            }
            int pathsize = path.Count;
            for (int j = 0; j < pathsize; ++j) // Go back through the path and 'cut the children'.
            {
                curr = path.Last();
                curr.Length -= curr.Right.Length;
                orphans.Add(curr.Right);
                curr.Right = null;
                path.RemoveAt(path.Count - 1);
            }
            /* Here you can see my algorithm begin to get unnecissarily verbose, as I desperately
             * attempt to snip off the correct right-children and attempt to link them back together
               by storing orphaned nodes in a generic list similar to the path. It did not go well. Enjoy. */
            R2.root.Right = root.Right;
            root.Right = null;
            root.Length = root.Left.Length;
            R1.root = root;
            int orphsize = orphans.Count;
            Node link = orphans.First();
            Node link2 = new Node();
            for (int l = 0; l < orphsize-1; l++)
            {
                orphans.RemoveAt(0); // Remove first list element
                link2 = orphans.First(); // Store new first node as link 2
                link2.Left = link; // Link them
                link = link2;
            }
            R2.root.Left = link2;
        }
        public void Insert(string S, int i)
        {
            Rope R1 = new Rope(S); // Build new rope R1 from S
            Rope R2 = new Rope();
            Rope R3 = new Rope();
            Split(i, R2, R3); // Split current rope into R2 and R3
            Rope temp = new Rope();
            temp = Concatenate(R2, R1); // Concatenate R1 and R2
            temp = Concatenate(temp, R3); // ... and R3
            root = temp.root;
        }
        public void Delete(int i, int j)
        {
            Rope R1 = new Rope();
            Rope R2 = new Rope();
            Rope R3 = new Rope();
            Split(i - 1, R1, R2); // Split current rope at indices i-1 and j to give roped R1, R2, and R3
            Split(j, R2, R3);
            Concatenate(R1, R3); // Concatenate R1 and R3
        }
        public string Substring(int i, int j)
        {
            Delete(j, Length());
            Delete(0, i);

            return ToString();
        }
        public char CharAt(int i)
        {
            Node curr = root;
            while (curr.Left != null && curr.Right != null)
            {
                if (i <= curr.Left.Length) { curr = curr.Left; }
                else
                {
                    i -= curr.Left.Length; // Update i to the difference
                    curr = curr.Right;
                }
            }
            return curr.S[i-1];
        }
        //public int IndexOf(char c)
        //{

        //}
        public void Reverse()
        {
            Print(root);
            Console.WriteLine();

            void Print(Node print) // Recursive print function inspired by my friend Fadi
            {
                if (print == null) { return; }
                if (print.Left == null && print.Right == null)
                {
                    Console.Write(print.S);
                } // If node is a leaf, print it.
                Print(print.Right); // Else recursively call print.
                Print(print.Left);
            }
        }
        public int Length()
        {
            return root.Length;
        }
        public string ToString()
        {
            string tostring = "";
            Print(root); // Literally same print function but builds string
            return tostring;

            void Print(Node print) // Recursive print function inspired by my friend Fadi
            {
                if (print == null) { return; }
                if (print.Left == null && print.Right == null) { tostring += print.S; } // If node is a leaf, print it.
                Print(print.Left); // Else recursively call print.
                Print(print.Right);
            }
        }
        public void PrintRope()
        {
            Print(root);
            Console.WriteLine();

            void Print(Node print) // Recursive print function inspired by my friend Fadi
            {
                if (print == null) { return; }
                if (print.Left == null && print.Right == null) { Console.Write(print.S); } // If node is a leaf, print it.
                Print(print.Left); // Else recursively call print.
                Print(print.Right);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rope R3 = new Rope("This_is_an_easy_assignment.");
            Console.WriteLine(R3.ToString());
            Console.ReadLine();
        }
    }
}
