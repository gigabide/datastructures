using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyBinomialHeaps
{
    public class BinomialNode<T>
    {
        public T Item { get; set; }
        public int Degree { get; set; }
        public BinomialNode<T> LeftMostChild { get; set; }
        public BinomialNode<T> RightSibling { get; set; }

        // Constructor

        public BinomialNode(T item)
        {
            Item = item;
            Degree = 0;
            LeftMostChild = null;
            RightSibling = null;
        }
    }

    //--------------------------------------------------------------------------------------

    // Common interface for all non-linear data structures

    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Test if an instance is empty
        int Size();        // Return the number of items in an instance
    }

    //--------------------------------------------------------------------------------------

    public interface IBinomialHeap<T> : IContainer<T> where T : IComparable
    {
        void Add(T item);               // Add an item to a binomial heap
        void Remove();                  // Remove the item with the highest priority
        T Front();                      // Return the item with the highest priority
    }

    //--------------------------------------------------------------------------------------

    // Binomial Heap
    // Implementation:  Leftmost-child, right-sibling

    public class BinomialHeap<T> : IBinomialHeap<T> where T : IComparable
    {
        private BinomialNode<T>[] B;        // Array of root lists
        private int size;                   // Size of the binomial heap
        private BinomialNode<T> highest;    // Stores highest

        // Contructor
        // Time complexity:  O(1)

        public BinomialHeap()
        {
            B = new BinomialNode<T>[6];   // Header node
            size = 0;
        }

        // Add
        // Inserts an item into the binomial heap
        // Time complexity:  O(log n)

        public void Add(T item)
        {
            BinomialNode<T> node = new BinomialNode<T>(item);
            if (B[0] != null)
                node.RightSibling = B[0];
            B[0] = node;
            if (highest != null)
            {
                if (node.Item.CompareTo(highest.Item) > 0)
                    highest = node;
            }
            else
                highest = node;
            size++;
        }

        // Coalesce
        // Combines trees at each order to ensure only one tree of B[k]
        // Time Complexity: O(n)
        public void Coalesce()
        {
            BinomialNode<T> curr;

            for(int i = 0; i < B.Length; ++i)
            {
                if (B[i] != null)
                {
                    curr = B[i];
                    if (curr.RightSibling != null && curr.RightSibling.RightSibling != null)
                    {
                        BinomialNode<T> T1 = curr.RightSibling;
                        BinomialNode<T> T2 = curr.RightSibling.RightSibling;

                        if (T1.Item.CompareTo(T2.Item) > 0)
                        {
                            if (T1.Item.CompareTo(highest.Item) > 0)
                                highest = T1;
                            B[i+1] = BinomialLink(T1, T2);
                        }
                        else
                        {
                            if (T2.Item.CompareTo(highest.Item) > 0)
                                highest = T2;
                            B[i+1] = BinomialLink(T2, T1);
                        }
                    }
                }
            }
        }

        // Remove
        // Removes the item with the highest priority from the binomial heap
        // Time complexity:  O(log n)

        public void Remove()
        {
            if (!Empty())
            {
                BinomialHeap<T> H = new BinomialHeap<T>();
                BinomialNode<T> p;
                BinomialNode<T> q = new BinomialNode<T>(default(T));
                BinomialNode<T> curr;

                // Get the reference to the preceding node with the highest priority
                for(int i=0; i<B.Length; ++i)
                {
                    if(B[i] != null)
                    {
                        curr = B[i];
                        if (curr == highest)
                        {
                            // This is the case that gave me real trouble.
                        }
                        while (curr.RightSibling != null)
                        {
                            if (curr.RightSibling.Item.CompareTo(highest.Item) == 0)
                            {
                                q = curr;
                                break;
                            }
                            else
                                curr = curr.RightSibling;
                        }
                    }
                }

                // Remove binomial tree p from root list
                p = q.RightSibling;
                q.RightSibling = q.RightSibling.RightSibling;

                // Add binomial subtrees of p in reverse order into H
                p = p.LeftMostChild;
                while (p != null)
                {
                    q = p.RightSibling;

                    // Splice p into H as the first binomial tree
                    p.RightSibling = H.B[0].RightSibling;
                    H.B[0].RightSibling = p;

                    p = q;
                }
                size--;
                highest = B[0];
                Coalesce();
            }
        }

        // Front
        // Returns the item with the highest priority
        // Time complexity:  O(log n)

        public T Front()
        {
            if (!Empty())
            {
                // Get the reference to the preceding node with the highest priority
                return highest.Item;
            }
            else
                return default(T);
        }

        // BinomialLink
        // Makes child the leftmost child of root
        // Time complexity:  O(1)

        private BinomialNode<T> BinomialLink(BinomialNode<T> child, BinomialNode<T> root)
        {
            child.RightSibling = root.LeftMostChild;
            root.LeftMostChild = child;
            root.Degree++;
            return root;
        }

        // MakeEmpty
        // Creates an empty binomial heap
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            for (int i = 0; i < B.Length; i++)
                B[i].RightSibling = null;
            size = 0;
        }

        // Empty
        // Returns true is the binomial heap is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return size == 0;
        }

        // Size
        // Returns the number of items in the binomial heap
        // Time complexity:  O(1)

        public int Size()
        {
            return size;
        }

        public void Print()
        {
            BinomialNode<T> curr;

            for (int i=0; i<B.Length; ++i)
            {
                if (B[i] != null)
                {
                    Console.WriteLine("Order " + i);
                    curr = B[i];
                    while (curr != null)
                    {
                        Console.WriteLine(curr.Item);
                        if (curr.RightSibling != null)
                            curr = curr.RightSibling;
                        else
                            curr = null;
                    }
                }
            }
        }
    }

    //--------------------------------------------------------------------------------------

    // Used by class BinomailHeap<T>
    // Implements IComparable and overrides ToString (from Object)

    public class PriorityClass : IComparable
    {
        private int priorityValue;
        private char letter;

        public PriorityClass(int priority, char letter)
        {
            this.letter = letter;
            priorityValue = priority;
        }

        public int CompareTo(Object obj)
        {
            PriorityClass other = (PriorityClass)obj;   // Explicit cast
            return priorityValue - other.priorityValue;  // High values have higher priority
        }

        public override string ToString()
        {
            return letter.ToString() + " with priority " + priorityValue;
        }
    }

    //--------------------------------------------------------------------------------------

    // Test for above classes

    public class Program
    {
        public static void Main(string[] args)
        {
            int i;
            Random r = new Random(1);

            BinomialHeap<PriorityClass> BH = new BinomialHeap<PriorityClass>();

            for (i = 0; i < 20; i++)
            {
                BH.Add(new PriorityClass(r.Next(50), (char)('a')));
            }

            BH.Remove();
            BH.Print();
            Console.ReadLine();
        }
    }
}
