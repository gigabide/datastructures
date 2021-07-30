using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraphAdjacencyMatrix
{
    public interface IDirectedGraph<T>
    {
        void AddVertex(T name);
        void RemoveVertex(T name);
        void AddEdge(T name1, T name2, int cost);
        void RemoveEdge(T name1, T name2);
    }

    //---------------------------------------------------------------------------------------------

    class DirectedGraph<T> : IDirectedGraph<T>
    {
        public T[] V { set; get; }             // Vertex list
        public int[,] E { set; get; }          // Adjacency matrix
        public int NumVertices { set; get; }
        public int MaxNumVertices { set; get; }

        public DirectedGraph(int maxNumVertices)
        {
            NumVertices = 0;
            MaxNumVertices = maxNumVertices;
            V = new T[maxNumVertices];
            E = new int[maxNumVertices, maxNumVertices];
        }

        // FindVertex
        // Returns the index of the given vertex (if found); otherwise returns -1
        // Time complexity: O(n) where n is the number of vertices

        private int FindVertex(T name)
        {
            int i;
            for (i = 0; i < NumVertices; i++)
            {
                if (V[i].Equals(name))
                    return i;
            }
            return -1;
        }

        // AddVertex
        // Adds the given vertex to the graph
        // Note: Duplicate vertices are not added
        // Time complexity: O(n)

        public void AddVertex(T name)
        {
            int i;
            if (NumVertices < MaxNumVertices && FindVertex(name) == -1)
            {
                V[NumVertices] = name;
                for (i = 0; i <= NumVertices; i++)
                {
                    E[i, NumVertices] = -1;
                    E[NumVertices, i] = -1;
                }
                NumVertices++;
            }
        }

        // RemoveVertex
        // Removes the given vertex and all incident edges from the graph
        // Note:  Nothing is done if the vertex does not exist
        // Time complexity: O(n)

        public void RemoveVertex(T name)
        {
            int i, j;
            if ((i = FindVertex(name)) > -1)
            {
                NumVertices--;
                V[i] = V[NumVertices];
                for (j = NumVertices; j >= 0; j--)
                {
                    E[j, i] = E[j, NumVertices];
                    E[i, j] = E[NumVertices, j];
                }
            }
        }

        // AddEdge
        // Adds the given edge (name1, name2) to the graph
        // Note: Duplicate edges are not added
        // Time complexity: O(n) due to FindVertex

        public void AddEdge(T name1, T name2, int cost = 0)
        {
            int i, j;
            if ((i = FindVertex(name1)) > -1 && (j = FindVertex(name2)) > -1)
                if (E[i, j] == -1)
                    E[i, j] = cost;
        }

        // RemoveEdge
        // Removes the given edge (name1, name2) from the graph
        // Note: Nothing is done if the edge does not exist
        // Time complexity: O(n) due to FindVertex

        public void RemoveEdge(T name1, T name2)
        {
            int i, j;
            if ((i = FindVertex(name1)) > -1 && (j = FindVertex(name2)) > -1)
                E[i, j] = -1;
        }

        // Depth-First Search
        // Performs a depth-first search (with re-start)
        // Time complexity: O(n^2)

        public void DepthFirstSearch()
        {
            int i;
            bool[] visited = new bool[NumVertices];

            for (i = 0; i < NumVertices; i++)     // Set all vertices as unvisited
                visited[i] = false;

            for (i = 0; i < NumVertices; i++)
                if (!visited[i])                  // (Re)start with vertex i
                {
                    DepthFirstSearch(i, visited);
                    Console.WriteLine();
                }
        }

        private void DepthFirstSearch(int i, bool[] visited)
        {
            int j;

            visited[i] = true;    // Output vertex when marked as visited
            Console.WriteLine(i);

            for (j = 0; j < NumVertices; j++)    // Visit next unvisited adjacent vertex
                if (!visited[j] && E[i, j] > -1)
                    DepthFirstSearch(j, visited);
        }

        // Breadth-First Search
        // Performs a breadth-first search (with re-start)
        // Time Complexity: O(n^2)

        public void BreadthFirstSearch()
        {
            int i;
            bool[] visited = new bool[NumVertices];

            for (i = 0; i < NumVertices; i++)
                visited[i] = false;               // Set all vertices as unvisited

            for (i = 0; i < NumVertices; i++)
                if (!visited[i])                  // (Re)start with vertex i
                {
                    BreadthFirstSearch(i, visited);
                    Console.WriteLine();
                }
        }

        private void BreadthFirstSearch(int i, bool[] visited)
        {
            int j;
            Queue<int> Q = new Queue<int>();

            Q.Enqueue(i);            // Mark vertex as visited when placed in the queue 
            visited[i] = true;       // Why?

            while (Q.Count != 0)
            {
                i = Q.Dequeue();     // Output vertex when removed from the queue
                Console.WriteLine(i);

                for (j = 0; j < NumVertices; j++)    // Enqueue unvisited adjacent vertices
                    if (!visited[j] && E[i, j] > -1)  
                    {
                        Q.Enqueue(j);
                        visited[j] = true;           // Mark vertex as visited
                    }
            }
        }

        // PrintVertices
        // Prints out all vertices of a graph
        // Time complexity: O(n)

        public void PrintVertices()
        {
            int i;
            for (i = 0; i < NumVertices; i++)
                Console.WriteLine(V[i]);
        }

        // PrintEdges
        // Prints out all edges of the graph
        // Time complexity: O(n^2)

        public void PrintEdges()
        {
            int i, j;
            for (i = 0; i < NumVertices; i++)
                for (j = 0; j < NumVertices; j++)
                    if (E[i, j] > -1)
                        Console.WriteLine("(" + V[i] + "," + V[j] + "," + E[i, j] + ")");
        }
    }

    // Test Class

    class Program
    {
        static void Main(string[] args)
        {
            int i, j;

            Console.WriteLine("Adjacency Matrix Implemention");

            DirectedGraph<char> H = new DirectedGraph<char>(7);

            for (i = 0; i < 7; i++)
                H.AddVertex((char)(i + 'a'));

            H.PrintVertices();

            for (i = 0; i < 7; i += 2)
                for (j = 0; j < 7; j += 3)
                {
                    H.AddEdge((char)(i + 'a'), (char)(j + 'a'), 10);
                }

            H.PrintEdges();
            Console.ReadKey();

            H.RemoveVertex('c');
            H.RemoveVertex('f');

            H.PrintVertices();
            H.PrintEdges();
            Console.ReadKey();

            DirectedGraph<int> G = new DirectedGraph<int>(10);

            for (i = 0; i < 7; i++)
                G.AddVertex(i);

            G.PrintVertices();

            G.AddEdge(0, 1, 0);
            G.AddEdge(1, 3, 0);
            G.AddEdge(1, 4, 0);
            G.AddEdge(3, 0, 0);
            G.AddEdge(1, 2, 0);
            G.AddEdge(2, 5, 0);
            G.AddEdge(5, 6, 0);

            G.PrintEdges();
            Console.ReadKey();

            G.DepthFirstSearch();
            Console.ReadKey();

            G.BreadthFirstSearch();
            Console.ReadKey();
        }
    }
}
