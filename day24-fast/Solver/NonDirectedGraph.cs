namespace AoC;

public class GraphNode
{
    public GraphNode(Point pos)
    {
        Pos = pos;
    }

    public Point Pos { get; set; }

    public override string ToString()
    {
        return $"{Pos}";
    }

    public int ShortestPath { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        GraphNode other = (GraphNode)obj;

        return other.Pos.Equals(Pos);
    }

    public override int GetHashCode()
    {
        return Pos.GetHashCode();
    }
}

public class Edge
{
    public GraphNode First { get; private set; }
    public GraphNode Second { get; private set; }
    public int Cost { get; private set; }

    public Edge(GraphNode first, GraphNode second, int cost)
    {
        if (first.Equals(second))
        {
            throw new Exception("second should be different");
        }

        First = first;
        Second = second;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"{First.Pos}-{Second.Pos} ({Cost})";
    }
}

public class NonDirectedGraph
{
    private readonly Dictionary<GraphNode, List<Edge>> _adjacencyList = new();
    // Dictionary to speed up position lookups
    private readonly Dictionary<Point, GraphNode> _pointToNodeMap = new();

    public NonDirectedGraph()
    {
    }

    public int Count => _pointToNodeMap.Count;

    public void AddVertex(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = new List<Edge>();
            _pointToNodeMap[vertex.Pos] = vertex;
        }
    }

    public void AddEdge(GraphNode vertex1, GraphNode vertex2, int cost)
    {
        if (!_adjacencyList.ContainsKey(vertex1))
        {
            throw new Exception($"Vertex {vertex1} not found");
        }

        if (!_adjacencyList.ContainsKey(vertex2))
        {
            throw new Exception($"Vertex {vertex2} not found");
        }

        _adjacencyList[vertex1].Add(new Edge(vertex1, vertex2, cost));
        _adjacencyList[vertex2].Add(new Edge(vertex2, vertex1, cost));
    }

    public List<Edge> GetEdges(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            return new List<Edge>();
        }

        return _adjacencyList[vertex];
    }

    public List<GraphNode> GetNeighbors(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            return new List<GraphNode>();
        }

        return _adjacencyList[vertex].Select(e => e.First).ToList();
    }

    public bool Contains(Point pos) => _pointToNodeMap.ContainsKey(pos);

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

    public int ComputeShortestPathFromStartToEnd(Point start, Point end)
    {
        GraphNode endNode = GetAt(end);

        var queue = new PriorityQueue<GraphNode, int>();
        var visited = new HashSet<GraphNode>();
        var shortestPaths = new Dictionary<GraphNode, int>();
        foreach (var kvp in _adjacencyList)
        {
            shortestPaths[kvp.Key] = int.MaxValue;
        }

        shortestPaths[GetAt(start)] = 0;
        queue.Enqueue(GetAt(start), 0);
        while (queue.Count > 0)
        {
            GraphNode node = queue.Dequeue();
            if (visited.Contains(node))
            {
                continue;
            }

            visited.Add(node);
            foreach (Edge edge in GetEdges(node))
            {
                GraphNode neighbor = edge.Second;
                int distance = edge.Cost;
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
