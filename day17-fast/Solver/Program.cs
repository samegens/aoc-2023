namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        int width = lines[0].Length;
        int height = lines.Length;
        int[,] heatloss = new int[width, height];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < width; x++)
            {
                heatloss[x, y] = line[x] - '0';
            }
        }
        heatloss[0, 0] = 0;

        NonDirectedGraph graph = new(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GraphNode node = new(heatloss[x, y], new Point(x, y));
                graph.AddVertex(node);
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Point p1 = new Point(x, y);
                GraphNode node1 = graph.GetAt(p1);
                if (y > 0)
                {
                    Point p2 = new Point(x, y - 1);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
                if (y < height - 1)
                {
                    Point p2 = new Point(x, y + 1);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
                if (x > 0)
                {
                    Point p2 = new Point(x - 1, y);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
                if (x < width - 1)
                {
                    Point p2 = new Point(x + 1, y);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
            }
        }

        int total = graph.ComputeShortestPathFromStartToEnd();

        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        int width = lines[0].Length;
        int height = lines.Length;
        int[,] heatloss = new int[width, height];
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < width; x++)
            {
                heatloss[x, y] = line[x] - '0';
            }
        }
        heatloss[0, 0] = 0;

        NonDirectedGraph graph = new(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GraphNode node = new(heatloss[x, y], new Point(x, y));
                graph.AddVertex(node);
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Point p1 = new Point(x, y);
                GraphNode node1 = graph.GetAt(p1);
                if (y > 0)
                {
                    Point p2 = new Point(x, y - 1);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
                if (y < height - 1)
                {
                    Point p2 = new Point(x, y + 1);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
                if (x > 0)
                {
                    Point p2 = new Point(x - 1, y);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
                if (x < width - 1)
                {
                    Point p2 = new Point(x + 1, y);
                    GraphNode node2 = graph.GetAt(p2);
                    graph.AddEdge(node1, node2);
                    graph.AddEdge(node2, node1);
                }
            }
        }

        int total = graph.ComputeShortestPathFromStartToEndPart2();

        Console.WriteLine($"Part 2: {total}");
    }
}
