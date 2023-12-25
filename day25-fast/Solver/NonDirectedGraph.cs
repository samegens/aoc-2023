using System.Text;

namespace AoC;

public class GraphNode
{
    public GraphNode(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }

    public int ShortestPath { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        GraphNode other = (GraphNode)obj;

        return other.Name.Equals(Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
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
        return $"{First.Name}-{Second.Name} ({Cost})";
    }
}

public class NonDirectedGraph
{
    private readonly Dictionary<GraphNode, List<Edge>> _adjacencyList = new();
    // Dictionary to speed up position lookups
    private readonly Dictionary<string, GraphNode> _nameToNodeMap = new();

    public NonDirectedGraph()
    {
    }

    public int Count => _nameToNodeMap.Count;

    public void AddVertex(GraphNode vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = new List<Edge>();
            _nameToNodeMap[vertex.Name] = vertex;
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

    public bool Contains(string name) => _nameToNodeMap.ContainsKey(name);

    public GraphNode GetAt(string name)
    {
        return _nameToNodeMap[name];
    }

    public GraphNode GetOrCreate(string name)
    {
        if (!Contains(name))
        {
            var node = new GraphNode(name);
            AddVertex(node);
            return node;
        }

        return _nameToNodeMap[name];
    }

    public void Print()
    {
        foreach (var kvp in _adjacencyList)
        {
            Console.Write($"{kvp.Key} -> ");
            Console.WriteLine(string.Join(", ", kvp.Value));
        }
    }

    public string ToGraphViz()
    {
        StringBuilder sb = new();
        sb.AppendLine("graph G {");
        HashSet<Tuple<string, string>> visited = new();
        foreach (var node in _nameToNodeMap.Values)
        {
            foreach (var edge in _adjacencyList[node])
            {
                Tuple<string, string> t1 = new(node.Name, edge.Second.Name);
                Tuple<string, string> t2 = new(edge.Second.Name, node.Name);
                if (!visited.Contains(t1) && !visited.Contains(t2))
                {
                    sb.AppendLine($"    \"{node.Name}\" -- \"{edge.Second.Name}\"");
                    visited.Add(t1);
                }
            }
        }
        sb.AppendLine("}");
        return sb.ToString();
    }
}
