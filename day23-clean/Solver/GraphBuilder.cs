public class GraphBuilder
{
    private readonly Board _board;
    private readonly Point _start;
    private readonly Point _end;
    private readonly HashSet<Point> _visited = new();
    public NonDirectedGraph Graph { get; private set; } = new();
    public GraphNode? EndNode { get; private set; }
    public GraphNode? StartNode { get; private set; }
    public int NrStepsToFirstNode { get; private set; }
    public int NrStepsFromEndNode { get; private set; }

    public GraphBuilder(Board board, Point start, Point end)
    {
        _board = board;
        _start = start;
        _end = end;
    }

    public void Build()
    {
        VisitTile(null, _start, 0);
    }

    private void VisitTile(GraphNode? previousNode, Point p, int nrSteps)
    {
        if (TileIsJunction(p))
        {
            GraphNode nextNode = new(p);
            Graph.AddVertex(nextNode);
            if (previousNode != null)
            {
                Graph.AddEdge(previousNode, nextNode, nrSteps);
            }
            else
            {
                StartNode = nextNode;
                NrStepsToFirstNode = nrSteps;
            }

            previousNode = nextNode;
            nrSteps = 0;
        }

        if (p.Equals(_end))
        {
            EndNode = previousNode;
            NrStepsFromEndNode = nrSteps;
            return;
        }

        _visited.Add(p);
        VisitAdjacentTiles(previousNode, p, nrSteps);
    }

    private void VisitAdjacentTiles(GraphNode? previousNode, Point p, int nrSteps)
    {
        foreach (Point newP in DirectionHelpers.Movements.Values.Select(p.Move))
        {
            if (_visited.Contains(newP) &&
                (!Graph.Contains(newP) || Graph.GetAt(newP).Equals(previousNode)))
            {
                continue;
            }
            if (!_board.Contains(newP) || _board[newP] == '#')
            {
                continue;
            }
            VisitTile(previousNode, newP, nrSteps + 1);
        }
    }

    private bool TileIsJunction(Point p)
    {
        int x = p.X;
        int y = p.Y;
        if (_board[p] == '#')
        {
            return false;
        }

        int nrPossibleDirections = 0;
        if (y > 0 && _board[x, y - 1] != '#')
        {
            nrPossibleDirections++;
        }
        if (y < _board.Height - 1 && _board[x, y + 1] != '#')
        {
            nrPossibleDirections++;
        }
        if (x > 0 && _board[x - 1, y] != '#')
        {
            nrPossibleDirections++;
        }
        if (x < _board.Width - 1 && _board[x + 1, y] != '#')
        {
            nrPossibleDirections++;
        }

        return nrPossibleDirections > 2;
    }
}