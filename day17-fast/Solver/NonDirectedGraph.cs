public class GraphNode
{
    public GraphNode(int heatLoss, Point pos)
    {
        HeatLoss = heatLoss;
        Pos = pos;
    }

    public int HeatLoss { get; set; }

    public Point Pos { get; set; }

    public override string ToString()
    {
        return $"{Pos}: {HeatLoss}";
    }

    public int ShortestPath { get; set; }
}

public class NonDirectedGraph
{
    private readonly Dictionary<GraphNode, List<GraphNode>> _adjacencyList;
    // Dictionary to speed up position lookups
    private readonly Dictionary<Point, GraphNode> _pointToNodeMap = new Dictionary<Point, GraphNode>();

    public int Width { get; private set; }
    public int Height { get; private set; }

    public NonDirectedGraph(int width, int height)
    {
        _adjacencyList = new Dictionary<GraphNode, List<GraphNode>>();
        Width = width;
        Height = height;
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

    enum Direction
    {
        Up, Right, Down, Left
    }

    static Dictionary<Direction, Point> _directionToDeltaMap = new()
    {
        [Direction.Up] = new Point(0, -1),
        [Direction.Right] = new Point(1, 0),
        [Direction.Down] = new Point(0, 1),
        [Direction.Left] = new Point(-1, 0)
    };

    static Dictionary<Direction, Direction> _reverseMap = new()
    {
        [Direction.Up] = Direction.Down,
        [Direction.Right] = Direction.Left,
        [Direction.Down] = Direction.Up,
        [Direction.Left] = Direction.Right
    };

    class NodeState
    {
        public GraphNode GraphNode;
        public Direction Direction;
        public int StepsSetInDirection;

        public NodeState(GraphNode node, Direction direction, int nrSteps)
        {
            GraphNode = node;
            Direction = direction;
            StepsSetInDirection = nrSteps;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            NodeState other = (NodeState)obj;

            return other.GraphNode == GraphNode &&
                other.Direction == Direction &&
                other.StepsSetInDirection == StepsSetInDirection;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GraphNode, Direction, StepsSetInDirection);
        }
    }

    public int ComputeShortestPathFromStartToEnd()
    {
        int maxX = _adjacencyList.Keys.Max(n => n.Pos.X);
        int maxY = _adjacencyList.Keys.Max(n => n.Pos.Y);
        GraphNode endNode = GetAt(new Point(maxX, maxY));

        var queue = new PriorityQueue<NodeState, int>();
        var visited = new HashSet<NodeState>();
        var shortestPaths = new Dictionary<NodeState, int>();

        NodeState nodeState = new(GetAt(new Point(0, 0)), Direction.Up, 0);
        shortestPaths[nodeState] = 0;
        queue.Enqueue(nodeState, 0);
        while (queue.Count > 0)
        {
            nodeState = queue.Dequeue();
            if (visited.Contains(nodeState))
            {
                continue;
            }

            visited.Add(nodeState);
            GraphNode node = nodeState.GraphNode;
            // foreach (GraphNode neighbor in GetNeighbors(nodeState.))
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                // Can't move in opposite direction:
                if (dir == _reverseMap[nodeState.Direction]) continue;
                // Can't travel further than 3 steps in same direction:
                if (dir == nodeState.Direction && nodeState.StepsSetInDirection >= 3) continue;

                Point neighborPos = node.Pos.Move(_directionToDeltaMap[dir]);
                if (!IsPosValid(neighborPos)) continue;

                GraphNode neighbor = GetAt(neighborPos);
                int distance = neighbor.HeatLoss;
                NodeState neighborNodeState = new(neighbor, dir, dir != nodeState.Direction ? 1 : nodeState.StepsSetInDirection + 1);
                if (!shortestPaths.ContainsKey(neighborNodeState) || shortestPaths[nodeState] + distance < shortestPaths[neighborNodeState])
                {
                    shortestPaths[neighborNodeState] = shortestPaths[nodeState] + distance;
                }
                queue.Enqueue(neighborNodeState, shortestPaths[neighborNodeState]);
            }
        }

        return shortestPaths
            .Where(kv => kv.Key.GraphNode == endNode)
            .Select(kv => kv.Value)
            .Min();
    }

    public int ComputeShortestPathFromStartToEndPart2()
    {
        int maxX = _adjacencyList.Keys.Max(n => n.Pos.X);
        int maxY = _adjacencyList.Keys.Max(n => n.Pos.Y);
        GraphNode endNode = GetAt(new Point(maxX, maxY));

        var queue = new PriorityQueue<NodeState, int>();
        var visited = new HashSet<NodeState>();
        var shortestPaths = new Dictionary<NodeState, int>();

        NodeState nodeState = new(GetAt(new Point(0, 0)), Direction.Right, 0);
        shortestPaths[nodeState] = 0;
        shortestPaths[new(GetAt(new Point(0, 0)), Direction.Down, 0)] = 0;
        queue.Enqueue(nodeState, 0);
        while (queue.Count > 0)
        {
            nodeState = queue.Dequeue();
            if (visited.Contains(nodeState))
            {
                continue;
            }

            visited.Add(nodeState);
            GraphNode node = nodeState.GraphNode;
            // foreach (GraphNode neighbor in GetNeighbors(nodeState.))
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                // Can't move in opposite direction:
                if (dir == _reverseMap[nodeState.Direction]) continue;
                // Can't travel further than 10 steps in same direction:
                if (dir == nodeState.Direction && nodeState.StepsSetInDirection >= 10) continue;
                // Must travel at least 4 steps in the same direction:
                if (dir != nodeState.Direction && nodeState.StepsSetInDirection < 4) continue;

                Point neighborPos = node.Pos.Move(_directionToDeltaMap[dir]);
                if (!IsPosValid(neighborPos)) continue;

                GraphNode neighbor = GetAt(neighborPos);
                int distance = neighbor.HeatLoss;
                NodeState neighborNodeState = new(neighbor, dir, dir != nodeState.Direction ? 1 : nodeState.StepsSetInDirection + 1);
                if (!shortestPaths.ContainsKey(neighborNodeState) || shortestPaths[nodeState] + distance < shortestPaths[neighborNodeState])
                {
                    shortestPaths[neighborNodeState] = shortestPaths[nodeState] + distance;
                }
                queue.Enqueue(neighborNodeState, shortestPaths[neighborNodeState]);
            }
        }

        return shortestPaths
            .Where(kv => kv.Key.GraphNode == endNode)
            .Select(kv => kv.Value)
            .Min();
    }

    private bool IsPosValid(Point p)
    {
        return p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;
    }
}
