using System;
using System.Text;
using System.Linq;
using System.Numerics;
using System.Diagnostics;

namespace Graphenalgorithmen_V2
{
    internal class Manager
    {
        public void UseGraph()
        {
            // Initialize the graph
            Graph graph = new Graph(new List<Node>(), new List<Edge>());

            // Generate a 3x3 field
            int n = 3;
            graph.GenerateField(n);

            // Print the Nodes and Edges of the graph
            Console.WriteLine(graph.ToString());
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            // Starting the Stopwatch
            var watch = Stopwatch.StartNew();

            // GetAllPaths with a practically guaranteed chance of finding all paths
            List<List<Node>> paths = paths = graph.GetAllPaths(graph.Nodes[6], graph.Nodes[5]);

            // Stop the Stopwatch
            watch.Stop();

            // Print all found paths
            Console.WriteLine(PathsToString(paths));
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            // Get shortest path
            List<Node?> shortestPath = graph.GetShortestPath(graph.Nodes[6], graph.Nodes[5]);

            // Print shortest path
            Console.WriteLine(ShortestPathToString(graph, shortestPath));

            // Print the execution time in milliseconds
            // by using the property elapsed milliseconds
            Console.WriteLine($"The Execution time of graph.GetAllPaths(graph.Nodes[6], " +
                $"graph.Nodes[5]) is {watch.ElapsedTicks} ticks");
        }

        private static string ShortestPathToString(Graph graph, List<Node?> shortestPath)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Shortest Path from {graph.Nodes[6].Id} to {graph.Nodes[5].Id}\n");
            for (int i = shortestPath.Count - 1; i >= 0; i--)
            {
                sb.Append($"{shortestPath[i].Predecessor.Id} --> " +
                    $"{shortestPath[i].Id} (Distance: {shortestPath[i].Distance})\n");
            }
            return sb.ToString();
        }

        private static string PathsToString(List<List<Node>> paths)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("All found paths:\n");
            int i = 1;
            foreach (var path in paths)
            {
                if (i != 1)
                    sb.Append("-------------------\n");
                sb.Append($"Path {i}:\n");
                foreach (var node in path)
                {
                    sb.Append($"{node.Id} --> {node.Next.Id}\n");
                }
                i++;
            }
            sb.Append("-------------------\n");
            sb.Append($"Total Paths: {paths.Count}\n");
            sb.Append("-------------------\n");
            return sb.ToString();
        }
    }
}
