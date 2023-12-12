public class GraphNode
{
    public GraphNode(int riskLevel, Point pos)
    {
        RiskLevel = riskLevel;
        Pos = pos;
    }

    public int RiskLevel { get; set; }

    public Point Pos { get; set; }

    public override string ToString()
    {
        return $"{Pos}: {RiskLevel}";
    }

    public int ShortestPath { get; set; }
}

public class NonDirectedGraph
{
    private readonly Dictionary<GraphNode, List<GraphNode>> _adjacencyList;
    // Dictionary to speed up position lookups
    private readonly Dictionary<Point, GraphNode> _pointToNodeMap = new Dictionary<Point, GraphNode>();

    public NonDirectedGraph()
    {
        _adjacencyList = new Dictionary<GraphNode, List<GraphNode>>();
    }

    public void AddVertex(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = new List<GraphNode>();
            _pointToNodeMap[vertex.Pos] = vertex;
        }
    }

    public void AddEdge(GraphNode vertex1, GraphNode vertex2)
    {
        if (!_adjacencyList.ContainsKey(vertex1))
        {
            throw new Exception($"Vertex {vertex1} not found");
        }

        if (!_adjacencyList.ContainsKey(vertex2))
        {
            throw new Exception($"Vertex {vertex2} not found");
        }

        _adjacencyList[vertex1].Add(vertex2);
    }

    public List<GraphNode> GetNeighbors(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            return new List<GraphNode>();
        }

        return _adjacencyList[vertex];
    }

    public GraphNode GetAt(Point pos)
    {
        return _pointToNodeMap[pos];
    }

    public void Print()
    {
        foreach (var kvp in _adjacencyList)
        {
            Console.Write($"{kvp.Key} -> ");
            Console.WriteLine(string.Join(", ", kvp.Value));
        }
    }

    public int ComputeShortestPathFromStartToEnd()
    {
        int maxX = _adjacencyList.Keys.Max(n => n.Pos.X);
        int maxY = _adjacencyList.Keys.Max(n => n.Pos.Y);
        GraphNode endNode = GetAt(new Point(maxX, maxY));

        var queue = new PriorityQueue<GraphNode, int>();
        var visited = new HashSet<GraphNode>();
        var shortestPaths = new Dictionary<GraphNode, int>();
        foreach (var kvp in _adjacencyList)
        {
            shortestPaths[kvp.Key] = int.MaxValue;
        }

        shortestPaths[GetAt(new Point(0, 0))] = 0;
        queue.Enqueue(GetAt(new Point(0, 0)), 0);
        while (queue.Count > 0)
        {
            GraphNode node = queue.Dequeue();
            if (visited.Contains(node))
            {
                continue;
            }

            visited.Add(node);
            foreach (GraphNode neighbor in GetNeighbors(node))
            {
                int distance = neighbor.RiskLevel;
                if (shortestPaths[node] + distance < shortestPaths[neighbor])
                {
                    shortestPaths[neighbor] = shortestPaths[node] + distance;
                }
                queue.Enqueue(neighbor, shortestPaths[neighbor]);
            }
        }

        return shortestPaths[endNode];
    }
}
