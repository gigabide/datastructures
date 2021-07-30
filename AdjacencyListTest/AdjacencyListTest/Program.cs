using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdjacencyListTest
{
    /* DEFINE EDGE AND VERTEX */

    public class Edge<T>
    {
        public Vertex<T> AdjVertex { get; set; }
        public int Cost { get; set; }

        public Edge(Vertex<T> vertex, int cost)
        {
            AdjVertex = vertex;
            Cost = cost;
        }
    }

    public class Vertex<T>
    {
        public T Name { get; set; } // Vertex name
        public bool Visited { get; set; }
        public List<Edge<T>> E { get; set; } // List of adjacency vertices

        public Vertex(T name)
        {
            Name = name;
            Visited = false;
            E = new List<Edge<T>>();
        }    
        
        // FindEdge
        // Returns the index of the given adjacent vertex in E; otherwise returns -1
        // TimeComplexity: O(n) where n is the number of vertices.

        public int FindEdge(T name)
        {
            int i;
            for(i=0; i<E.Count; i++)
            {
                if (E[i].AdjVertex.Name.Equals(name))
                    return i;
            }
            return -1;
        }
    }

    /* ---------------------- */

    public interface IDirectedGraph<T>
    {
        void AddVertex(T name);
        void RemoveVertex(T name);
        void AddEdge(T name1, T name2, int cost);
        void RemoveEdge(T name1, T name2);
    }

    /* ---------------------- */

    public class DirectedGraph<T> : IDirectedGraph<T>
    {
        private List<Vertex<T>> V;

        public DirectedGraph()
        {
            V = new List<Vertex<T>>();
        }

        // FindVertex
        // Returns the index of the given vertex (if found); otherwise returns -1;\
        // TimeComplexity: O(n)

        private int FindVertex(T name)
        {
            int i;

            for(i=0; i < V.Count; i++)
            {
                if (V[i].Name.Equals(name))
                    return i;
            }
            return -1;
        }

        // AddVertex
        // Adds the given vertex to the graph
        // Note: Duplicate vertices are not added
        // TimeComplexity: O(n) due to FindVertex

        public void AddVertex(T name)
        {
            if (FindVertex(name) == -1)
            {
                Vertex<T> v = new Vertex<T>(name);
                V.Add(v);
            }
        }

        // RemoveVertex
        // Removes the given vertex and all incident edges from the graph 
        // Note: Nothing is done if the vertex does not exist.
        // TimeComplexity: O(max(n, m)) where m is the number of edges.

        public void RemoveVertex(T name)
        {
            int i, j, k;
            if((i=FindVertex(name)) > -1)
            {
                for (j = 0; j < V.Count; j++)
                {
                    for (k=0; k < V[j].E.Count; k++)
                        if (V[j].E[k].AdjVertex.Name.Equals(name))
                        {
                            V[j].E.RemoveAt(k);
                            break;
                        }
                }
                V.RemoveAt(i);
            }
        }

        // AddEdge
        // Adds the given edge (name1, name2) to the graph
        // Notes: Duplicate edges are not added
        //        By default, the cost of the edge is 0
        // Time complexity: O(n)

        public void AddEdge(T name1, T name2, int cost = 0)
        {
            int i, j;
            Edge<T> e;

            // Do the vertices exist?
            if ((i = FindVertex(name1)) > -1 && (j = FindVertex(name2)) > -1)
            {
                // Does the edge not already exist?
                if (V[i].FindEdge(name2) == -1)
                {
                    e = new Edge<T>(V[j], cost);
                    V[i].E.Add(e);
                }
            }
        }

        // RemoveEdge
        // Removes the given edge (name1, name2) from the graph
        // Note: Nothing is done if the edge does not exist
        // Time complexity: O(n)

        public void RemoveEdge(T name1, T name2)
        {
            int i, j;
            if ((i = FindVertex(name1)) > -1 && (j = V[i].FindEdge(name2)) > -1)
                V[i].E.RemoveAt(j);
        }

        // PrintVertices
        // Prints out all vertices of a graph
        // Time complexity: O(n)

        public void PrintVertices()
        {
            for (int i = 0; i < V.Count; i++)
                Console.WriteLine(V[i].Name);
            Console.ReadLine();
        }

        // PrintEdges
        // Prints out all edges of the graph
        // Time complexity: O(m)

        public void PrintEdges()
        {
            int i, j;
            for (i = 0; i < V.Count; i++)
                for (j = 0; j < V[i].E.Count; j++)
                    Console.WriteLine("(" + V[i].Name + "," + V[i].E[j].AdjVertex.Name + "," + V[i].E[j].Cost + ")");
            Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int i, j;

            //Console.WriteLine("Adjacency Matrix Implementation");

            DirectedGraph<char> H = new DirectedGraph<char>();

            H.AddVertex('a');
            H.AddVertex('b');
            H.AddVertex('c');
            H.PrintVertices();
            H.AddEdge('a', 'b', 10);
            H.PrintEdges();
        }
    }
}
