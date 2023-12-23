
namespace AoC;

enum Direction
{
    Up, Right, Down, Left
}


public class Program
{

    static Dictionary<Direction, Point> directions = new()
    {
        [Direction.Up] = new Point(0, -1),
        [Direction.Right] = new Point(1, 0),
        [Direction.Down] = new Point(0, 1),
        [Direction.Left] = new Point(-1, 0),
    };
    static int longestPath = int.MinValue;
    static NonDirectedGraph graph = new();
    static GraphNode? endNode = null;
    static GraphNode? startNode = null;
    static int stepsToFirstNode = 0;
    static int stepsFromEndNode = 0;

    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        int total = 0;
        int width = lines[0].Length;
        int height = lines.Length;
        char[,] tiles = new char[width, height];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
                tiles[x, y] = line[x];
        }

        Point p = new(1, 0);
        Point end = new(width - 2, height - 1);

        HashSet<Point> visited = new();

        Visit(p, visited, 0, end, tiles, width, height);

        Console.WriteLine($"Part 1: {longestPath}");
    }

    private static void Visit(Point p, HashSet<Point> visited, int nrSteps, Point end, char[,] tiles, int width, int height)
    {
        if (p.Equals(end))
        {
            if (nrSteps > longestPath) longestPath = nrSteps;
            return;
        }

        visited.Add(p);
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            var newP = p.Move(directions[dir]);
            if (visited.Contains(newP)) continue;
            int x = newP.X;
            int y = newP.Y;
            if (newP.X < 0 || newP.X >= width || newP.Y < 0 || newP.Y >= height) continue;
            if (tiles[x, y] == '#') continue;
            if (tiles[x, y] == '^' && dir != Direction.Up) continue;
            if (tiles[x, y] == '>' && dir != Direction.Right) continue;
            if (tiles[x, y] == 'v' && dir != Direction.Down) continue;
            if (tiles[x, y] == '<' && dir != Direction.Left) continue;
            Visit(newP, visited, nrSteps + 1, end, tiles, width, height);
        }
        visited.Remove(p);
    }

    private static void SolvePart2(string[] lines)
    {
        int total = 0;
        int width = lines[0].Length;
        int height = lines.Length;
        char[,] tiles = new char[width, height];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
                tiles[x, y] = line[x];
        }

        Point p = new(1, 0);
        Point end = new(width - 2, height - 1);

        HashSet<Point> visited = new();

        List<Point> junctions = new();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int dirCount = 0;
                if (tiles[x, y] != '#')
                {
                    if (y > 0 && tiles[x, y - 1] != '#') dirCount++;
                    if (y < height - 1 && tiles[x, y + 1] != '#') dirCount++;
                    if (x > 0 && tiles[x - 1, y] != '#') dirCount++;
                    if (x < width - 1 && tiles[x + 1, y] != '#') dirCount++;

                    if (dirCount > 2) junctions.Add(new Point(x, y));
                }
            }
        }

        BuildGraph(null, p, visited, 0, end, tiles, width, height);
        graph.Print();

        longestPath = int.MinValue;
        // Visit2(p, visited, 0, end, tiles, width, height);
        HashSet<GraphNode> v = new();
        Visit3(startNode!, endNode!, v, stepsToFirstNode);

        Console.WriteLine($"Part 2: {longestPath}");
    }

    private static void Visit3(GraphNode node, GraphNode endNode, HashSet<GraphNode> visited, int nrSteps)
    {
        if (node == endNode)
        {
            if (nrSteps + stepsFromEndNode > longestPath)
            {
                longestPath = nrSteps + stepsFromEndNode;
                Console.WriteLine(longestPath);
            }
        }

        visited.Add(node);
        foreach (var edge in graph.GetEdges(node))
        {
            if (!edge.First.Equals(node)) throw new Exception("first should be source node");
            if (visited.Contains(edge.Second)) continue;
            Visit3(edge.Second, endNode, visited, nrSteps + edge.Cost);
        }
        visited.Remove(node);
    }

    private static void Visit2(Point p, HashSet<Point> visited, int nrSteps, Point end, char[,] tiles, int width, int height)
    {
        if (p.Equals(end))
        {
            if (nrSteps > longestPath)
            {
                longestPath = nrSteps;
                Console.WriteLine(longestPath);
            }
            return;
        }

        visited.Add(p);
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            var newP = p.Move(directions[dir]);
            if (visited.Contains(newP)) continue;
            int x = newP.X;
            int y = newP.Y;
            if (newP.X < 0 || newP.X >= width || newP.Y < 0 || newP.Y >= height) continue;
            if (tiles[x, y] == '#') continue;
            // if (tiles[x, y] == '^' && dir != Direction.Up) continue;
            // if (tiles[x, y] == '>' && dir != Direction.Right) continue;
            // if (tiles[x, y] == 'v' && dir != Direction.Down) continue;
            // if (tiles[x, y] == '<' && dir != Direction.Left) continue;
            Visit2(newP, visited, nrSteps + 1, end, tiles, width, height);
        }
        visited.Remove(p);
    }

    static void BuildGraph(GraphNode? node, Point p, HashSet<Point> visited, int nrSteps, Point end, char[,] tiles, int width, int height)
    {
        if (TileIsJunction(p, tiles, width, height))
        {
            GraphNode newNode = new GraphNode(p);
            graph.AddVertex(newNode);
            if (node != null)
            {
                graph.AddEdge(node, newNode, nrSteps);
            }
            else
            {
                startNode = newNode;
                stepsToFirstNode = nrSteps;
            }

            node = newNode;
            nrSteps = 0;
        }

        if (p.Equals(end))
        {
            endNode = node;
            stepsFromEndNode = nrSteps;
            return;
        }

        visited.Add(p);
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            var newP = p.Move(directions[dir]);
            if (visited.Contains(newP))
            {
                if (!graph.Contains(newP)) continue;
                if (graph.GetAt(newP) == node) continue;
            }
            int x = newP.X;
            int y = newP.Y;
            if (newP.X < 0 || newP.X >= width || newP.Y < 0 || newP.Y >= height) continue;
            if (tiles[x, y] == '#') continue;
            BuildGraph(node, newP, visited, nrSteps + 1, end, tiles, width, height);
        }
        // visited.Remove(p);
    }

    private static bool TileIsJunction(Point p, char[,] tiles, int width, int height)
    {
        int dirCount = 0;
        int x = p.X;
        int y = p.Y;
        if (tiles[x, y] == '#') return false;
        if (y > 0 && tiles[x, y - 1] != '#') dirCount++;
        if (y < height - 1 && tiles[x, y + 1] != '#') dirCount++;
        if (x > 0 && tiles[x - 1, y] != '#') dirCount++;
        if (x < width - 1 && tiles[x + 1, y] != '#') dirCount++;

        return dirCount > 2;
    }
}
