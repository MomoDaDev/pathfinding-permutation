namespace Graphenalgorithmen_V2
{
    internal class Graph
    {
        #region Properties
        public static Random random = new Random();
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        #endregion

        #region Initialization
        public Graph(List<Node> nodes, List<Edge> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }
        #endregion

        #region Field Generating
        public void GenerateField(int n)
        {
            // Initialize Nodes and Edges with calculated size
            Nodes = new List<Node>(n * n);
            Edges = new List<Edge>((n - 1) * n * 2);

            // Generate Nodes
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    Nodes.Add(new Node($"{i}{j.ToStr()}", i, j));

            // Generate Edges
            // Connect Collumns
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n - 1; j++)
                    Edges.Add(new Edge(Nodes.First(x => x.X == i && x.Y == j), Nodes.First(x => x.X == i && x.Y == j + 1)));

            // Connect Rows
            for (int i = 1; i <= n - 1; i++)
                for (int j = 1; j <= n; j++)
                    Edges.Add(new Edge(Nodes.First(x => x.X == i && x.Y == j), Nodes.First(x => x.X == i + 1 && x.Y == j)));
        }
        #endregion

        #region Path Finding
        /// <summary>
        /// Es ist sehr wenig Zeit für das Beispiel
        /// Man iteriert vom Startpunkt aus in alle Richtungen aber nie zurück zu den Nodes,
        /// wo man bereits gewesen ist.Falls der Knoten das Ziel ist, dann höre auf.
        /// Falls man nicht mehr weiter gehen kann aber das Ziel nicht erreicht wurde, dann
        /// schmeiße den Weg weg.
        /// 
        /// Es wird umgesetzt, indem man eine List mit Nodes führt und wenn sich die Wege bei
        /// zwei Nachbarn trennen, dann wird ein Branch erstellt, der zwei deepcopyies erstellt
        /// und mit diesen dann weiter arbeitet.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dest"></param>
        /// <param name="precision"></param>
        /// <returns>Returns a List of all found paths</returns>
        public List<List<Node>> GetAllPaths(Node start, Node dest)
        {
            // List of all correct paths
            List<List<Node>> correctPaths = new List<List<Node>>();

            // Queue for the next upcoming node
            HashSet<Route> q = new HashSet<Route>();

            // Set the Start node as the first upcoming node
            q.Add(new Route(start));

            // Loop through as long as there are nodes in the queue
            while (q.Count > 0)
            {
                // Get node and route from queue
                Route? route = q.First();
                q.Remove(route);
                HashSet<Node> neighbours = new HashSet<Node>();

                // Find all neighbours from the Node node with the help of the edges
                foreach (var edge in Edges)
                {
                    if (edge.N1.Equals(route.n))
                    {
                        if (!route.path.Contains(edge.N2))
                            neighbours.Add(edge.N2);
                    }
                    else if (edge.N2.Equals(route.n))
                    {
                        if (!route.path.Contains(edge.N1))
                            neighbours.Add(edge.N1);
                    }
                }

                // Get all neighbours that are valid to continue iterating through them
                foreach (var ne in neighbours)
                {
                    Route? r;
                    // Branch the route if there is more than 1 neighbour
                    // (huge performance increase at higher numbers)
                    if (neighbours.Count == 1)
                        r = route;
                    else
                        r = route.DeepCopy();

                    // Set up to store in the found path
                    r.n.Next = ne;

                    // Add a the deep copy to the path
                    r.path.Add(r.n);

                    // Update the node
                    r.n = r.n.Next;

                    // If the destination has been reached then stop
                    if (ne.Equals(dest))
                    {
                        // Add the path to the list
                        correctPaths.Add(r.path);

                        // Dispose route
                        r = null;
                    } else
                    {
                        // Enqueue the picked neighbour for the next upcoming node
                        q.Add(r);
                    }
                }

                // Dispose route
                route = null;
            }

            return correctPaths;
        }
        #endregion

        #region Dijkstra Algorithm
        public List<Node?> GetShortestPath(Node start, Node dest)
        {
            ResetNodes();
            InitializeNeighbours();
            start.Predecessor = start;

            DijkstraRecursive(start, 0);

            Node? n = dest;
            List<Node?> shortestPath = new List<Node?>();

            while (n != start)
            {
                shortestPath.Add(n);
                n = n.Predecessor;
            }

            return shortestPath;
        }

        private void DijkstraRecursive(Node n, int distance)
        {
            foreach (var ne in n.Neighbours)
            {
                // Get current edge between n and ne
                Edge edge = Edges.First(x => (x.N1 == n && x.N2 == ne) || (x.N1 == ne && x.N2 == n));

                if (distance + edge.Distance < ne.Distance)
                {
                    ne.Distance = distance + edge.Distance;
                    ne.Predecessor = n;
                    DijkstraRecursive(ne, ne.Distance);
                }
            }
        }

        private void ResetNodes()
        {
            foreach (var node in Nodes)
            {
                node.Distance = int.MaxValue;
                node.Predecessor = null;
                node.Next = null;
                node.Neighbours = new HashSet<Node>();
            }
        }

        private void InitializeNeighbours()
        {
            foreach (var edge in Edges)
            {
                edge.N1.Neighbours.Add(edge.N2);
                edge.N2.Neighbours.Add(edge.N1);
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return "Nodes:\n" + String.Join("\n", Nodes.Select(x => x.Id)) + "\n" +
                "Edges:\n" + String.Join("\n", Edges.Select(x => x.N1.Id + "-" + x.N2.Id));
        }
        #endregion
    }
}
