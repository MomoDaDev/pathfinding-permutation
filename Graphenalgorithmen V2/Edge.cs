namespace Graphenalgorithmen_V2
{
    internal class Edge
    {
        #region Properties
        public Node N1 { get; set; }
        public Node N2 { get; set; }
        public int Distance { get; set; }
        #endregion

        #region Initialization
        public Edge(Node n1, Node n2)
        {
            N1 = n1;
            N2 = n2;
            Distance = Graph.random.Next(1, 21);
        }
        #endregion

        #region Calculations
        public void CalculateDistance()
        {
            Distance = Math.Abs(N1.X - N2.X) + Math.Abs(N1.Y - N2.Y);
        }
        #endregion
    }
}
