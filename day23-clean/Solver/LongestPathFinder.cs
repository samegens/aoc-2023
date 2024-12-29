public class LongestPathFinder
{
    private readonly NonDirectedGraph _graph;
    private int _longestPath = int.MinValue;
    private readonly GraphNode _start;
    private readonly GraphNode _end;
    private readonly HashSet<GraphNode> _visited = new();

    public LongestPathFinder(NonDirectedGraph graph, GraphNode start, GraphNode end)
    {
        _graph = graph;
        _start = start;
        _end = end;
    }

    public int Go()
    {
        VisitNode(_start, 0);
        return _longestPath;
    }

    private void VisitNode(GraphNode node, int nrSteps)
    {
        if (node == _end)
        {
            if (nrSteps > _longestPath)
            {
                _longestPath = nrSteps;
            }
        }

        _visited.Add(node);
        foreach (Edge edge in _graph.GetEdges(node))
        {
            if (!edge.First.Equals(node))
            {
                throw new Exception("bug: first should be source node");
            }
            if (!_visited.Contains(edge.Second))
            {
                VisitNode(edge.Second, nrSteps + edge.Cost);
            }
        }
        _visited.Remove(node);
    }
}