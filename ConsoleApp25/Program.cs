using System;
using System.Collections.Generic;

public class FordFulkerson
{
    private int V;

    public FordFulkerson(int v)
    {
        V = v;
    }

    public int FindMaxFlow(int[,] graph, int source, int sink)
    {
        int[,] residualGraph = new int[V, V];
        Array.Copy(graph, 0, residualGraph, 0, V * V);

        int[] parent = new int[V];
        int maxFlow = 0;


        while (BFS(residualGraph, source, sink, parent))
        {
            int pathFlow = int.MaxValue;

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
            }

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                residualGraph[u, v] -= pathFlow;
                residualGraph[v, u] += pathFlow;
            }

            maxFlow += pathFlow;
        }

        return maxFlow;
    }

    private bool BFS(int[,] residualGraph, int source, int sink, int[] parent)
    {
        bool[] visited = new bool[V];
        Queue<int> queue = new Queue<int>();

        queue.Enqueue(source);
        visited[source] = true;
        parent[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            for (int v = 0; v < V; v++)
            {
                if (!visited[v] && residualGraph[u, v] > 0)
                {
                    queue.Enqueue(v);
                    visited[v] = true;
                    parent[v] = u;
                }
            }
        }

        return visited[sink];
    }

    public static void Main(string[] args)
    {
        int[,] graph = new int[,]
        {
 { 0, 16, 13, 0, 0, 0 },
 { 0, 0, 10, 12, 0, 0 },
 { 0, 4, 0, 0, 14, 0 },
 { 0, 0, 9, 0, 0, 20 },
 { 0, 0, 0, 7, 0, 4 },
 { 0, 0, 0, 0, 0, 0 }
        };

        FordFulkerson fordFulkerson = new FordFulkerson(6);
        int source = 0;
        int sink = 5;

        int maxFlow = fordFulkerson.FindMaxFlow(graph, source, sink);

        Console.WriteLine("Максимальный поток: " + maxFlow);
        Console.ReadLine();
    }
    
}
