namespace Graphenalgorithmen_V2
{
    internal class Route
    {
        #region Properties
        public Node n { get; set; }
        public List<Node> path { get; set; }
        #endregion

        #region Initialization
        public Route(Node n)
        {
            this.n = n;
            this.path = new List<Node>();
        }

        public Route(Node n, List<Node> path) : this(n)
        {
            this.path = path;
        }
        #endregion

        #region Copy
        public Route DeepCopy()
        {
            // Create a Deep Copy of the path
            List<Node> _path = new List<Node>();

            // Iterate through path
            foreach (var node in this.path)
            {
                _path.Add(node.DeepCopy());
            }

            // Return instance of new route
            return new Route(n.DeepCopy(), _path);
        }
        #endregion
    }
}
