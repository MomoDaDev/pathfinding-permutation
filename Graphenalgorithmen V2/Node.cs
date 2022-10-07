namespace Graphenalgorithmen_V2
{
    internal class Node
    {
        #region Properties
        /// <summary>
        /// Is the name of the Node
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Is used for storing a path
        /// </summary>
        public Node? Next { get; set; }
        /// <summary>
        /// Cordinate X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Cordinate Y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Used for Dijkstra path finding
        /// </summary>
        public HashSet<Node> Neighbours { get; set; }
        /// <summary>
        /// Used for Dijkstra path finding
        /// </summary>
        public Node? Predecessor { get; set; }
        /// <summary>
        /// Used for Dijkstra path finding
        /// </summary>
        public int Distance { get; set; }
        #endregion

        #region Initialization
        public Node()
        {
            Id = String.Empty;
            Neighbours = new HashSet<Node>();
        }
        public Node(string id) : this()
        {
            Id = id;
        }

        public Node(string id, int x, int y) : this(id)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Copy
        public Node DeepCopy()
        {
            Node n2 = new Node(this.Id);
            n2.Next = this.Next;
            n2.X = this.X;
            n2.Y = this.Y;
            n2.Neighbours = this.Neighbours;
            n2.Predecessor = this.Predecessor;
            n2.Distance = this.Distance;

            return n2;
        }
        #endregion

        #region Overrides
        public override bool Equals(object? obj)
        {
            return obj is Node node &&
                   Id == node.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
        #endregion
    }
}
